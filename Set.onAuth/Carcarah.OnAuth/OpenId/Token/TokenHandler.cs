using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Token
{
    public static class TokenHandler
    {
        public static string Sha256(this string value)
        {
            var sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                var encript = Encoding.UTF8;
                var results = hash.ComputeHash(encript.GetBytes(value));

                foreach (Byte b in results)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static string GenerateAuthorizationCodeToken(this CarcarahOnAuthContext context, string code)
        {
            var exp = DateTime.Now.AddSeconds(context.Client.AuthorizationCodeLifetime).Ticks;

            var payload = new Dictionary<string, object>
            {
                ["iss"] = "https://onauth.set.rn.gov.br",
                ["sub"] = context.ClaimByType(ClaimTypes.Sid).Sha256(),
                ["aud"] = context.Client.ClientId.Sha256(),
                ["nonce"] = context.State,
                ["exp"] = exp,
                ["iat"] = DateTime.Now.Ticks,
                ["tid"] = context.ClaimByType(ClaimTypes.Sid),
                ["cod"] = code
            };

            return JWT.JsonWebToken.Encode(
                        payload,
                        context.Client.ClientSecrets[0],
                        JWT.JwtHashAlgorithm.HS256);
        }
    }
}

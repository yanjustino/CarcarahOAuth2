using System.Collections.Generic;
using static JWT.JsonWebToken;
using Carcarah.OnAuth.Server.Repositories;

namespace Carcarah.OnAuth.Server.OpenId.Token
{
    public class ClientSecretJwtToken
    {
        public static Client Get(CarcarahOAuthContext context)
        {
            var token = CookieRepository.Get(context.Request.Context, 
                CookieRepository.CLIENT_SECRET_JWT);

            var dic = DecodeToObject(token, context.Options.SecretKey)
                as IDictionary<string, object>;

            return context.Options.ClientById(dic["sub"].ToString());
        }

        public static string Register(CarcarahOAuthContext context)
        {
            var seconds = context.Client.AuthorizationCodeLifetime;

            var payload = new Dictionary<string, object>
            {
                ["iss"] = "https://onauth.set.rn.gov.br",
                ["sub"] = context.Client.ClientId,
                ["aud"] = "https://onauth.set.rn.gov.br/token",
                ["jti"] = context.State,
                ["exp"] = TokenHandler.GenerateExp(seconds),
                ["iat"] = TokenHandler.GenerateExp(0),
            };
            var token = Encode(payload, context.Options.SecretKey, JWT.JwtHashAlgorithm.HS256);
            CookieRepository.Set(context.Request.Context, CookieRepository.CLIENT_SECRET_JWT, token);

            return token;
        }
    }
}

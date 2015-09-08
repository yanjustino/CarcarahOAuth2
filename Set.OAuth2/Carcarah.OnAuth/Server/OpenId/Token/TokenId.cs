using Carcarah.OnAuth.Server.OpenId.Response;
using Carcarah.OnAuth.Server.Repositories;
using System;
using System.Collections.Generic;
using static JWT.JsonWebToken;

namespace Carcarah.OnAuth.Server.OpenId.Token
{
    public class TokenId
    {
        public static SuccessfulTokenResponse Register(CarcarahOAuthContext context, Client client)
        {
            var jti = Guid.NewGuid().ToString().Sha256();
            var seconds = client.AuthorizationCodeLifetime;
            var exp = TokenHandler.GenerateExp(seconds);

            var payload = new Dictionary<string, object>
            {
                ["iss"] = "https://onauth.set.rn.gov.br",
                ["sub"] = client.ClientId,
                ["aud"] = "https://onauth.set.rn.gov.br/token",
                ["jti"] = jti,
                ["exp"] = exp,
                ["iat"] = TokenHandler.GenerateExp(0),
                ["nonce"] = context.State,
            };

            var token = Encode(payload, client.ClientSecrets[0], JWT.JwtHashAlgorithm.HS256);

            return new SuccessfulTokenResponse
            {
                access_token = context.State.Substring(0, 10),
                refresh_token = jti.Substring(0, 10),
                expires_in = exp.ToString(),
                token_type = "Bearer",
                id_token = token
            };
        }

        public static void Remove(CarcarahOAuthContext context)
        {
            CookieRepository.Remove(context.Request.Context, CookieRepository.TOKEN_ID);
        }
    }
}

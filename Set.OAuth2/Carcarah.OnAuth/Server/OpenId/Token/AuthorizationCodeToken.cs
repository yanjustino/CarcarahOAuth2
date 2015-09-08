using Carcarah.OnAuth.Server.Repositories;
using System.Collections.Generic;
using static JWT.JsonWebToken;

namespace Carcarah.OnAuth.Server.OpenId.Token
{
    public class AuthorizationCodeToken
    {
        public static bool Validate(CarcarahOAuthContext context, Client client)
        {
            var token = CookieRepository.Get(context.Request.Context,
                CookieRepository.AUTHORIZATION_CODE);

            var dic = DecodeToObject(token, client.ClientSecrets[0])
                as IDictionary<string, object>;

            return dic["sub"].ToString().Equals(client.ClientId) &&
                   dic["code"].ToString().Equals(context.Request.Body.Code().Result) &&
                   dic["redirect_uri"].ToString().Equals(context.Request.Body.RedirectUri().Result) &&
                   dic["scope"].ToString().Contains("openid") &&
                   context.Request.Body.GrantType().Result.Equals("authorization_code");
        }

        public static void Register(CarcarahOAuthContext context, string code)
        {
            var seconds = context.Client.AuthorizationCodeLifetime;

            var payload = new Dictionary<string, object>
            {
                ["iss"] = "https://onauth.set.rn.gov.br",
                ["sub"] = context.Client.ClientId,
                ["aud"] = "https://onauth.set.rn.gov.br/token",
                ["nonce"] = context.State,
                ["exp"] = TokenHandler.GenerateExp(seconds),
                ["iat"] = TokenHandler.GenerateExp(0),
                ["jit"] = code,
                ["code"] = code,
                ["scope"] = context.Request.Query.scope,
                ["redirect_uri"] = context.Request.Query.redirect_uri
            };

            var token = Encode(payload, context.Client.ClientSecrets[0], JWT.JwtHashAlgorithm.HS256);

            CookieRepository.Set(context.Request.Context, CookieRepository.AUTHORIZATION_CODE, token);
        }
    }
}

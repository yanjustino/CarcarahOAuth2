using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcarah.OnAuth.OpenId.Request;
using Carcarah.OnAuth.Options;
using System.Security.Claims;
using Carcarah.OnAuth.OpenId.Token;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow.Flows
{
    public class AuthorizationCodeFlow : AuthenticationFlowBase, IAuthenticationFlow
    {
        public AuthorizationCodeFlow(CarcarahOnAuthContext context) : base(context) { }

        protected override string SuccessfulAuthenticationResponse()
        {
            var state = TokenHandler.Sha256(Guid.NewGuid().ToString());
            var token = GenerateAuthorizationCode();

            return $"?code={token}&state";
        }

        private string GenerateAuthorizationCode()
        {
            var sta = Context.State;
            var cid = Context.Client.ClientId;
            var sid = Context.ClaimByType(ClaimTypes.Sid);
            var cod = TokenHandler.Sha256(Guid.NewGuid().ToString());
            var exp = DateTime.Now.AddSeconds(Context.Client.AuthorizationCodeLifetime).Ticks;

            var payload = new Dictionary<string, object>
            {
                ["iss"] = "https://onauth.set.rn.gov.br",
                ["sub"] = TokenHandler.Sha256(Context.ClaimByType(ClaimTypes.Sid)),
                ["aud"] = TokenHandler.Sha256(Context.Client.ClientId),
                ["nonce"] = Context.State,
                ["exp"] = exp,
                ["iat"] = DateTime.Now.Ticks,
                ["tid"] = Context.ClaimByType(ClaimTypes.Sid)
            };

            var token = JWT.JsonWebToken.Encode(
                        payload, 
                        Context.Client.ClientSecrets[0], 
                        JWT.JwtHashAlgorithm.HS256);

            Context.RegisterAuthorizationCodeToken(token);

            return cod;
        }
    }
}

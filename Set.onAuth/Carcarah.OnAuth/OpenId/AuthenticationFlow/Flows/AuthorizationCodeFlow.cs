using Carcarah.OnAuth.OpenId.Token;
using Carcarah.OnAuth.Repositories;
using System;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow.Flows
{
    public class AuthorizationCodeFlow : AuthenticationFlowBase, IAuthenticationFlow
    {
        public AuthorizationCodeFlow(CarcarahOnAuthContext context) : base(context) { }

        protected override string SuccessfulAuthenticationResponse()
        {
            var code = Guid.NewGuid().ToString().Sha256();
            var token = Context.GenerateAuthorizationCodeToken(code);

            OwinContext.RegisterAuthorizationCodeToken(token);

            return $"code={code}&state={Context.State}";
        }
    }
}

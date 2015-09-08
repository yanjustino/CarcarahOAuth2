using Carcarah.OnAuth.Server.Configuration;
using Carcarah.OnAuth.Server.OpenId.AuthenticationFlow.Flows;
using Carcarah.OnAuth.Server.OpenId.Request;
using Microsoft.Owin;

namespace Carcarah.OnAuth.Server.OpenId.AuthenticationFlow
{
    public class AuthenticationFlowFactory
    {
        public static IAuthenticationFlow New(IOwinContext owinContext, OAuthOptions options)
        {
            var request = new AuthenticationRequest(owinContext);
            var context = new CarcarahOAuthContext(options, request);

            switch (context.Request.Query.response_type?.Trim())
            {
                case "code": return new AuthorizationCodeFlow(context);
                case "id_token": return new ImplicitFlow(context);
                case "id_token token": return new ImplicitFlow(context);
                case "code id_token": return new HybridFlow(context);
                case "code token": return new HybridFlow(context);
                case "code id_token token": return new HybridFlow(context);
                default: return null;
            }
        }

        public static IAuthenticationFlow NewAuthorizationCodeFlow(IOwinContext owinContext, OAuthOptions options)
        {
            var request = new AuthenticationRequest(owinContext);
            var context = new CarcarahOAuthContext(options, request);

            return new AuthorizationCodeFlow(context);
        }
    }
}

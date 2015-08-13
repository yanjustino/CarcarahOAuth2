using Carcarah.OnAuth.OpenId.AuthenticationFlow.Flows;
using Carcarah.OnAuth.OpenId.Request;
using Carcarah.OnAuth.Options;
using Microsoft.Owin;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow
{
    public class AuthenticationFlowFactory
    {
        public static IAuthenticationFlow New(IOwinContext owinContext, OnAuthOptions options)
        {
            var request = new AuthenticationRequest(owinContext);
            var context = new CarcarahOnAuthContext(options, request);

            switch (context.Request.Query.response_type.Trim())
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
    }
}

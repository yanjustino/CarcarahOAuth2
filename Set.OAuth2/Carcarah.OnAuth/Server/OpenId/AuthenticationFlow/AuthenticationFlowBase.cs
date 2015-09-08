using Microsoft.Owin;

namespace Carcarah.OnAuth.Server.OpenId.AuthenticationFlow
{
    public abstract class AuthenticationFlowBase
    {
        protected IOwinContext OwinContext { get; }
        protected CarcarahOAuthContext Context { get; }

        public AuthenticationFlowBase(CarcarahOAuthContext context)
        {
            Context = context;
            OwinContext = context.Request.Context;
        }
    }
}

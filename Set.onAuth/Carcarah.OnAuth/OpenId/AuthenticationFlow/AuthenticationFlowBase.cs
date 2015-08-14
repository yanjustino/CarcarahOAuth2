using System.Threading.Tasks;
using Carcarah.OnAuth.Options;
using Carcarah.OnAuth.Repositories;
using Microsoft.Owin;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow
{
    public abstract class AuthenticationFlowBase
    {
        protected CarcarahOnAuthContext Context { get; }
        protected IOwinContext OwinContext { get; }

        public AuthenticationFlowBase(CarcarahOnAuthContext context)
        {
            Context = context;
            OwinContext = context.Request.Context;
        }

        public async Task AuthorizeEndUser()
        {
            var IsAuthorized = await Context.FindUserInMemory() || 
                               await Context.GrantResourceOwnerCredentials();

            if (!IsAuthorized)
                OwinContext.Unauthorized(Context.Options);
            else
            {
                var response = SuccessfulAuthenticationResponse();
                var redirect_uri = $"{Context.Request.Query.redirect_uri}?{response}";

                OwinContext.Authorized(redirect_uri);
            }
        }

        protected abstract string SuccessfulAuthenticationResponse();
    }
}

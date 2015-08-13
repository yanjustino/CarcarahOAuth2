using System.Threading.Tasks;
using Carcarah.OnAuth.Options;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow
{
    public abstract class AuthenticationFlowBase
    {
        public CarcarahOnAuthContext Context { get; }

        public AuthenticationFlowBase(CarcarahOnAuthContext context)
        {
            this.Context = context;
        }
        
        public async Task AuthorizeEndUser()
        {
            var IsAuthorized = await this.Context.GrantResourceOwnerCredentials();

            if (!IsAuthorized)
                this.Context.Request.Context.Unauthorized(this.Context.Options);
            else
            {
                var response = SuccessfulAuthenticationResponse();
                var redirect_uri = this.Context.Request.Query.redirect_uri;
                this.Context.Request.Context.Authorized($"{redirect_uri}?{response}");
            }
        }

        protected abstract string SuccessfulAuthenticationResponse();
    }
}

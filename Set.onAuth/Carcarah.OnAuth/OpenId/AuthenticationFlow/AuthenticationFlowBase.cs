using System.Threading.Tasks;
using Carcarah.OnAuth.Config;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow
{
    /// <summary>
    /// Description (pt-br):
    ///     Classe base para operações genéricas de um 
    ///     fluxo de autenticação específico
    /// </summary>
    public abstract class AuthenticationFlowBase
    {
        public CarcarahOnAuthContext Context { get; }

        public AuthenticationFlowBase(CarcarahOnAuthContext context)
        {
            this.Context = context;
        }
        
        public async Task AuthorizeEndUser()
        {
            var grantOwnerCredentials = GrantResourceOwnerCredentials();

            var IsAuthorized = await grantOwnerCredentials;

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

        private Task<bool> GrantResourceOwnerCredentials()
        {
            return this.Context.Options.AuthorizationProvider.GrantResourceOwnerCredentials(this.Context);
        }
    }
}

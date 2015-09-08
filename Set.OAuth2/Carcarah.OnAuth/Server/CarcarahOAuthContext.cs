using Carcarah.OnAuth.Server.Configuration;
using Carcarah.OnAuth.Server.OpenId;
using Carcarah.OnAuth.Server.OpenId.Request;
using Carcarah.OnAuth.Server.OpenId.Token;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Server
{
    public sealed class CarcarahOAuthContext
    {
        internal string State { get; }
        internal Client Client { get; }
        internal OAuthOptions Options { get; }
        internal AuthenticationRequest Request { get; }
        internal ClaimsIdentity Identity { get; private set; }

        public async Task<string> UserName() => await Request.Body.UserName();
        public async Task<string> Password() => await Request.Body.Password();

        public CarcarahOAuthContext(OAuthOptions options, AuthenticationRequest request)
        {
            this.Options = options;
            this.Request = request;
            this.Client = options.Clients.FirstOrDefault(x => x.ClientId == Request.Query.client_id);
            this.State = TokenHandler.Sha256(Guid.NewGuid().ToString());

            if (!Request.Context.Request.Path.Equals(Routes.TokenEndPoint))
            {
                request.ValidateAuthenticationRequest();
                RequestAssertationConcern.IsNotNull(Client, "client_not_found");
                RequestAssertationConcern.IsTrue(Client.Enabled, "client_not_enabled");
            }
        }

        public void AddIdentityClaims(ClaimsIdentity identity) =>
            this.Identity = identity;

        internal string ClaimByType(string type) =>
            Identity.Claims.FirstOrDefault(x => x.Type == type)?.Value;

        internal async Task<bool> GrantResourceOwnerCredentials()
        {
            return await Options.AuthorizationProvider.GrantResourceOwnerCredentials(this);
        }

        internal async Task<bool> FindUserInMemory()
        {
            var user = await UserName();
            var pass = await Password();

            var findedUser = Options.Users.FirstOrDefault(x => x.Username == user && x.Password == pass);

            if (findedUser == null)
                return false;
            else
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, findedUser.Username));

                AddIdentityClaims(identity);
                return true;
            }
        }
    }
}

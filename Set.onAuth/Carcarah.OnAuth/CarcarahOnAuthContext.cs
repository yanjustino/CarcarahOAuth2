using Carcarah.OnAuth.OpenId;
using Carcarah.OnAuth.OpenId.AuthenticationFlow;
using Carcarah.OnAuth.OpenId.Request;
using Carcarah.OnAuth.OpenId.Response;
using Carcarah.OnAuth.OpenId.Token;
using Carcarah.OnAuth.Options;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth
{
    public sealed class CarcarahOnAuthContext
    {
        internal Client Client { get; }
        internal OnAuthOptions Options { get; }
        internal AuthenticationRequest Request { get; }
        internal ClaimsIdentity Identity { get; private set; }
        internal string State { get; }

        public async Task<string> UserName() => await Request.Body.FindUserName();
        public async Task<string> Password() => await Request.Body.FindPassword();

        public CarcarahOnAuthContext(OnAuthOptions options, AuthenticationRequest request)
        {
            this.Options = options;
            this.Request = request;
            this.Client = options.Clients.FirstOrDefault(x => x.ClientId == Request.Query.client_id);
            this.State = TokenHandler.Sha256(Guid.NewGuid().ToString());

            RequestAssertationConcern.IsNotNull(Client, "client_not_found");
            RequestAssertationConcern.IsTrue(Client.Enabled, "client_not_enabled");
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
                identity.AddClaim(new Claim(ClaimTypes.Sid, findedUser.Subject));

                AddIdentityClaims(identity);
                return true;
            }
        }
    }
}

using Carcarah.OAuth2.Server.Configuration;
using Carcarah.OAuth2.Server.Helpers;
using Carcarah.OAuth2.Server.OpenId;
using Carcarah.OAuth2.Server.OpenId.AuthenticationFlow;
using Carcarah.OAuth2.Server.OpenId.Request;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server
{
    public sealed class OAuthContext
    {
        internal IOwinContext OwinConext { get; }
        internal OAuthOptions Options { get; }
        internal AuthenticationRequest AuthenticationRequest { get; }
        internal TokenRequest TokenRequest { get; }
        internal ClaimsIdentity Identity { get; private set; }
        internal string SubjectId { get; private set; }

        internal Client CurrentClient =>
            Options.Clients.FirstOrDefault(Client.ClientIdScope(AuthenticationRequest.client_id));

        public string Username { get; private set; }
        public string Password { get; private set; }

        public OAuthContext(OAuthOptions options, AuthenticationRequest request, IOwinContext context)
        {
            Options = options;
            AuthenticationRequest = request;
            OwinConext = context;
        }

        public OAuthContext(OAuthOptions options, TokenRequest request, IOwinContext context)
        {
            Options = options;
            TokenRequest = request;
            OwinConext = context;
        }

        public void SignIn(ClaimsIdentity identity)
        {
            Identity = identity;
            SubjectId = $"{Username}:{Password}".ToBase64String();
            OwinConext.Authentication.SignIn(identity);
        }

        internal bool GrantUserInMemoryCredentials()
        {
            var user = Options.Users.FirstOrDefault(User.Find(Username, Password));
            if (user == null)
                return false;
            else
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, Username));

                SignIn(identity);

                return true;
            }
        }

        internal async Task<bool> GrantResourceOwnerCredentials(string username, string password)
        {
            await SetCredentials(username, password);
            var grantResourceOwner = Options.AuthorizationProvider
                                            .GrantResourceOwnerCredentials(this);
            
            return GrantUserInMemoryCredentials() || await grantResourceOwner;
        }

        private Task SetCredentials(string username, string password)
        {
            this.Username = username;
            this.Password = password;

            return Task.FromResult(0);
        }
    }
}

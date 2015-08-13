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
        internal AuthenticationResponse Response { get; }
        internal ClaimsIdentity Identity { get; private set; }
        internal string State { get; }

        public string UserName { get; }
        public string Password { get; }

        public CarcarahOnAuthContext(OnAuthOptions options, AuthenticationRequest request)
        {
            this.Options = options;
            this.Request = request;
            this.Response = new AuthenticationResponse(request.Context);
            this.UserName = request.Body.UserName;
            this.Password = request.Body.Password;
            this.Client = options.Clients.FirstOrDefault(x => x.ClientId == Request.Query.client_id);
            this.State = TokenHandler.Sha256(Guid.NewGuid().ToString());

            RequestAssertationConcern.IsNotNull(Client, "client_not_found");
            RequestAssertationConcern.IsTrue(Client.Enabled, "client_not_enabled");
        }

        public void AddIdentityClaims(ClaimsIdentity identity) =>
            this.Identity = identity;

        internal string ClaimByType(string type) =>
            Identity.Claims.FirstOrDefault(x => x.Type == type)?.Value;

        internal Task<bool> GrantResourceOwnerCredentials() =>
            Options.AuthorizationProvider.GrantResourceOwnerCredentials(this);

        internal void RegisterAuthorizationCodeToken(string token) =>
            Request.Context.RegisterAuthorizationCodeToken(token);
    }
}

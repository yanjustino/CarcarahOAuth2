using Carcarah.OnAuth.OpenId.AuthenticationFlow;
using Carcarah.OnAuth.OpenId.Request;
using Carcarah.OnAuth.OpenId.Response;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth
{
    public class CarcarahOnAuthContext
    {
        internal CarcarahOnAuthOptions Options { get; }
        internal ClaimsIdentity Identity { get; private set; }
        internal AuthenticationRequest Request { get; }
        internal AuthenticationResponse Response { get; }

        public string UserName { get; }
        public string Password { get; }

        public CarcarahOnAuthContext(CarcarahOnAuthOptions options, AuthenticationRequest request)
        {
            this.Options = options;
            this.Request = request;
            this.Response = new AuthenticationResponse(request.Context);
            this.UserName = request.Body.UserName;
            this.Password = request.Body.Password;
        }

        public void AddIdentityClaims(ClaimsIdentity identity) =>
            this.Identity = identity;
    }
}

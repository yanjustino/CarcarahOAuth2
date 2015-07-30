using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Carcarah.OnAuth.Config
{
    public abstract class CarcarahAuthorizationServerProvider
    {
        internal IOwinContext Context { get; set; }

        public abstract Task<bool> GrantResourceOwnerCredentials();

        internal Task<bool> ValidateClientAuthentication()
        {
            return Task.FromResult<bool>(this.Context.TokenRegistered());
        }

        //public void AddClaims(IEnumerable<Claim> claims = null)
        //{
        //    var identity = new ClaimsIdentity(claims);

        //    Context.RegisterToken("true");
        //}

        internal async Task<bool> IsAuthenticated()
        {
            return await GrantResourceOwnerCredentials() || await ValidateClientAuthentication();
        }

    }
}

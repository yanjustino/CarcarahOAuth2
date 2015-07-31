using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IO;
using Carcarah.OnAuth.OpenId.Request;

namespace Carcarah.OnAuth.Config
{
    public abstract class CarcarahAuthorizationProvider
    {
        internal IOwinContext Context { get; set; }

        public abstract Task<bool> GrantResourceOwnerCredentials(string userName, string passWord);

        protected void Validate(ClaimsIdentity identity)
        {
            CarcarahCookieHandler.RegisterToken(this.Context, "true");
        }

        internal Task<bool> ValidateClientAuthentication()
        {
            Console.WriteLine("Validate Client");
            return Task.FromResult<bool>(this.Context.TokenRegistered());
        }
    }
}

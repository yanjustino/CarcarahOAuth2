using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Response
{
    public class AuthenticationResponse
    {
        internal IOwinContext Context { get; }
        internal ResponseCookieCollection Cookies { get; }

        public AuthenticationResponse(IOwinContext Context)
        {
            this.Context = Context;
            this.Cookies = Context.Response.Cookies;
        }
    }
}

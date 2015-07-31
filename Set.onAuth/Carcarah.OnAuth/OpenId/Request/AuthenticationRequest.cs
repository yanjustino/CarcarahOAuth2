using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Request
{
    internal class AuthenticationRequest
    {
        public AuthenticationRequestParams Params { get; private set; }
        public AuthenticationRequestBody Body { get; private set; }

        public AuthenticationRequest(IOwinContext context)
        {
            this.Params = new AuthenticationRequestParams(context);
            this.Body = new AuthenticationRequestBody(context);
        }
    }
}

using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId
{
    internal class AuthenticationRequestHandler
    {
        public AuthenticationRequestParams Params { get; private set; }

        public AuthenticationRequestHandler(IOwinContext context)
        {
            this.Params = new AuthenticationRequestParams(context);
        }
    }
}

using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Request
{
    public class TokenRequest : RequestBase
    {
        public string redirect_uri { get; private set; } 
        public string grant_type { get; private set; }
        public string code { get; private set; }

        public async Task<string> username() => await FindInForm("username");
        public async Task<string> password() => await FindInForm("password");

        public TokenRequest(IOwinContext context) : base(context) { }

        public async Task LoadAsync()
        {
            redirect_uri = await Find("redirect_uri");
            grant_type = await Find("grant_type");
            code = await Find("code");
        }
    }
}

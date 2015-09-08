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
        public Task<string> redirect_uri() => 
            Task.FromResult(FindInForm("redirect_uri").Result);

        public Task<string> grant_type() => 
            Task.FromResult(FindInForm("grant_type").Result);

        public Task<string> code() => 
            Task.FromResult(FindInForm("code").Result);

        public async Task<string> username() =>
            await FindInForm("username");

        public async Task<string> password() =>
            await FindInForm("password");

        public TokenRequest(IOwinContext context) : base(context) { }
    }
}

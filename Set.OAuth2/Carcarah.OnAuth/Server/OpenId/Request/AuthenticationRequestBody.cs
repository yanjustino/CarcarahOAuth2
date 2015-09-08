using Microsoft.Owin;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Server.OpenId.Request
{
    public class AuthenticationRequestBody
    {
        private IOwinContext context;

        public AuthenticationRequestBody(IOwinContext context)
        {
            this.context = context;
        }

        public async Task<string> GrantType() => 
            await Find("grant_type");

        public async Task<string> Code() =>
            await Find("code");

        public async Task<string> RedirectUri() =>
            await Find("redirect_uri");

        public async Task<string> UserName() => 
            await Find("username");

        public async Task<string> Password() => 
            await Find("password");

        private async Task<string> Find(string index)
        {
            if (context.Request.Body == null)
                return null;

            var form = await context.Request.ReadFormAsync();
            return form[index];
        }
    }
}

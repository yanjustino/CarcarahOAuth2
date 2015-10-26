using Microsoft.Owin;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Request
{
    public abstract class RequestBase
    {
        public RequestMethod Method { get; }
        protected IOwinContext Context { get; }

        protected async Task<string> Find(string param)
        {
            var result = Context.Request.Query[param] != null ?
                         Context.Request.Query[param] :
                         await FindInForm(param);

            return result;
        }

        public RequestBase(IOwinContext context)
        {
            Context = context;
            Method = context.Request.Method.Equals("POST") ? RequestMethod.POST : RequestMethod.GET;
        }

        public async Task<string> FindInForm(string key)
        {
            var form = await Context.Request.ReadFormAsync();
            return form[key];
        }
    }
}

using System;
using System.Linq;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carcarah.OnAuth.Options;

namespace Carcarah.OnAuth
{
    using OpenId.AuthenticationFlow;
    using OpenId.Request;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class CarcarahOnAuth
    {
        private AppFunc next;
        private IOwinContext context;
        private OnAuthOptions options;

        public CarcarahOnAuth(AppFunc next, OnAuthOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            context = new OwinContext(environment);

            try
            {
                bool isEndSessionEndPoint =
                    context.Request.Path.Equals(options.EndSessionEndPoint);

                bool isAuthorizationEndpoint =
                    context.Request.Path.Equals(options.AuthorizationEndpoint);

                if (isEndSessionEndPoint)
                    await Signout();
                else if (isAuthorizationEndpoint)
                    await next.Invoke(environment);
                else
                {
                    var flow = AuthenticationFlowFactory.New(context, options);

                    await flow.AuthorizeEndUser();
                    await next.Invoke(environment);
                }
            }
            catch (AuthenticationRequestException ex)
            {
                context.BadRequest(ex.Message);
            }
            catch
            {
                context.InternalServerError();
            }
        }

        private async Task Signout()
        {
            context.DeleteToken();
            context.Unauthorized(options);

            await Task.FromResult<int>(1);
        }
    }
}

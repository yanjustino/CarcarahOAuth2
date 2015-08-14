using System;
using System.Linq;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carcarah.OnAuth.Options;
using Carcarah.OnAuth.Repositories;

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
                if (IsLogoutRequest())
                {
                    await context.Singout(options);
                }
                else if (IsLoginRequest())
                {
                    await next.Invoke(environment);
                }
                else
                {
                    await AuthenticationFlowFactory
                        .New(context, options)
                        .AuthorizeEndUser();
                }
            }
            catch (AuthenticationRequestException ex)
            {
                context.AuthenticationErrorResponse(ex.Description);
            }
            catch
            {
                context.InternalServerError();
            }
        }

        private bool IsLoginRequest()
        {
            return context.Request.Path.Equals(options.AuthorizationUri) ||
                context.Request.Path.Equals(options.AuthorizationEndpoint);
        }

        private bool IsLogoutRequest()
        {
            return context.Request.Path.Equals(options.EndSessionEndPoint);
        }
    }
}

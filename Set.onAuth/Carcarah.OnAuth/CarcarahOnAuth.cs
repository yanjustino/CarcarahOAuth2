using System;
using System.Linq;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using Carcarah.OnAuth.Config;

namespace Carcarah.OnAuth
{
    using OpenId.AuthenticationFlow;
    using OpenId.Request;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class CarcarahOnAuth
    {
        private AppFunc next;
        private IOwinContext context;
        private CarcarahOnAuthOptions options;

        public CarcarahOnAuth(AppFunc next, CarcarahOnAuthOptions options)
        {
            this.next = next;
            this.options = options;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            context = new OwinContext(environment);

            try
            {
                if (context.Request.Path == options.EndSessionEndPoint)
                {
                    context.DeleteToken();
                    context.Unauthorized(options);
                }
                else if (context.Request.Path == options.AuthorizationEndpoint)
                    await next.Invoke(environment);
                else
                    await Authorize(environment);

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

        private async Task Authorize(IDictionary<string, object> environment)
        {
            var request = new AuthenticationRequest(context);
            var onAuthContext = new CarcarahOnAuthContext(options, request);
            var flow = AuthenticationFlowFactory.New(onAuthContext);

            if (request.TokenRegistered())
                return;
            else
                await flow.AuthorizeEndUser();

            await next.Invoke(environment);
        }
    }
}

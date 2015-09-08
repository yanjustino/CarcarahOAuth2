using System;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.Server
{
    using OpenId;
    using Configuration;
    using OpenId.AuthenticationFlow;
    using OpenId.Request;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class CarcarahOAuth
    {
        private AppFunc next;
        private IOwinContext context;
        private OAuthOptions options;

        public CarcarahOAuth(AppFunc next, OAuthOptions options)
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
                else if (IsTokenRequest())
                {
                    await AuthenticationFlowFactory
                        .NewAuthorizationCodeFlow(context, options)
                        .TokenRequestValidation();
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                context.InternalServerError();
            }
        }

        private bool IsTokenRequest()
        {
            return context.Request.Method.Equals("POST") && 
                   context.Request.Path.Equals(Routes.TokenEndPoint);
        }

        private bool IsLoginRequest()
        {
            return context.Request.Path.Equals(options.AuthorizationUri) ||
                context.Request.Path.Equals(Routes.AuthorizationEndpoint);
        }

        private bool IsLogoutRequest()
        {
            return context.Request.Path.Equals(Routes.EndSessionEndPoint);
        }
    }
}

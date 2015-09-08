using System;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server
{
    using OpenId;
    using Configuration;
    using OpenId.AuthenticationFlow;
    using OpenId.Request;
    using AppFunc = Func<IDictionary<string, object>, Task>;
    using Helpers;
    using System.Collections.Concurrent;
    using OpenId.AuthenticationFlow.Storage;

    public class CarcarahOAuth
    {
        private AppFunc next;
        private IOwinContext context;
        private readonly OAuthOptions options;

        internal AuthorizationCodeStorage AuthCodeStorage { get; }
        internal RefreshTokenStorage RefreshTokenStorage { get; }

        public CarcarahOAuth(AppFunc next, OAuthOptions options)
        {
            this.next = next;
            this.options = options;
            AuthCodeStorage = new AuthorizationCodeStorage();
            RefreshTokenStorage = new RefreshTokenStorage();
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            context = new OwinContext(environment);

            try
            {
                if (context.IsLogoutRequest())
                {
                    await context.Singout(options);
                }
                else if (context.IsLoginRequest(options))
                {
                    await next.Invoke(environment);
                }
                else if (context.IsTokenRequest())
                {
                    var tokenEndPoint = FlowFactory.GetTokenEndProint
                        (
                            context,
                            options,
                            AuthCodeStorage,
                            RefreshTokenStorage
                        );

                    await tokenEndPoint.TokenRequest();
                }
                else
                {
                    var flow = FlowFactory.Get(context, options, AuthCodeStorage, RefreshTokenStorage);

                    await flow.AuthenticatesEndUser();
                }
            }
            catch (AuthenticationRequestException ex)
            {
                context.BadRequest(ex);
            }
            catch (UnauthorizedException ex)
            {
                context.Unauthorized(options, ex);
            }
            catch (Exception ex)
            {
                var aex = new AuthenticationRequestException("internal_error", ex.Message);
                context.BadRequest(aex);
            }
        }
    }
}

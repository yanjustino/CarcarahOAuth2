using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Carcarah.OnAuth.Config;

[assembly: OwinStartup(typeof(Set.onAuth.Server.Startup))]

namespace Set.onAuth.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCarcarahMiddleware(new CarcarahMiddlewareConfigOptions
            {
                Provider = new MyProvider()
            });
        }
    }

    public class MyProvider : CarcarahAuthorizationServerProvider
    {
        public override Task<bool> GrantResourceOwnerCredentials()
        {
            //AddClaims();
            return Task.FromResult<bool>(false);
        }
    }
}

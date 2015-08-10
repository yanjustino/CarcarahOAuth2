using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Carcarah.OnAuth.Config;
using Carcarah.OnAuth;
using System.Security.Claims;

[assembly: OwinStartup(typeof(Set.onAuth.Server.Startup))]

namespace Set.onAuth.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCarcarahMiddleware(new CarcarahOnAuthOptions
            {
                AuthorizationEndpoint = new PathString("/onauth/authorize"),
                AuthorizationProvider = new MyProvider()
            });
        }
    }

    public class MyProvider : CarcarahAuthorizationProvider
    {
        public override Task<bool> GrantResourceOwnerCredentials(CarcarahOnAuthContext context)
        {
            var isValid = context.UserName == "yan" && context.Password == "master";

            if (isValid)
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, "yan"));
                identity.AddClaim(new Claim(ClaimTypes.Sid, "010101"));

                context.AddIdentityClaims(identity);
            }

            return Task.FromResult<bool>(isValid);
        }
    }
}

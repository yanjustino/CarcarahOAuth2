using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Carcarah.OnAuth;
using System.Security.Claims;
using Carcarah.OnAuth.Options;
using Set.onAuth.Server.App_Start;

[assembly: OwinStartup(typeof(Set.onAuth.Server.Startup))]

namespace Set.onAuth.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCarcarahMiddleware(new OnAuthOptions
            {
                AuthorizationEndpoint = new PathString("/onauth/authorize"),
                AuthorizationProvider = new MyProvider(),
                Clients = Clients.Build()
            });
        }
    }

    public class MyProvider : AuthorizationProvider
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

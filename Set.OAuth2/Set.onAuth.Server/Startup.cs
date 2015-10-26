using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Security.Claims;
using Set.onAuth.Server.App_Start;
using Carcarah.OAuth2.Server.Configuration;
using Carcarah.OAuth2.Server;
using System;

[assembly: OwinStartup(typeof(Set.onAuth.Server.Startup))]

namespace Set.onAuth.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCarcarahMiddleware(new OAuthOptions
            {
                AuthorizationUri = new PathString("/onauth/authorize"),
                AuthorizationProvider = new MyProvider(),
                Clients = Clients.Get(),
                Users = Users.Get()
            });
        }
    }

    public class MyProvider : AuthorizationProvider
    {
        public override Task<bool> GrantResourceOwnerCredentials(OAuthContext context)
        {
            var isValid = context.Username == "yan" && context.Password == "master";

            if (isValid)
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, context.Username));
                identity.AddClaim(new Claim(ClaimTypes.Hash, ""));

                context.SignIn(identity);
            }

            return Task.FromResult(isValid);
        }
    }
}

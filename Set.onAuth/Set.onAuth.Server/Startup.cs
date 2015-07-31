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
        public override Task<bool> GrantResourceOwnerCredentials(string username, string password)
        {
            Console.WriteLine("Validate Provider");

            var user = username;
            var pass = password;

            var isValid = user == "yan" && pass == "master";

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Name, "yan"));

            Validate(identity);

            return Task.FromResult<bool>(isValid);
        }
    }
}

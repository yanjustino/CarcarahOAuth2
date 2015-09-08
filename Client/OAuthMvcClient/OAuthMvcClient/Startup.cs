using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OAuthMvcClient.Startup))]
namespace OAuthMvcClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

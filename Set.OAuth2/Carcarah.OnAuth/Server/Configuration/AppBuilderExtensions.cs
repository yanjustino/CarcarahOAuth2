using Owin;

namespace Carcarah.OnAuth.Server.Configuration
{
    public static class AppBuilderExtensions
    {
        public static void UseCarcarahMiddleware(this IAppBuilder app, OAuthOptions config)
        {
            app.Use<CarcarahOAuth>(config);
        }
    }
}

using Owin;

namespace Carcarah.OnAuth.Config
{
    public static class AppBuilderExtensions
    {
        public static void UseCarcarahMiddleware(this IAppBuilder app, CarcarahMiddlewareConfigOptions config)
        {
            app.Use<CarcarahOnAuthMiddleware>(config);
        }
    }
}

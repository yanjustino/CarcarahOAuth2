using Owin;

namespace Carcarah.OnAuth.Config
{
    public static class AppBuilderExtensions
    {
        public static void UseCarcarahMiddleware(this IAppBuilder app, CarcarahOnAuthOptions config)
        {
            app.Use<CarcarahOnAuth>(config);
        }
    }
}

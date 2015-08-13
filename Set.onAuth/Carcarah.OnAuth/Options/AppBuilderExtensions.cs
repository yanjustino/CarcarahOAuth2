using Owin;

namespace Carcarah.OnAuth.Options
{
    public static class AppBuilderExtensions
    {
        public static void UseCarcarahMiddleware(this IAppBuilder app, OnAuthOptions config)
        {
            app.Use<CarcarahOnAuth>(config);
        }
    }
}

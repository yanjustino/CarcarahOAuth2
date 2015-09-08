using Microsoft.Owin;
using System.Threading.Tasks;
using Carcarah.OnAuth.Server.Configuration;
using Carcarah.OnAuth.Server.OpenId.Response;
using Carcarah.OnAuth.Server.Repositories;

namespace Carcarah.OnAuth.Server
{
    internal static class CarcarahOAuthResponse
    {
        static internal void Authorized(this IOwinContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = "OK";
        }

        static internal void Authorized(this IOwinContext context, string redirectUri)
        {
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = "OK";
            context.Response.Redirect(redirectUri);
        }

        static internal void Unauthorized(this IOwinContext context, string redirect_url)
        {
            context.ClearAllTokens();
            context.Response.StatusCode = 401;
            context.Response.Redirect(redirect_url);
        }

        static internal void Unauthorized(this IOwinContext context, OAuthOptions options)
        {
            var redirect_url =
                $"{options.AuthorizationUri.Value}{context.Request.QueryString}";

            context.ClearAllTokens();
            context.Response.StatusCode = 401;
            context.Response.Redirect(redirect_url);
        }

        static internal void AuthenticationErrorResponse(this IOwinContext context, string error)
        {
            context.Response.StatusCode = 302;
            context.Response.ReasonPhrase = $"invalid_request";
        }

        static internal void TokenErrorResponse(this IOwinContext context)
        {
            context.Response.StatusCode = 400;
            context.Response.ReasonPhrase = $"Bad Request";

            context.Response.ContentType = "application/json";
            context.Response.Headers.Add("Cache-Control", new string[] { "no-store" });
            context.Response.Headers.Add("Pragma", new string[] { "no-cache" });

            context.Response.Write("bad");
        }

        static internal void SuccessfulToken(this IOwinContext context, SuccessfulTokenResponse response)
        {
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = $"OK";

            context.Response.ContentType = "application/json";
            context.Response.Headers.Add("Cache-Control", new string[] { "no-store" });
            context.Response.Headers.Add("Pragma", new string[] { "no-cache" });

            context.Response.Write(response.GenerateJson());
        }

        static internal void InternalServerError(this IOwinContext context)
        {
            context.Response.StatusCode = 501;
            context.Response.ReasonPhrase = "Internal Server Error";
        }

        static internal Task Singout(this IOwinContext context, OAuthOptions options)
        {
            Unauthorized(context, options);

            return Task.FromResult<int>(0);
        }
    }
}

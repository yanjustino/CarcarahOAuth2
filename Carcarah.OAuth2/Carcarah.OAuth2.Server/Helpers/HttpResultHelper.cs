using Carcarah.OAuth2.Server.Configuration;
using Carcarah.OAuth2.Server.OpenId.Request;
using Carcarah.OAuth2.Server.OpenId.Response;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.Helpers
{
    public static class HttpResultHelper
    {
        public static bool IsPostBack(this IOwinContext context)
        {
            return context.Request.Method.Equals("POST");
        }

        public static bool IsPath(this IOwinContext context, string path)
        {
            return context.Request.Path.Value.Equals(path);
        }

        public static bool IsTokenRequest(this IOwinContext context)
        {
            return IsPostBack(context) && IsPath(context, RequestRoutes.TokenEndPoint.Value);
        }

        public static bool IsLoginRequest(this IOwinContext context, OAuthOptions options)
        {
            return context.Request.Path.Equals(options.AuthorizationUri);
        }

        public static bool IsLogoutRequest(this IOwinContext context)
        {
            return context.Request.Path.Equals(RequestRoutes.EndSessionEndPoint);
        }

        static internal void Authorized(this IOwinContext context, Uri redirectUri)
        {
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = "OK";
            context.Response.Redirect(redirectUri.AbsoluteUri);
        }

        static internal void Authorized(this IOwinContext context, string value, string contentType = "application/json")
        {
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = "OK";
            context.Response.ContentType = contentType;

            context.Response.Write(value);
        }

        static internal void Unauthorized(this IOwinContext context, OAuthOptions options, UnauthorizedException ex)
        {
            context.Response.StatusCode = 403;
            context.Response.ReasonPhrase = "Forbidden";

            if (ex.prompt.Equals("login"))
            {
                var url = $"{options.AuthorizationUri.Value}{context.Request.QueryString}";
                context.Response.Redirect(url);
            }
            else if (ex.prompt.Equals("none"))
            {
                var state = GetState(context);
                var response = new AuthenticationErrorResponse(ex.error, ex.error_description, state);

                context.Response.ContentType = "application/json";
                context.Response.Write(response.Json());
            }
        }

        static internal void BadRequest(this IOwinContext context, AuthenticationRequestException exception, string contentType = "")
        {
            var state = GetState(context);
            var redirect_uri = GetRedirectUri(context);

            var response = new AuthenticationErrorResponse(exception.error, exception.error_description, state);

            context.Response.StatusCode = 400;
            context.Response.ReasonPhrase = $"Bad Request";

            if (string.IsNullOrEmpty(redirect_uri))
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(response.Json());
            }
            else
            {
                context.Response.Redirect(response.UrlEncoded(redirect_uri));
            }
        }

        static internal void SuccessfulToken(this IOwinContext context, SuccessfulTokenResponse response)
        {
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = $"OK";

            context.Response.ContentType = "application/json";
            context.Response.Headers.Add("Cache-Control", new string[] { "no-store" });
            context.Response.Headers.Add("Pragma", new string[] { "no-cache" });

            context.Response.Write(response.Json());
        }

        static internal void InternalServerError(this IOwinContext context)
        {
            context.Response.StatusCode = 501;
            context.Response.ReasonPhrase = "Internal Server Error";
        }

        static internal Task Singout(this IOwinContext context, OAuthOptions options)
        {
            //Unauthorized(context, options);

            return Task.FromResult<int>(0);
        }

        private static string GetRedirectUri(IOwinContext context)
        {
            return context.Request.Query["redirect_uri"] ?? string.Empty;
        }

        private static string GetState(IOwinContext context)
        {
            return context.Request.Query["state"] ?? string.Empty;
        }
    }
}

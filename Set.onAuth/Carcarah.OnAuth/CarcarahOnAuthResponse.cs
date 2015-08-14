using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carcarah.OnAuth.Options;
using Carcarah.OnAuth.Repositories;

namespace Carcarah.OnAuth
{
    internal static class CarcarahOnAuthResponse
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
            context.DeleteToken();
            context.Response.StatusCode = 401;
            context.Response.Redirect(redirect_url);
        }

        static internal void Unauthorized(this IOwinContext context, OnAuthOptions options)
        {
            var redirect_url =
                $"{options.AuthorizationUri.Value}{context.Request.QueryString}";

            context.DeleteToken();
            context.Response.StatusCode = 401;
            context.Response.Redirect(redirect_url);
        }

        static internal void AuthenticationErrorResponse(this IOwinContext context, string error)
        {
            context.Response.StatusCode = 302;
            context.Response.ReasonPhrase = $"invalid_request {error}";
        }

        static internal void InternalServerError(this IOwinContext context)
        {
            context.Response.StatusCode = 501;
            context.Response.ReasonPhrase = "Internal Server Error";
        }

        static internal Task Singout(this IOwinContext context, OnAuthOptions options)
        {
            context.DeleteToken();
            Unauthorized(context, options);

            return Task.FromResult<int>(0);
        }
    }
}

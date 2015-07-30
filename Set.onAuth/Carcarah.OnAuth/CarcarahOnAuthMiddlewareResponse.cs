﻿using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth
{
    internal static class CarcarahOnAuthMiddlewareResponse
    {
        static internal void Authorize(this IOwinContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.ReasonPhrase = "OK";
        }

        static internal void Deny(this IOwinContext context)
        {
            context.Response.StatusCode = 401;
            context.Response.ReasonPhrase = "invalid_grant";
        }

        static internal void BadRequest(this IOwinContext context, string message)
        {
            context.Response.StatusCode = 302;
            context.Response.ReasonPhrase = message;
        }

        static internal void InternalServerError(this IOwinContext context)
        {
            context.Response.StatusCode = 501;
            context.Response.ReasonPhrase = "Internal Server Error";
        }
    }
}
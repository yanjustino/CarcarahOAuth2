using Carcarah.OnAuth.OpenId.AuthenticationFlow;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Request
{
    public class AuthenticationRequestQuery
    {
        private IOwinContext context;

        public string scope { get; }
        public string response_type { get; }
        public string client_id { get; }
        public string redirect_uri { get; }
        public string state { get; }
        public string prompt { get; }

        public AuthenticationRequestQuery(IOwinContext context)
        {
            this.context = context;

            if (!this.context.Request.QueryString.HasValue)
                throw new AuthenticationRequestException("invalid_request_uri", "request uri is required");

            this.scope = context.Request.Query["scope"];
            this.response_type = context.Request.Query["response_type"];
            this.client_id = context.Request.Query["client_id"];
            this.redirect_uri = context.Request.Query["redirect_uri"];
            this.state = context.Request.Query["state"];
            this.prompt = context.Request.Query["prompt"];
        }

    }
}

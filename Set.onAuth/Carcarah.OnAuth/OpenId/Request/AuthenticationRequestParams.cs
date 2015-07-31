using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Request
{
    internal class AuthenticationRequestParams
    {
        private IOwinContext _context;

        public string scope { get; private set; }
        public string response_type { get; private set; }
        public string client_id { get; private set; }
        public string redirect_uri { get; private set; }
        public string state { get; private set; }
        public string prompt { get; private set; }

        public AuthenticationRequestParams(IOwinContext context)
        {
            _context = context;

            this.scope = context.Request.Query["scope"];
            this.response_type = context.Request.Query["response_type"];
            this.client_id = context.Request.Query["client_id"];
            this.redirect_uri = context.Request.Query["redirect_uri"];
            this.state = context.Request.Query["state"];
            this.prompt = context.Request.Query["prompt"];

            ValidateQueryString();
        }

        public bool IsLoginPrompt()
        {
            return this.prompt.Equals("login");
        }

        private void ValidateQueryString()
        {
            if (!_context.Request.QueryString.HasValue)
                throw new AuthenticationRequestException("invalid_request_uri", "request uri is required");

            RequestNullAssertation(scope, "scope is required");
            RequestNullAssertation(response_type, "response_type is required");
            RequestNullAssertation(client_id, "client_id is required");
            RequestNullAssertation(response_type, "response_type is required");

            RequestHasValueAssertation(scope, "openid", "'openid' value no present");
        }

        private void RequestNullAssertation(string key, string message)
        {
            if (key == null) throw new AuthenticationRequestException("invalid_request", message);
        }

        private void RequestHasValueAssertation(string key, string value, string message)
        {
            if (!key.Contains(value)) throw new AuthenticationRequestException("invalid_request", message);
        }
    }
}

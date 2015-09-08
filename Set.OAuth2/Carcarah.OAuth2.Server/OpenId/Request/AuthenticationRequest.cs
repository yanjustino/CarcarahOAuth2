using Microsoft.Owin;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Request
{
    public class AuthenticationRequest: RequestBase
    {
        public string scope  => this["scope"];
        public string response_type => this["response_type"];
        public string client_id => this["client_id"];
        public string redirect_uri => this["redirect_uri"];
        public string state => this["state"];
        public string response_mode => this["response_mode"];
        public string nonce => this["nonce"];
        public string display => this["display"];
        public string prompt => this["prompt"];
        public string max_age => this["max_age"];
        public string ui_locales => this["ui_locales"];
        public string id_token_hint => this["id_token_hint"];
        public string login_hint => this["login_hint"];
        public string acr_values => this["acr_values"];

        public async Task<string> username() => 
            await FindInForm("username");
        public async Task<string> password() =>
            await FindInForm("password");

        public AuthenticationRequest(IOwinContext context):base(context)
        {
            ValidateAuthenticationRequest();
        }

        public void ValidateAuthenticationRequest()
        {
            if (!Context.Request.QueryString.HasValue)
                throw new AuthenticationRequestException(
                    "invalid_request_uri",
                    "request uri is required");

            AuthenticationRequestAssertationConcern.IsNotNull(scope, "invalid_request_uri", "scope is required");
            AuthenticationRequestAssertationConcern.IsNotNull(prompt, "interaction_required", "prompt is required");
            AuthenticationRequestAssertationConcern.IsNotNull(response_type, "invalid_request_uri", "response_type is required");
            AuthenticationRequestAssertationConcern.IsNotNull(client_id, "invalid_request_uri", "client_id is required");
            AuthenticationRequestAssertationConcern.IsNotNull(redirect_uri, "request_uri_not_supported", "redirect_uri is required");

            AuthenticationRequestAssertationConcern.Contains(scope, "openid", "invalid_request_uri", "'openid' value no present");
        }


    }
}

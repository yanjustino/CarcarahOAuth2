using Microsoft.Owin;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Request
{
    public class AuthenticationRequest : RequestBase
    {
        public string scope { get; private set; }
        public string response_type { get; private set; }
        public string client_id { get; private set; }
        public string client_secret { get; private set; }
        public string redirect_uri { get; private set; }
        public string state { get; private set; }
        public string response_mode { get; private set; }
        public string nonce { get; private set; }
        public string display { get; private set; }
        public string prompt { get; private set; }
        public string max_age { get; private set; }
        public string ui_locales { get; private set; }
        public string id_token_hint { get; private set; }
        public string login_hint { get; private set; }
        public string acr_values { get; private set; }

        public async Task<string> username() =>
            await FindInForm("username");

        public async Task<string> password() =>
            await FindInForm("password");

        public AuthenticationRequest(IOwinContext context) : base(context)
        {
        }

        public async Task LoadAsync()
        {
            scope = await Find("scope");
            response_type = await Find("response_type");
            client_id = await Find("client_id");
            client_secret = await Find("client_secret");
            redirect_uri = await Find("redirect_uri");
            state = await Find("state");
            response_mode = await Find("response_mode");
            nonce = await Find("nonce");
            display = await Find("display");
            prompt = await Find("prompt");
            max_age = await Find("max_age");
            ui_locales = await Find("ui_locales");
            id_token_hint = await Find("id_token_hint");
            login_hint = await Find("login_hint");
            acr_values = await Find("acr_values");

            ValidateAuthenticationRequest();
        }

        public void ValidateAuthenticationRequest()
        {
            AuthenticationRequestAssertationConcern.IsNotNull(scope, "invalid_request_uri", "scope is required");
            AuthenticationRequestAssertationConcern.IsNotNull(prompt, "interaction_required", "prompt is required");
            AuthenticationRequestAssertationConcern.IsNotNull(response_type, "invalid_request_uri", "response_type is required");
            AuthenticationRequestAssertationConcern.IsNotNull(client_id, "invalid_request_uri", "client_id is required");
            AuthenticationRequestAssertationConcern.IsNotNull(redirect_uri, "request_uri_not_supported", "redirect_uri is required");

            AuthenticationRequestAssertationConcern.Contains(scope, "openid", "invalid_request_uri", "'openid' value no present");
        }
    }
}

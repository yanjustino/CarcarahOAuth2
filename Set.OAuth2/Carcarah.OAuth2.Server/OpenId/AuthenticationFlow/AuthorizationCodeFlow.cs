using Carcarah.OAuth2.Server.Helpers;
using Carcarah.OAuth2.Server.OpenId.AuthenticationFlow.Storage;
using Carcarah.OAuth2.Server.OpenId.Request;
using Carcarah.OAuth2.Server.OpenId.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow
{
    public class AuthorizationCodeFlow : Flow
    {
        private AuthorizationCodeStorage _authorizationCodestorage;
        private RefreshTokenStorage _refreshTokenStorage;

        public AuthorizationCodeFlow(OAuthContext context, 
            AuthorizationCodeStorage authCodeStorage, 
            RefreshTokenStorage refreshTokenStorage) : base(context)
        {
            _authorizationCodestorage = authCodeStorage;
            _refreshTokenStorage = refreshTokenStorage;
        }

        public override async Task AuthenticatesEndUser()
        {
            var prompt = Context.AuthenticationRequest.prompt;
            var username = await Context.AuthenticationRequest.username();
            var password = await Context.AuthenticationRequest.password();
            var isAuthorized = await Context.GrantResourceOwnerCredentials(username, password);

            if (!isAuthorized)
                throw new UnauthorizedException(prompt);
            else
            {
                var response = await CreateAuthenticationCode();

                if (prompt.Equals("none"))
                {
                    var json = response.Json();
                    Context.OwinConext.Authorized(json);
                }
                else
                {
                    var redirect = response.UrlEncoded(Context.AuthenticationRequest.redirect_uri);
                    Context.OwinConext.Authorized(new Uri(redirect));
                }
            }
        }

        private async Task<SuccessfulAuthenticationResponse> CreateAuthenticationCode()
        {
            var authCode = (
                Guid.NewGuid().ToString("n") + 
                Guid.NewGuid().ToString("n")).ToSha256String();

            var code = new AuthorizationCode
            {
                Client = Context.CurrentClient,
                IsOpenId = Context.AuthenticationRequest.scope.Contains("openid"),
                Nonce = Context.AuthenticationRequest.nonce,
                RedirectUri = Context.AuthenticationRequest.redirect_uri,
                SubjectId = Context.SubjectId
            };

            await _authorizationCodestorage.StoreAsync(authCode, code);

            return new SuccessfulAuthenticationResponse(authCode, 
                Context.AuthenticationRequest.state);
        }
    }
}

using Carcarah.OAuth2.Server.Helpers;
using Carcarah.OAuth2.Server.OpenId.AuthenticationFlow.Storage;
using Carcarah.OAuth2.Server.OpenId.Request;
using Carcarah.OAuth2.Server.OpenId.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow
{
    public class TokenEndPoint
    {
        private OAuthContext _context;
        private AuthorizationCodeStorage _authorizationCodestorage;
        private RefreshTokenStorage _refreshTokenStorage;

        public TokenEndPoint(
            OAuthContext context,
            AuthorizationCodeStorage authCodeStorage,
            RefreshTokenStorage refreshTokenStorage)
        {
            _context = context;
            _authorizationCodestorage = authCodeStorage;
            _refreshTokenStorage = refreshTokenStorage;
        }

        public async Task TokenRequest()
        {
            var username = await _context.TokenRequest.username();
            var password = await _context.TokenRequest.password();
            var isAuthorized = await _context.GrantResourceOwnerCredentials(username, password);

            if (!isAuthorized)
                throw new UnauthorizedException();
            else
            {
                var authCode = await GetAuthorizationCode();
                var response = await CreateTokenIdResponse(authCode);

                _context.OwinConext.SuccessfulToken(response);
            }
        }

        private async Task<SuccessfulTokenResponse> CreateTokenIdResponse(AuthorizationCode authCode)
        {
            var tokenLifeTime = authCode.Client.IdentityTokenLifetime;
            var jti = Guid.NewGuid().ToString().ToSha256String();
            var exp = DateTimeOffsetHelper.NewExpirationDate(tokenLifeTime).ToString();
            var accessToken = authCode.SubjectId.ToSha256String();

            var payload = new Dictionary<string, object>
            {
                ["iss"] = "https://onauth.set.rn.gov.br",
                ["sub"] = authCode.SubjectId,
                ["aud"] = "https://onauth.set.rn.gov.br/token",
                ["jti"] = jti,
                ["exp"] = exp,
                ["iat"] = DateTimeOffsetHelper.NewExpirationDate(0),
                ["nonce"] = authCode.Nonce,
                ["at_hash"] = accessToken
            };

            var token = JWT.JsonWebToken.Encode(payload,
                        authCode.Client.ClientSecrets[0],
                        JWT.JwtHashAlgorithm.HS256);

            var key = (Guid.NewGuid().ToString("n") +
                       Guid.NewGuid().ToString("n")).ToSha256String();

            var value = new RefreshToken
            {
                SubjectId = authCode.SubjectId,
                ClientId = authCode.ClientId,
                Client = authCode.Client
            };

            await _refreshTokenStorage.StoreAsync(key, value);

            return new SuccessfulTokenResponse
            {
                access_token = accessToken,
                expires_in = exp,
                refresh_token = key,
                id_token = token
            };
        }

        private async Task<AuthorizationCode> GetAuthorizationCode()
        {
            var code = await _authorizationCodestorage.GetAsync(await _context.TokenRequest.code());

            if (code == null ||
                code.RedirectUri != await _context.TokenRequest.redirect_uri() ||
                code.SubjectId != _context.SubjectId || !code.IsOpenId)
            {
                throw new UnauthorizedException("invalid_request", "invalid_request");
            }

            return code;
        }

    }
}

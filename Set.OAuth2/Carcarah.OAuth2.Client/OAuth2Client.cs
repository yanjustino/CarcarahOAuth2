﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Client
{
    public class OAuth2Client
    {
        public async Task<SuccessfulTokenResponse> AuthorizationGrantAsync(AuthorizationGrantRequest request)
        {
            var authCode = await GetAuthorizationCode(request);
            return await GetTokenId(request, authCode.Code);
        }

        private async Task<SuccessfulAuthenticationResponse> GetAuthorizationCode(AuthorizationGrantRequest request)
        {
            var url = request.Uri;

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("scope", "openid"),
                new KeyValuePair<string, string>("response_type", "code"),
                new KeyValuePair<string, string>("client_id", request.ClientId),
                new KeyValuePair<string, string>("client_secret", request.ClientSecret),
                new KeyValuePair<string, string>("prompt", "none"),
                new KeyValuePair<string, string>("redirect_uri", request.RedirectUri),
                new KeyValuePair<string, string>("username", request.Username),
                new KeyValuePair<string, string>("password", request.Password),
            });

            var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = JsonConvert
                    .DeserializeObject<AuthenticationErrorResponse>
                    (content);

                throw new Exception(error.error + ": " + error.error_description);
            }
            else
            {
                return JsonConvert
                   .DeserializeObject<SuccessfulAuthenticationResponse>
                   (content);
            }
        }

        private async Task<SuccessfulTokenResponse> GetTokenId(AuthorizationGrantRequest request, string code)
        {
            var client = new HttpClient();
            var url = $"{request.Uri}token";

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", request.Username),
                new KeyValuePair<string, string>("password", request.Password),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", request.RedirectUri)
            });

            var response = await client.PostAsync(url, data);
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var error = JsonConvert
                    .DeserializeObject<AuthenticationErrorResponse>
                    (content);

                throw new Exception(error.error + ": " + error.error_description);
            }
            else
            {
                return JsonConvert
                   .DeserializeObject<SuccessfulTokenResponse>
                   (content);
            }
        }
    }
}

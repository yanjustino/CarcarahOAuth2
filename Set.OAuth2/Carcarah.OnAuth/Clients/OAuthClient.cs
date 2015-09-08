using Carcarah.OnAuth.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Carcarah.OnAuth.Clients
{
    public class OAuthClient
    {
        public Uri Uri { get; }
        public string ClientId { get; }
        public string SecretKey { get; }

        public OAuthClient(Uri uri, string clientId, string secretKey)
        {
            Uri = uri;
            ClientId = clientId;
            SecretKey = secretKey;
        }

        public string RequestClientCredentialsAsync(string scope, string redirect_uri)
        {
            return $"{Uri.AbsoluteUri}?" +
                   $"response_type=code&scope=openid {scope}&" +
                   $"client_id={ClientId}&" +
                   $"redirect_uri={redirect_uri}";
        }

        public string TokenRequest(HttpRequestBase request, string redirect_uri)
        {
            var code = request.QueryString["code"];

            string query = $"code={code}&" +
                "grant_type=authorization_code&" +
                $"redirect_uri={redirect_uri}";

            var wc = new WebClient();

            wc.Headers[HttpRequestHeader.ContentType] =
                "application/x-www-form-urlencoded";

            wc.Headers.Add(HttpRequestHeader.Cookie,
                CookieRepository.AUTHORIZATION_CODE + "=" +
                request.Cookies[CookieRepository.AUTHORIZATION_CODE].Value);

            wc.Headers.Add(HttpRequestHeader.Cookie,
                CookieRepository.CLIENT_SECRET_JWT + "=" +
                request.Cookies[CookieRepository.CLIENT_SECRET_JWT].Value);

            request.Cookies.Clear();

            return wc.UploadString(Uri.AbsoluteUri + "token", query);
        }

        public string RequestResourceOwnerPasswordAsync(string username, string password, string scope, string redirect_uri)
        {
            var uri = $"{Uri.AbsoluteUri}?" +
                      $"response_type=code&scope=openid {scope}&" +
                      $"client_id={ClientId}&" +
                      $"redirect_uri={redirect_uri}";

            string query = $"username={username}&password={password}";

            var wc = new WebClient();

            wc.Headers[HttpRequestHeader.ContentType] =
                "application/x-www-form-urlencoded";

            var u = wc.UploadString(uri, query);
            return u;
        }
    }
}

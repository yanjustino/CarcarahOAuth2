using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Response
{
    public class SuccessfulTokenResponse : ResponseBase
    {
        public string access_token { get; set; }
        public string token_type => "Bearer";
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string id_token { get; set; }

        protected override Dictionary<string, string> GenerateDictionaryFromProperties()
        {
            return new Dictionary<string, string>
            {
                ["access_token"] = access_token,
                ["token_type"] = token_type,
                ["refresh_token"] = refresh_token,
                ["expires_in"] = expires_in,
                ["id_token"] = id_token,
            };
        }
    }
}

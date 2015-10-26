using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Response
{
    public class AuthenticationErrorResponse : ResponseBase
    {
        public string error { get; set; }
        public string state { get; set; }
        public string error_description { get; set; }

        public AuthenticationErrorResponse(string error, string error_description, string state)
        {
            this.error = error;
            this.error_description = error_description;
            this.state = state;
        }

        protected override Dictionary<string, string> GenerateDictionaryFromProperties()
        {
            return new Dictionary<string, string>
            {
                ["error"] = error,
                ["error_description"] = error_description,
                ["state"] = state
            };
        }
    }
}

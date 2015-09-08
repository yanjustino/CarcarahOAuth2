using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Response
{
    public class TokenErrorResponse : ResponseBase
    {
        public string error { get; set; }

        protected override Dictionary<string, string> GenerateDictionaryFromProperties()
        {
            return new Dictionary<string, string>
            {
                ["error"] = error
            };
        }
    }
}

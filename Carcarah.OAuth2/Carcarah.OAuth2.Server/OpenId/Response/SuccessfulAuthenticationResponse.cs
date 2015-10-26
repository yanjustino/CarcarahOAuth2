using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.Response
{
    public class SuccessfulAuthenticationResponse : ResponseBase
    {
        public string Code { get; set; }
        public string State { get; set; }

        public SuccessfulAuthenticationResponse(string code, string state)
        {
            Code = code;
            State = state;
        }

        protected override Dictionary<string, string> GenerateDictionaryFromProperties()
        {
            return new Dictionary<string, string>
            {
                ["code"] = Code,
                ["state"] = State
            };
        }
    }
}

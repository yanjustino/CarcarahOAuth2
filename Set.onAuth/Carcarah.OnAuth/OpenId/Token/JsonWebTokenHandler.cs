using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.Token
{
    public class JsonWebTokenHandler
    {
        public string GenerateAuthorizationCode(string clientId, string secretKey, string userID)
        {
            var payload = new Dictionary<string, object>
            {
                ["client_id"] = clientId,
                ["user_id"] = userID,
                ["code_uid"] = Guid.NewGuid().ToString(),
                ["exp"] = DateTime.Now.AddMinutes(5).Ticks
            };

            return JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
        }
    }
}

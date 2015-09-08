using Carcarah.OAuth2.Server.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow.Storage
{
    public class RefreshToken
    {
        public DateTimeOffset CreationTime { get; set; }
        public string SubjectId { get; set; }
        public Client Client { get; set; }
        public string ClientId { get; set; }

        public RefreshToken()
        {
            CreationTime = DateTimeOffsetHelper.UtcNow;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Client
{
    [Serializable]
    public class AuthenticationErrorResponse
    {
        public string error { get; set; }
        public string state { get; set; }
        public string error_description { get; set; }
    }

}

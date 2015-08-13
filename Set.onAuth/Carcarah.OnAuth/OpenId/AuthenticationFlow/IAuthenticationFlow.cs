using Carcarah.OnAuth.OpenId.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OnAuth.OpenId.AuthenticationFlow
{
    public interface IAuthenticationFlow
    {
        Task AuthorizeEndUser();
    }
}

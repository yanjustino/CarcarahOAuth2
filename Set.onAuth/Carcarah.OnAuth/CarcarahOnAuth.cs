using System;
using System.Linq;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carcarah.OnAuth
{
    using OpenId.Request;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class CarcarahOnAuth
    {
        private AppFunc _next;
        private IOwinContext _context;
        private CarcarahOnAuthOptions _options;
        private AuthenticationRequest _request;

        public CarcarahOnAuth(AppFunc next, CarcarahOnAuthOptions options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            try
            {

                InitializeOwinContext(environment);

                if (IsAuthorizationEndPoint())
                    await _next.Invoke(environment);
                else
                    await AuthorizeRequest(environment);
            }
            catch (AuthenticationRequestException ex)
            {
                _context.BadRequest(ex.Message);
            }
            catch
            {
                _context.InternalServerError();
            }
        }

        private void InitializeOwinContext(IDictionary<string, object> environment)
        {
            _context = new OwinContext(environment);
            _options.AuthorizationProvider.Context = _context;
            _request = new AuthenticationRequest(_context);
        }

        private bool IsAuthorizationEndPoint()
        {
            return _context.Request.Path == _options.AuthorizationEndpoint;
        }

        private async Task AuthorizeRequest(IDictionary<string, object> environment)
        {
            var user = await _request.Body.FindUserName();
            var pass = await _request.Body.FindPassword();

            var IsAuthorized = await _options.AuthorizationProvider.ValidateClientAuthentication() ||
                               await _options.AuthorizationProvider.GrantResourceOwnerCredentials(user, pass);

            if (!IsAuthorized)
                _context.Unauthorized(_options);
            else
            {
                _context.Authorized();
                await _next.Invoke(environment);
            }
        }
    }
}

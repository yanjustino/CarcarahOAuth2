using System;
using System.Linq;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carcarah.OnAuth
{
    using OpenId;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class CarcarahOnAuthMiddleware
    {
        private AppFunc _next;
        private IOwinContext _context;
        private Config.CarcarahMiddlewareConfigOptions _config;
        private OpenId.AuthenticationRequestHandler _autenticationRequest;

        public CarcarahOnAuthMiddleware(AppFunc next, Config.CarcarahMiddlewareConfigOptions config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            try
            {
                InitializeOwinContext(environment);
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
            _config.Provider.Context = _context;
            _autenticationRequest = new OpenId.AuthenticationRequestHandler(_context);
        }

        private async Task AuthorizeRequest(IDictionary<string, object> environment)
        {
            if (!await _config.Provider.IsAuthenticated())
                _context.Deny();
            else
            {
                _context.Authorize();
                await _next.Invoke(environment);
            }
        }
    }
}

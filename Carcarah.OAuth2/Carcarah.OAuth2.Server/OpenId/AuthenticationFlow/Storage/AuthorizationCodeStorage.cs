using Carcarah.OAuth2.Server.OpenId.AuthenticationFlow;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow.Storage
{
    public class AuthorizationCodeStorage
    {
        private readonly ConcurrentDictionary<string, AuthorizationCode> _repository = 
            new ConcurrentDictionary<string, AuthorizationCode>();

        public Task StoreAsync(string key, AuthorizationCode value)
        {
            _repository[key] = value;

            return Task.FromResult<object>(null);
        }

        public Task<AuthorizationCode> GetAsync(string key)
        {
            AuthorizationCode code;
            if (_repository.TryGetValue(key, out code))
            {
                return Task.FromResult(code);
            }

            return Task.FromResult<AuthorizationCode>(null);
        }

        public Task RemoveAsync(string key)
        {
            AuthorizationCode val;
            _repository.TryRemove(key, out val);

            return Task.FromResult<object>(null);
        }

        public Task RevokeAsync(string subject, string client)
        {
            var query =
                from item in _repository
                where item.Value.SubjectId == subject && item.Value.ClientId == client
                select item.Key;

            foreach (var key in query)
            {
                RemoveAsync(key);
            }

            return Task.FromResult(0);
        }
    }
}

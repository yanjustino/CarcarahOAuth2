using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carcarah.OAuth2.Server.OpenId.AuthenticationFlow.Storage
{
    public class RefreshTokenStorage
    {
        private readonly ConcurrentDictionary<string, RefreshToken> _repository =
            new ConcurrentDictionary<string, RefreshToken>();

        public Task StoreAsync(string key, RefreshToken value)
        {
            _repository[key] = value;

            return Task.FromResult<object>(null);
        }

        public Task<RefreshToken> GetAsync(string key)
        {
            RefreshToken code;
            if (_repository.TryGetValue(key, out code))
            {
                return Task.FromResult(code);
            }

            return Task.FromResult<RefreshToken>(null);
        }

        public Task RemoveAsync(string key)
        {
            RefreshToken val;
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

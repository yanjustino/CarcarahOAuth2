using System.Collections.Generic;
using System.Linq;

namespace Carcarah.OAuth2.Server.OpenId.Response
{
    public abstract class ResponseBase
    {
        protected abstract Dictionary<string, string> GenerateDictionaryFromProperties();

        public string Json()
        {
            var dic = GenerateDictionaryFromProperties();
            return FromDictionaryToJson(dic);
        }

        public string UrlEncoded(string url)
        {
            var dic = GenerateDictionaryFromProperties();
            return FromDictionaryToUrlEncoded(dic, url);
        }

        private string FromDictionaryToJson(Dictionary<string, string> dictionary)
        {
            var dic = dictionary.Where(x => !string.IsNullOrEmpty(x.Value));

            var kvs = dic.Select(kvp => string.Format("\"{0}\":\"{1}\"", kvp.Key, string.Join(",", kvp.Value)));
            return string.Concat("{", string.Join(",", kvs), "}");
        }

        private string FromDictionaryToUrlEncoded(Dictionary<string, string> dictionary, string url)
        {
            var dic = dictionary.Where(x => !string.IsNullOrEmpty(x.Value));

            var kvs = dic.Select(kvp => $"{kvp.Key}={string.Join("&", kvp.Value)}");
            return string.Concat(url, "?", string.Join("&", kvs));
        }
    }
}

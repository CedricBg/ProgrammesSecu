using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammesSecu.Helpers
{
    public class BearerToken
    {
        private string _authorizationkey;
        HttpClient _httpClient;

        public BearerToken(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpClient> GetClient()
        {
            _authorizationkey = await SecureStorage.GetAsync("token");
            if ((_authorizationkey != null) && (!(_httpClient.DefaultRequestHeaders.Contains("Authorization"))))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _authorizationkey);
                _httpClient.DefaultRequestHeaders.Add("Accept", "Application/Json");
            }
            return _httpClient;
        }
    }
}

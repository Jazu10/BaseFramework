using Frontend.DTO.Response.Common;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace Frontend.Core.HttpClients
{
    public class GenericHttpClient : IGenericHttpClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private string _username;
        private string _password;
        public GenericHttpClient(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _client.BaseAddress = new Uri(_configuration.GetSection("ClientUrl").Value);
            _username = _configuration.GetValue<string>("ClientId");
            _password = _configuration.GetValue<string>("ClientPassword");

        }

        public async Task DeleteAsync(string address)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, address);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    //var result = await response.Content.ReadAsStringAsync();
                    //return JsonConvert.DeserializeObject<TResponse>(result);                 }
                }
                var errResult = await response.Content.ReadAsStringAsync();
                throw new Exception(errResult);
            }
        }

        public async Task<Result<List<TResponse>>> GetAllAsync<TResponse>(string address)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, address);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            //var authKey = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}"));
            //_client.DefaultRequestHeaders.Add("Authorization", $"Basic {authKey}");
            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Result<List<TResponse>>>(result);
                }
                var errResult = await response.Content.ReadAsStringAsync();
                throw new Exception(errResult);
            }
        }

        public async Task<Result<TResponse>> GetAsync<TResponse>(string address)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, address);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Result<TResponse>>(result);
                }
                var errResult = await response.Content.ReadAsStringAsync();
                throw new Exception(errResult);
            }
        }

        public async Task<Result<TResponse>> PostAsync<TResponse>(string address, dynamic dynamicObject)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, address);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Content = new StringContent(JsonConvert.SerializeObject(dynamicObject));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Result<TResponse>>(result);
                }
                var errResult = await response.Content.ReadAsStringAsync();
                throw new Exception(errResult);
            }
        }

        public async Task<Result<TResponse>> PutAsync<TResponse>(string address, dynamic dynamicObject)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, address);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            request.Content = new StringContent(JsonConvert.SerializeObject(dynamicObject));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            var token = await GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Result<TResponse>>(result);
                }
                var errResult = await response.Content.ReadAsStringAsync();
                throw new Exception(errResult);
            }
        }

        public async Task<string> GetToken()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("UserName", _username);
            dict.Add("Password", _password);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/token");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            request.Content = new StringContent(JsonConvert.SerializeObject(dict));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<string>(result);
                }
                var errResult = await response.Content.ReadAsStringAsync();
                throw new Exception(errResult);
            }
        }

        public class ApiException : Exception
        {
            public int StatusCode { get; set; }
            public string ErrorMessage { get; set; }
            public string ? InnerException { get; set; }
            public string ? ErrorType { get; set; }
        }

    }
}

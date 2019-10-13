using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        private readonly HttpClient _client;
        public CustomHttpClient()
        {
            _client = new HttpClient();
        }
        public Task<HttpResponseMessage> DeleteAsync<T>(string uri, string autherizationToken = null, string autherizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetStringAsync(string uri, string autherizationToken = null, string autherizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            if (autherizationToken != null)
            {
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(autherizationMethod, autherizationToken);

            }
            var response = await _client.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        //Http request to Post T item
        // for example to create an event.
        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string autherizationToken = null, string autherizationMethod = "Bearer")
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                var json = JsonConvert.SerializeObject(item);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await _client
                        .SendAsync(request))
                    {
                        return response;
                        
                    }
                }
            }
        }

        public Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string autherizationToken = null, string autherizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
    }
}

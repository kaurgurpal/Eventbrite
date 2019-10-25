using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<HttpResponseMessage> DeleteAsync(string uri, string autherizationToken = null, string autherizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);
            if (autherizationToken != null)
            {

                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(autherizationMethod, autherizationToken);

            }

            return await _client.SendAsync(requestMessage);
        }

        public async Task<string> GetStringAsync(string uri, string autherizationToken = null, string autherizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            if (autherizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(autherizationMethod, autherizationToken);

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

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string autherizationToken = null, string autherizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item, autherizationToken, autherizationMethod);
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string autherizationToken, string autherizationMethod)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            // a new StringContent must be created for each retry 
            // as it is disposed after each call

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
            //  SetAuthorizationHeader(requestMessage);
            if (autherizationToken != null)
            {


                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(autherizationMethod, autherizationToken);

            }


            //if (requestId != null)
            //{
            //    requestMessage.Headers.Add("x-requestid", requestId);
            //}

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500 
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        
    }
}

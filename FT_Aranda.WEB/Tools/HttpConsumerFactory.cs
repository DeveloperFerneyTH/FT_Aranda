using FT_Aranda.WEB.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FT_Aranda.WEB.Tools
{
    public class HttpConsumerFactory : IHttpConsumerFactory
    {
        // Globals
        private readonly Uri baseUrl;
        private readonly WebSettings settings;

        public HttpConsumerFactory(IOptions<WebSettings> options)
        {
            // Dependency injection
            settings = options.Value;

            baseUrl = new Uri(settings.BaseUrlAPI);
        }

        #region Methods

        public async Task<HttpResponseMessage> GetAsync(string nameApiMethod)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = baseUrl;
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var consumer = httpClient.GetAsync(nameApiMethod);
                consumer.Wait();

                return consumer.Result;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(string nameApiMethod, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = baseUrl;
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
                var consumer = httpClient.GetAsync(nameApiMethod);
                consumer.Wait();

                return consumer.Result;
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string nameApiMethod, T dataSend)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = baseUrl;
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var consumer = httpClient.PostAsync(nameApiMethod, CreateHttpContent<T>(dataSend));
                consumer.Wait();

                return consumer.Result;
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string nameApiMethod, T dataSend, string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = baseUrl;
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var consumer = httpClient.PostAsync(nameApiMethod, CreateHttpContent<T>(dataSend));
                consumer.Wait();

                return consumer.Result;
            }
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        #endregion
    }
}

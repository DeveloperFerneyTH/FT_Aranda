using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FT_Aranda.WEB.Tools
{
    public interface IHttpConsumerFactory
    {
        Task<HttpResponseMessage> GetAsync(string nameApiMethod);
        Task<HttpResponseMessage> GetAsync(string nameApiMethod, string token);
        Task<HttpResponseMessage> PostAsync<T>(string nameApiMethod, T dataSend);
        Task<HttpResponseMessage> PostAsync<T>(string nameApiMethod, T dataSend, string token);
    }
}

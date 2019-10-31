using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Http.Extensions;
using WeiXinOpenPlatForm.Http.Settings;

namespace WeiXinOpenPlatForm.Service.Http
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 初始化 <see cref="HttpClientService"/> 类的一个新实例
        /// </summary>
        /// <param name="httpClientFactory"><see cref="HttpClient"/> 工厂，用于创建 <see cref="HttpClient"/></param>
        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResponse> PostAsJsonAsync<TResponse>(HttpClientPriority priority, string requestUri, object value, AuthenticationHeaderValue authorization = null)
        {
            var name = Enum.GetName(typeof(HttpClientPriority), priority);
            return await PostAsJsonAsync<TResponse>(name, requestUri, value, authorization);
        }

        public async Task<TResponse> PostAsJsonAsync<TResponse>(string name, string requestUri, object value, AuthenticationHeaderValue authorization = null)
        {
            var client = _httpClientFactory.CreateClient(name);
            return await client.PostAsJsonAsync<TResponse>(requestUri, value, authorization);
        }
    }
}

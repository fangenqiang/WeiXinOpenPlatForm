using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Http.Settings;

namespace WeiXinOpenPlatForm.Service.Http
{
    public interface IHttpClientService : IDenpendency
    {
        Task<TResponse> PostAsJsonAsync<TResponse>(HttpClientPriority priority,
    string requestUri, object value, AuthenticationHeaderValue authorization = null);

        Task<TResponse> PostAsJsonAsync<TResponse>(string name, string requestUri, object value,
    AuthenticationHeaderValue authorization = null);
    }
}

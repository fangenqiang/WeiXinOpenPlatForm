using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Logs;

namespace WeiXinOpenPlatForm.Http.Extensions
{
    public static class HttpClientExtensions
    {
        private static readonly ILog _log = LogManager.GetLogger("NETCoreRepository", typeof(HttpClientExtensions));

        private static void Info(Guid requestId, string httpMethod, string baseAddress, string requestUri, byte direction, string content)
        {
            var log = new RequestLog
            {
                RequestId = requestId,
                HttpMethod = httpMethod,
                BaseAddress = baseAddress,
                RequestUri = requestUri,
                Direction = direction,
                Content = content
            };
            _log.Info(log);
        }

        private static void Error(Guid requestId, string httpMethod, string baseAddress, string requestUri, byte direction, Exception exception)
        {
            var log = new RequestLog
            {
                RequestId = requestId,
                HttpMethod = httpMethod,
                BaseAddress = baseAddress,
                RequestUri = requestUri,
                Direction = direction
            };
            _log.Error(log, exception);
        }
        /// <summary>
        /// 以异步的方式将 POST 请求发送到指定的 URI
        /// </summary>
        /// <typeparam name="TResponse">返回结果类型</typeparam>
        /// <param name="client">当前 <see cref="HttpClient"/></param>
        /// <param name="requestUri">请求地址</param>
        /// <param name="value">请求参数</param>
        /// <param name="authorization">身份验证信息</param>
        /// <returns>返回结果</returns>
        public static async Task<TResponse> PostAsJsonAsync<TResponse>(this HttpClient client,
            string requestUri, object value, AuthenticationHeaderValue authorization)
        {
            var contentTypes = "application/json";
            var content = JsonConvert.SerializeObject(value);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(content, Encoding.UTF8, contentTypes)
            };
            if (authorization != null)
            {
                request.Headers.Authorization = authorization;
            }
            //记录请求日志
            var requestId = Guid.NewGuid();
            Info(requestId, request.Method.Method, $"{client.BaseAddress}", requestUri, 0, content);
            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                //记录错误日志
                Error(requestId, request.Method.Method, $"{client.BaseAddress}", requestUri, 1, ex);
                throw;
            }
            var json = await response.Content.ReadAsStringAsync();
            //记录返回日志
            Info(requestId, request.Method.Method, $"{client.BaseAddress}", requestUri, 1, json);
            try
            {
                return JsonConvert.DeserializeObject<TResponse>(json);
            }
            catch (Exception ex)
            {
                Error(requestId, request.Method.Method, $"{client.BaseAddress}", requestUri, 1, ex);
                throw;
            }

        }
    }
}

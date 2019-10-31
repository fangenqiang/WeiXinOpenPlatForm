using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Config;

namespace WeiXinOpenPlatForm.Http.Handlers
{
    /// <summary>
    /// 极速数据接口handler
    /// </summary>
    public class JiSuApiHandler : DelegatingHandler
    {
        private readonly JiSuApiConfig _jiSuApiConfig;
        public JiSuApiHandler(IConfiguration configuration)
        {
            _jiSuApiConfig = configuration.GetSection(nameof(JiSuApiConfig))?.Get<JiSuApiConfig>();
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var content = await request.Content.ReadAsStringAsync();
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            dict.Add("appkey", _jiSuApiConfig.Appkey);
            string urlParam = GetParamSrc(dict);
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri($"{request.RequestUri}?{urlParam}");
            var result = await base.SendAsync(request, cancellationToken);
            return result;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="paramsMap"></param>
        /// <returns></returns>
        public static string GetParamSrc(Dictionary<string, object> paramsMap)
        {
            var builder = new StringBuilder();
            foreach (var para in paramsMap.Where(e => e.Value != null).OrderBy(b => b.Key))
            {
                builder.AppendFormat("{0}={1}&", para.Key, para.Value);
            }
            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }
    }
}

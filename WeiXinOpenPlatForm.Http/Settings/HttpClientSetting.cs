using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Http.Settings
{
    /// <summary>
    /// 用于存储 <see cref="System.Net.Http.HttpClient"/> 配置
    /// </summary>
    public class HttpClientSetting
    {
        /// <summary>
        /// <see cref="System.Net.Http.HttpClient"/> 请求的关键程度
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// HTTP 请求基地址
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// HTTP 处理程序集合
        /// </summary>
        public List<string> DelegatingHandlers { get; set; }

        /// <summary>
        /// HTTP 处理程序生存期，单位为分钟，如果不指定则 HTTP 处理程序生存期为两分钟
        /// </summary>
        public int? HandlerLifetime { get; set; }

        /// <summary>
        /// HTTP 错误处理政策集合
        /// </summary>
        public List<string> ErrorPolicies { get; set; }
    }
}

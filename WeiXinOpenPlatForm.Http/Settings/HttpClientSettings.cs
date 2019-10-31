using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Http.Settings
{
    /// <summary>
    /// 用于存储 <see cref="System.Net.Http.HttpClient"/> 配置集合
    /// </summary>
    public class HttpClientSettings
    {
        /// <summary>
        /// 用于指示 <see cref="System.Net.Http.HttpClient"/> 请求的关键程度的枚举类型
        /// </summary>
        public string PriorityType { get; set; }

        /// <summary>
        /// HTTP 请求基地址键值对集合
        /// </summary>
        public Dictionary<string, string> BaseAddresses { get; set; }

        /// <summary>
        /// HTTP 处理程序键值对集合
        /// </summary>
        public Dictionary<string, string> DelegatingHandlers { get; set; }

        /// <summary>
        /// HTTP 错误重试处理政策的键值对集合
        /// </summary>
        public Dictionary<string, RetryPolicy> RetryPolicies { get; set; }

        /// <summary>
        /// HTTP 错误等待后重试处理政策的键值对集合
        /// </summary>
        public Dictionary<string, WaitAndRetryPolicy> WaitAndRetryPolicies { get; set; }

        /// <summary>
        /// HTTP 错误断路处理政策的键值对集合
        /// </summary>
        public Dictionary<string, CircuitBreakerPolicy> CircuitBreakerPolicies { get; set; }

        /// <summary>
        /// <see cref="System.Net.Http.HttpClient"/> 配置集合
        /// </summary>
        public List<HttpClientSetting> Settings { get; set; }
    }
}

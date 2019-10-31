using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Http.Settings
{
    /// <summary>
    /// 用于存储 HTTP 错误断路处理的政策参数
    /// </summary>
    public class CircuitBreakerPolicy
    {
        /// <summary>
        /// 断路前允许尝试的次数
        /// </summary>
        public int HandledEventsAllowedBeforeBreaking { get; set; }

        /// <summary>
        /// 断路时间，单位为毫秒
        /// </summary>
        public int DurationOfBreak { get; set; }
    }
}

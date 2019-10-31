using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Http.Settings
{
    /// <summary>
    /// 用于存储 HTTP 错误重试处理的政策参数
    /// </summary>
    public class RetryPolicy
    {
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }
    }
}

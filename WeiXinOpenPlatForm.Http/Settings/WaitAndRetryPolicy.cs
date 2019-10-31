using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Http.Settings
{
    /// <summary>
    /// 用于存储 HTTP 错误等待后重试处理的政策参数
    /// </summary>
    public class WaitAndRetryPolicy
    {
        /// <summary>
        /// 等待重试的时间间隔，单位为毫秒
        /// </summary>
        public int[] SleepDurations { get; set; }
    }
}

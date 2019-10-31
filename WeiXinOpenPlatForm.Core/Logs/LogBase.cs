using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Core.Logs
{
    public class LogBase
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message { get; set; }
    }
}

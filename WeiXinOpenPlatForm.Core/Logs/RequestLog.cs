using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Core.Logs
{
    public class RequestLog : LogBase
    {
        /// <summary>
        /// 请求 ID
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 基地址
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUri { get; set; }

        /// <summary>
        /// 方向（0：请求；1：返回）
        /// </summary>
        public byte Direction { get; set; }
    }
}

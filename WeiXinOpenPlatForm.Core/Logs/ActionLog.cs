using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Core.Logs
{
    public class ActionLog : LogBase
    {
        /// <summary>
        /// API 版本
        /// </summary>
        public string ApiVersion { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 操作 ID
        /// </summary>
        public string ActionId { get; set; }

        /// <summary>
        /// 操作路径
        /// </summary>
        public string ActionPath { get; set; }

        /// <summary>
        /// 操作类型（1：调用前；2：调用中；3：调用后；4：异常）
        /// </summary>
        public byte ActionType { get; set; }

        /// <summary>
        /// 操作运行时间（单位为 100 毫微秒，即万分之一秒）
        /// </summary>
        public long ActionElapsed { get; set; }
    }
}

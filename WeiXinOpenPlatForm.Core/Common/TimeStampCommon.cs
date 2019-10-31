using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Core.Common
{
    /// <summary>
    /// 时间戳
    /// </summary>
    public static class TimeStampCommon
    {

        /// <summary>
        /// 生成时间戳 
        /// </summary>
        /// <returns>当前时间减去 1970-01-01 00.00.00 得到的毫秒数</returns>
        public static string GetTimeStamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

    }
}

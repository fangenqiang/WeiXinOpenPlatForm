using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Logs;

namespace WeiXinOpenPlatForm.Web.Extensions
{
    public static class ContextExtensions
    {
        /// <summary>
        /// 基于当前 <see cref="HttpContext"/> 创建 <see cref="OpenPlatformApiActionLog"/>
        /// </summary>
        /// <param name="context">当前 <see cref="HttpContext"/></param>
        /// <param name="actionType">操作类型（1：调用前；2：调用中；3：调用后；4：异常）</param>
        /// <param name="content">日志内容</param>
        /// <returns>基于当前 <see cref="HttpContext"/> 创建的 <see cref="OpenPlatformApiActionLog"/></returns>
        public static ActionLog CreateActionLog(this HttpContext context, byte actionType, string content)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return new ActionLog
            {
                ApiVersion = "1.0", // TODO 基于 HttpContext 获取版本号
                ActionId = context.Items.ContainsKey("ActionId") ? $"{context.Items["ActionId"]}" : string.Empty,
                Ip = $"{context.Connection.RemoteIpAddress}",
                ActionPath = context.Request.Path,
                ActionType = actionType,
                ActionElapsed = context.Items.ContainsKey("ActionElapsed") && context.Items["ActionElapsed"] is long l
                    ? l
                    : 0,
                Content = content
            };
        }
    }
}

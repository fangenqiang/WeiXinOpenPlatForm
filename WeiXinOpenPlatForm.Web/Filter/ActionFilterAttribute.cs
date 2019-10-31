using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Common;
using WeiXinOpenPlatForm.Web.Extensions;

namespace WeiXinOpenPlatForm.Web.Filter
{
    /// <summary>
    /// 用于记录操作日志及验证参数
    /// </summary>
    public class WeiXinActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ILog _log = LogManager.GetLogger(Startup.repository.Name,typeof(WeiXinActionFilterAttribute));

        ///  <summary>
        /// 在调用操作方法之前发生
        /// </summary>
        /// <param name="context">操作上下文</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        
        {
            context.HttpContext.Items["ActionId"] = $"{Guid.NewGuid()}";
            context.HttpContext.Items["Ticks"] = DateTime.Now.Ticks;

            // 验证参数是否为空
            if (context.ActionArguments != null && context.ActionArguments.Values.Any(v => v == null))
            {
                var log = context.HttpContext.CreateActionLog(
                    1, $"参数不能为空|{JsonConvert.SerializeObject(context.ActionArguments)}");
                _log.Warn(log);
                context.Result = new JsonResult(ApiResult<string>.CreateBadRequestResult("参数不能为空"));
                return;
            }

            // 验证参数是否合法
            if (!context.ModelState.IsValid)
            {
                var msg = string.Join(";",
                    context.ModelState.Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors).Select(e =>
                            string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception.Message : e.ErrorMessage));

                var log = context.HttpContext.CreateActionLog(
                    1, $"参数不合法|{JsonConvert.SerializeObject(context.ActionArguments)}|{msg}");
                _log.Warn(log);

                context.Result =
                    new JsonResult(ApiResult<string>.CreateBadRequestResult("参数不合法", "400", msg));
                return;
            }
            _log.Info(context.HttpContext.CreateActionLog(1, JsonConvert.SerializeObject(context.ActionArguments)));
            _log.Info(JsonConvert.SerializeObject(context.HttpContext.CreateActionLog(1, context.HttpContext.Request.QueryString.ToString())));
        }

        /// <summary>
        /// 在调用操作方法之后发生
        /// </summary>
        /// <param name="context">操作上下文</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Items.ContainsKey("Ticks") &&
                context.HttpContext.Items["Ticks"] is long l && l > 0)
            {
                context.HttpContext.Items["ActionElapsed"] = DateTime.Now.Ticks - l;
            }

            var content = context.Result is ObjectResult result
                ? JsonConvert.SerializeObject(result.Value)
                : JsonConvert.SerializeObject(context.Result);

            _log.Info(JsonConvert.SerializeObject(context.HttpContext.CreateActionLog(3, content)));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Service.WeiXin.Dto;

namespace WeiXinOpenPlatForm.Service.WeiXin.WeiXinHandler
{
    /// <summary>
    /// 微信消息处理接口
    /// </summary>
    public interface IWeiXinHandler : IDenpendency
    {
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <returns></returns>
        Task<string> HandleRequest(Message msg);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Common;
using WeiXinOpenPlatForm.Service.Weather;
using WeiXinOpenPlatForm.Service.Weather.Dto;
using WeiXinOpenPlatForm.Service.WeiXin.Dto;

namespace WeiXinOpenPlatForm.Service.WeiXin.WeiXinHandler
{
    /// <summary>
    /// 文本消息处理
    /// </summary>
    public class TextWeiXinHandler : IWeiXinHandler
    {

        private readonly static string MsgTemplate = @"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>";
        public async Task<string> HandleRequest(Message msg)
        {
            return await Task.FromResult(string.Format(MsgTemplate, msg.ToUserName, msg.FromUserName, TimeStampCommon.GetTimeStamp(), msg.ReturnContent));
        }
    }
}

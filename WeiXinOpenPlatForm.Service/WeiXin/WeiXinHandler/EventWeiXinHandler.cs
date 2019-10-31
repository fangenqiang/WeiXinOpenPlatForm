using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Common;
using WeiXinOpenPlatForm.Service.WeiXin.Dto;

namespace WeiXinOpenPlatForm.Service.WeiXin.WeiXinHandler
{
    public class EventWeiXinHandler : IWeiXinHandler
    {
        private readonly static string MsgTemplate = @"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>";
        public async Task<string> HandleRequest(Message msg)
        {
            string defaultText = "回复城市名称获取最新天气状况,例如:杭州";
            return await Task.FromResult(string.Format(MsgTemplate, msg.ToUserName, msg.FromUserName, TimeStampCommon.GetTimeStamp(), defaultText));
        }
    }
}

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
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append("回复以下序号获取对应功能：\r\n");
            sbStr.Append("1.智能问答\r\n");
            sbStr.Append("2.天气预报\r\n");
            sbStr.Append("3.笑话大全\r\n");
            sbStr.Append("4.谜语\r\n");
            sbStr.Append("5.脑筋急转弯\r\n");
            return await Task.FromResult(string.Format(MsgTemplate, msg.ToUserName, msg.FromUserName, TimeStampCommon.GetTimeStamp(), sbStr.ToString()));
        }
    }
}

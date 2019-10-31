using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Common;
using WeiXinOpenPlatForm.Service.WeiXin.Dto;

namespace WeiXinOpenPlatForm.Service.WeiXin.WeiXinHandler
{
    /// <summary>
    /// 默认实现
    /// </summary>
    public class DefaultWeiXinHandler : IWeiXinHandler
    {
        private readonly static string MsgTemplate = @"<xml><ToUserName><![CDATA[{0}]]></ToUserName><FromUserName><![CDATA[{1}]]></FromUserName><CreateTime>{2}</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[{3}]]></Content></xml>";

        async Task<string> IWeiXinHandler.HandleRequest(Message msg)
        {
            string defaultText = @"杭州
今日：2019-10-23 星期三 多云 温度15-23 当前温度20
穿衣指数：建议着薄外套、开衫牛仔衫裤等服装。年老体弱者应适当添加衣物，宜着夹克衫、薄毛衣等。
未来7日：
2019-10-23 星期三 多云 温度15-23
2019-10-23 星期三 多云 温度15-23
2019-10-23 星期三 多云 温度15-23
2019-10-23 星期三 多云 温度15-23
2019-10-23 星期三 多云 温度15-23
2019-10-23 星期三 多云 温度15-23
2019-10-23 星期三 多云 温度15-23";
            return await Task.FromResult(string.Format(MsgTemplate, msg.ToUserName, msg.FromUserName, TimeStampCommon.GetTimeStamp(), defaultText));
        }


    }
}

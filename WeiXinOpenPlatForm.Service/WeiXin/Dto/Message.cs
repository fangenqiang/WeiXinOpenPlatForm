using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Service.WeiXin.Dto
{
    /// <summary>
    /// 文本回复消息
    /// </summary>
    public class Message
    {
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string CreateTime { get; set; }
        public string MsgType { get; set; }
        public string Content { get; set; }
        public string ReturnContent { get; set; }

        public string Event { get; set; }
    }

    public enum MessageOrder
    {
        智能问答 = 1,
        天气预报 = 2,
        笑话大全 = 3,
        谜语 = 4,
        脑筋急转弯 = 5
    }
}

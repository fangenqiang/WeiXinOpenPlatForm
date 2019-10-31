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
        public  string ToUserName { get; set; }
        public  string FromUserName { get; set; }
        public  string CreateTime { get; set; }
        public  string MsgType { get; set; }
        public  string Content { get; set; }
        public string ReturnContent { get; set; }

        public string Event { get; set; }
    }
}

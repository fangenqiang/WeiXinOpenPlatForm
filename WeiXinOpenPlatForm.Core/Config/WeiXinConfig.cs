using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Core.Config
{
    /// <summary>
    /// 微信配置
    /// </summary>
    public class WeiXinConfig
    {
        public string Token { get; set; }
        public string EncodingAESKey { get; set; }
        public string AppID { get; set; }
        public string AppSecret { get; set; }
    }
}

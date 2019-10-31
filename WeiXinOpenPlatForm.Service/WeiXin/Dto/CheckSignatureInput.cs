using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Service.WeiXin.Dto
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public class CheckSignatureInput
    {
        public string Token { get; set; }
        public string EchoString { get; set; }
        public string Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Core.Config
{
    public  class JiSuApiConfig
    {
        [JsonProperty("appkey")]
        public string Appkey { get; set; }
    }
}

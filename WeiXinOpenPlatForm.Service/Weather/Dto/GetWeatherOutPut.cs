using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Service.Weather.Dto
{
    /// <summary>
    /// 查询天气返回参数
    /// </summary>
    public class GetWeatherOutPut
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("msg")]
        public string Msg { get; set; }
        [JsonProperty("result")]
        public dynamic Result { get; set; }

    }
}

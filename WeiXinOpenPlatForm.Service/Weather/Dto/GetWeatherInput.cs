using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Service.Weather.Dto
{
    /// <summary>
    /// 查询天气请求参数
    /// </summary>
    public class GetWeatherInput
    {
        /// <summary>
        /// 城市名称
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }
    }
}

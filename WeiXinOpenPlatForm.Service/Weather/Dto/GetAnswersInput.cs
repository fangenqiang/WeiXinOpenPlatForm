using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Service.Weather.Dto
{
    public class GetAnswersInput
    {
        [JsonProperty("question")]
        public string Question { get; set; }
    }
}

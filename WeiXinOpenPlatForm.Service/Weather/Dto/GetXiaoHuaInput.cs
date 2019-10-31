using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Service.Weather.Dto
{
    public class GetXiaoHuaInput
    {

        [JsonProperty("pagenum")]
        public string Pagenum { get; set; }
        [JsonProperty("pagesize")]
        public string Pagesize { get; set; }
        [JsonProperty("sort")]
        public string Sort { get; set; }
    }
}

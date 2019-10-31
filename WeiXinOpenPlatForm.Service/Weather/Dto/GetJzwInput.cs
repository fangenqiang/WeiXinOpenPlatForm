﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Service.Weather.Dto
{
    public class GetJzwInput
    {
        [JsonProperty("pagenum")]
        public string Pagenum { get; set; }
        [JsonProperty("pagesize")]
        public string Pagesize { get; set; }
        [JsonProperty("keyword")]
        public string Keyword { get; set; }
    }
}

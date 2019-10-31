using System;
using System.Collections.Generic;
using System.Text;

namespace WeiXinOpenPlatForm.Service.Weather.Dto
{
    public class GetReturnMessageInput
    {
        public bool IsOrder { get; set; }
        public int Order { get; set; }
        public string Content { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Http.Settings;
using WeiXinOpenPlatForm.Service.Http;
using WeiXinOpenPlatForm.Service.Weather.Dto;
using WeiXinOpenPlatForm.Service.WeiXin.Dto;

namespace WeiXinOpenPlatForm.Service.Weather
{
    /// <summary>
    /// 天气服务
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientService _httpClientService;
        public WeatherService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<string> GetReturnMessage(GetReturnMessageInput input)
        {
            string returnMessage = string.Empty;
            Enum.TryParse(input.Order.ToString(), out MessageOrder order);
            switch (order)
            {
                case MessageOrder.智能问答:
                    returnMessage = input.IsOrder ? "成功开启智能问答功能，快来聊聊吧！" : await GetAnswers(new GetAnswersInput() { Question = input.Content });
                    break;
                case MessageOrder.天气预报:
                    returnMessage = input.IsOrder ? "成功开启天气预报功能，输入你要查询的城市名称！" : await GetWeather(new GetWeatherInput() { City = input.Content });
                    break;
                case MessageOrder.笑话大全:
                    returnMessage = input.IsOrder ? "成功开启笑话大全功能，随机输入可获取笑话！" : await GetXiaoHua(new GetXiaoHuaInput() { Pagenum = "1", Pagesize = "1", Sort = "rand" });
                    break;
                case MessageOrder.谜语:
                    returnMessage = input.IsOrder ? "成功开启谜语功能，输入关键字就可以获取谜语呢！" : await GetMiYu(new GetMiYuInput() { Pagesize = "1", Pagenum = "1", Keyword = input.Content, Classid = "1" });
                    break;
                case MessageOrder.脑筋急转弯:
                    returnMessage = input.IsOrder ? "成功开启脑筋急转弯功能，输入关键字就可以获取急转弯哦！" : await GetJzw(new GetJzwInput() { Pagesize = "1", Pagenum = "1", Keyword = input.Content });
                    break;
                default:
                    returnMessage = "请先输入指令序号，指令序号【1，2，3，4，5】";
                    break;
            }
            return await Task.FromResult(returnMessage);

        }
        /// <summary>
        /// 获取天气
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> GetWeather(GetWeatherInput input)
        {
            var result = await _httpClientService.PostAsJsonAsync<GetApiOutPut>(HttpClientPriority.JiSuApi, "/weather/query", input);
            if (result.Status == "0")
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append($"{result.Result.city}\r\n");
                sbStr.Append($"今日:{result.Result.date} {result.Result.week} {result.Result.weather} {result.Result.templow}-{result.Result.temphigh}°C 此时温度{result.Result.temp}°C\r\n");
                sbStr.Append($"穿衣指数:{result.Result.index[6].detail}\r\n");
                sbStr.Append("未来7日:\r\n");
                sbStr.Append($"{result.Result.daily[0].date} {result.Result.daily[0].week} {result.Result.daily[0].day.weather} {result.Result.daily[0].night.templow}-{result.Result.daily[0].day.temphigh}°C\r\n");
                sbStr.Append($"{result.Result.daily[1].date} {result.Result.daily[1].week} {result.Result.daily[1].day.weather} {result.Result.daily[1].night.templow}-{result.Result.daily[1].day.temphigh}°C\r\n");
                sbStr.Append($"{result.Result.daily[2].date} {result.Result.daily[2].week} {result.Result.daily[2].day.weather} {result.Result.daily[2].night.templow}-{result.Result.daily[2].day.temphigh}°C\r\n");
                sbStr.Append($"{result.Result.daily[3].date} {result.Result.daily[3].week} {result.Result.daily[3].day.weather} {result.Result.daily[3].night.templow}-{result.Result.daily[3].day.temphigh}°C\r\n");
                sbStr.Append($"{result.Result.daily[4].date} {result.Result.daily[4].week} {result.Result.daily[4].day.weather} {result.Result.daily[4].night.templow}-{result.Result.daily[4].day.temphigh}°C\r\n");
                sbStr.Append($"{result.Result.daily[5].date} {result.Result.daily[5].week} {result.Result.daily[5].day.weather} {result.Result.daily[5].night.templow}-{result.Result.daily[5].day.temphigh}°C\r\n");
                sbStr.Append($"{result.Result.daily[6].date} {result.Result.daily[6].week} {result.Result.daily[6].day.weather} {result.Result.daily[6].night.templow}-{result.Result.daily[6].day.temphigh}°C");
                return sbStr.ToString();
            }
            return result.Msg;
        }
        /// <summary>
        /// 获取智能问答
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAnswers(GetAnswersInput input)
        {
            var result = await _httpClientService.PostAsJsonAsync<GetApiOutPut>(HttpClientPriority.JiSuApi, "/iqa/query", input);
            if (result.Status == "0")
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append($"{result.Result.content}");
                return sbStr.ToString();
            }
            return result.Msg;
        }
        /// <summary>
        /// 获取笑话
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetXiaoHua(GetXiaoHuaInput input)
        {
            var result = await _httpClientService.PostAsJsonAsync<GetApiOutPut>(HttpClientPriority.JiSuApi, "/xiaohua/text", input);
            if (result.Status == "0")
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append($"{result.Result.list[0].content}");
                return sbStr.ToString();
            }
            return result.Msg;
        }
        /// <summary>
        /// 获取谜语
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetMiYu(GetMiYuInput input)
        {
            var result = await _httpClientService.PostAsJsonAsync<GetApiOutPut>(HttpClientPriority.JiSuApi, "/miyu/search", input);
            if (result.Status == "0")
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append($"谜语:{result.Result.list[0].content}\r\n");
                sbStr.Append($"谜底:{result.Result.list[0].answer}");
                return sbStr.ToString();
            }
            return result.Msg;
        }
        /// <summary>
        /// 获取脑筋急转弯
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetJzw(GetJzwInput input)
        {
            var result = await _httpClientService.PostAsJsonAsync<GetApiOutPut>(HttpClientPriority.JiSuApi, "/jzw/search", input);
            if (result.Status == "0")
            {
                StringBuilder sbStr = new StringBuilder();
                sbStr.Append($"题目:{result.Result.list[0].content}\r\n");
                sbStr.Append($"答案:{result.Result.list[0].answer}");
                return sbStr.ToString();
            }
            return result.Msg;
        }
    }
}

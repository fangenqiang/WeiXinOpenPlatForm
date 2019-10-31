using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Http.Settings;
using WeiXinOpenPlatForm.Service.Http;
using WeiXinOpenPlatForm.Service.Weather.Dto;

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
        public async Task<string> GetWeather(GetWeatherInput input)
        {
            if (input.City == "1")
                input.City = "杭州";
            var result = await _httpClientService.PostAsJsonAsync<GetWeatherOutPut>(HttpClientPriority.JiSuApi, "/weather/query", input);
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
                sbStr.Append($"{result.Result.daily[6].date} {result.Result.daily[6].week} {result.Result.daily[6].day.weather} {result.Result.daily[6].night.templow}-{result.Result.daily[6].day.temphigh}°C\r\n");
                return sbStr.ToString();
            }
            return result.Msg;
        }
    }
}

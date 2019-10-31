using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Service.Weather.Dto;

namespace WeiXinOpenPlatForm.Service.Weather
{
    /// <summary>
    /// 天气服务接口
    /// </summary>
    public interface IWeatherService: IDenpendency
    {
        /// <summary>
        /// 天气查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<string> GetWeather(GetWeatherInput input);
    }
}

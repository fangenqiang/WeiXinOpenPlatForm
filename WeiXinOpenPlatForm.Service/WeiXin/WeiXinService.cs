using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WeiXinOpenPlatForm.Core.Security;
using WeiXinOpenPlatForm.Service.Weather;
using WeiXinOpenPlatForm.Service.WeiXin.Dto;
using WeiXinOpenPlatForm.Service.WeiXin.WeiXinHandler;

namespace WeiXinOpenPlatForm.Service.WeiXin
{
    /// <summary>
    /// 微信服务实现
    /// </summary>
    public class WeiXinService : IWeiXinService
    {
        private readonly IWeatherService _weatherService;
        public WeiXinService(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        /// <summary>
        /// 渠道服务集合
        /// </summary>
        protected static ConcurrentDictionary<string, IWeiXinHandler> Handlers = new ConcurrentDictionary<string, IWeiXinHandler>();

        /// <summary>
        /// 设定消息handler
        /// </summary>
        /// <param name="channelId">渠道 ID</param>
        /// <returns>渠道接口</returns>
        public static IWeiXinHandler SetHandler(string msgType)
        {
            return Handlers.GetOrAdd(msgType, id =>
            {
                var typeName = $"WeiXinOpenPlatForm.Service.WeiXin.WeiXinHandler.{msgType}WeiXinHandler";
                var type = Type.GetType(typeName);

                if (type != null && Activator.CreateInstance(type) is IWeiXinHandler handler)
                {
                    return handler;
                }
                return Activator.CreateInstance<DefaultWeiXinHandler>();
            });
        }
        /// <summary>
        /// 校验Token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> CheckSignature(CheckSignatureInput input)
        {
            return await Task.FromResult(WeiXinSign.SHA1Encrypt(input.Token, input.Signature, input.Timestamp, input.Nonce));
        }
        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="requestXml"></param>
        /// <returns></returns>
        public async Task<string> ResponseMsg(string requestXml)
        {
            string responseMsg = string.Empty;
            if (!string.IsNullOrEmpty(requestXml))
            {
                //解析数据
                XElement element = XElement.Parse(requestXml);
                Message msg = new Message
                {
                    ToUserName = element.Element(MessageDesc.FromUserName)?.Value,
                    FromUserName = element.Element(MessageDesc.ToUserName)?.Value,
                    MsgType = element.Element(MessageDesc.MsgType)?.Value,
                    Content = element.Element(MessageDesc.Content)?.Value,
                    Event = element.Element(MessageDesc.Event)?.Value,
                };

                if (msg.MsgType != null)
                {
                    string msgType = msg.MsgType.First().ToString().ToUpper() + msg.MsgType.Substring(1);
                    if (msgType == "Text")
                    {
                        msg.ReturnContent = await _weatherService.GetWeather(new Weather.Dto.GetWeatherInput() { City = msg.Content });
                    }
                    responseMsg = await SetHandler(msgType).HandleRequest(msg);
                }
            }
            return await Task.FromResult(responseMsg);
        }
    }
}

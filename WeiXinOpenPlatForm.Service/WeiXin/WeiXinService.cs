﻿using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using WeiXinOpenPlatForm.Core.Security;
using WeiXinOpenPlatForm.Service.Weather;
using WeiXinOpenPlatForm.Service.Weather.Dto;
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
        /// 功能服务集合
        /// </summary>
        protected static ConcurrentDictionary<string, IWeiXinHandler> Handlers = new ConcurrentDictionary<string, IWeiXinHandler>();
        /// <summary>
        /// 用户指令集合
        /// </summary>
        protected static ConcurrentDictionary<string, int> Orders = new ConcurrentDictionary<string, int>();
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
                        int order = 0;
                        bool isOrder = false;
                        //判断是否是回复指令
                        if (int.TryParse(msg.Content, out order) && Enum.IsDefined(typeof(MessageOrder), order))
                        {
                            isOrder = true;
                            Orders.AddOrUpdate(msg.FromUserName, order, (key, oldValue) => order);
                        }
                        else
                        {
                            if (Orders.ContainsKey(msg.FromUserName))
                            {
                                order = Orders[msg.FromUserName];
                            }
                        }
                        msg.ReturnContent = await _weatherService.GetReturnMessage(new GetReturnMessageInput() { Order = order, Content = msg.Content, IsOrder = isOrder });
                    }
                    responseMsg = await SetHandler(msgType).HandleRequest(msg);
                }
            }
            return await Task.FromResult(responseMsg);
        }
    }
}

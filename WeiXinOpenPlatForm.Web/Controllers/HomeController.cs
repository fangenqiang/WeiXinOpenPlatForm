using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Config;
using WeiXinOpenPlatForm.Service.Weather;
using WeiXinOpenPlatForm.Service.WeiXin;
using WeiXinOpenPlatForm.Service.WeiXin.Dto;
using WeiXinOpenPlatForm.Web.Filter;
using WeiXinOpenPlatForm.Web.Models;

namespace WeiXinOpenPlatForm.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly WeiXinConfig weiXinConfig;

        private readonly IWeatherService _weatherService;
        private readonly IWeiXinService _weiXinService;
        private readonly ILog _log = LogManager.GetLogger(Startup.repository.Name, typeof(WeiXinActionFilterAttribute));
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWeatherService weatherService, IWeiXinService weiXinService)
        {
            _logger = logger;
            weiXinConfig = configuration.GetSection(nameof(WeiXinConfig))?.Get<WeiXinConfig>();
            _weatherService = weatherService;
            _weiXinService = weiXinService;
        }
        /// <summary>
        /// 微信请求接口
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> WeiXin()
        {
            CheckSignatureInput input = new CheckSignatureInput()
            {
                Token = weiXinConfig.Token,
                EchoString = HttpContext.Request.Query["echoStr"],
                Signature = HttpContext.Request.Query["signature"],
                Timestamp = HttpContext.Request.Query["timestamp"],
                Nonce = HttpContext.Request.Query["nonce"]
            };
            var check = await _weiXinService.CheckSignature(input);
            if (check)
            {
                //微信token验证
                if (HttpContext.Request.Method.ToLower().Equals("get"))
                {
                    return Content(input.EchoString);
                }
                //处理微信消息
                else
                {
                    string postString = string.Empty;
                    Request.EnableBuffering();
                    using (var reader = new StreamReader(Request.Body))
                    {
                        postString = await reader.ReadToEndAsync();
                    }
                    //请求body
                    _log.Info(postString);
                    string responseMsg = await _weiXinService.ResponseMsg(postString);
                    return Content(responseMsg);
                }
            }
            return Content("微信token校验不通过");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeiXinOpenPlatForm.Core.Config;
namespace WeiXinOpenPlatForm.Http.Handlers
{
    /// <summary>
    /// 微信接口请求配置
    /// </summary>
    public class WeiXinHandler : DelegatingHandler
    {

        private readonly WeiXinConfig _weiXinConfig;
        public WeiXinHandler(IConfiguration configuration)
        {
            _weiXinConfig = configuration.GetSection(nameof(WeiXinConfig))?.Get<WeiXinConfig>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            return result;
        }
    }
}

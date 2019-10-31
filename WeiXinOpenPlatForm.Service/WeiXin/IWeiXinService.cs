using System.Threading.Tasks;
using WeiXinOpenPlatForm.Service.WeiXin.Dto;

namespace WeiXinOpenPlatForm.Service.WeiXin
{
    /// <summary>
    /// 微信服务接口
    /// </summary>
    public interface IWeiXinService:IDenpendency
    {
        /// <summary>
        /// 校验Token
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> CheckSignature(CheckSignatureInput input);

        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="requestXml"></param>
        /// <returns></returns>
        Task<string> ResponseMsg(string requestXml);

    }
}

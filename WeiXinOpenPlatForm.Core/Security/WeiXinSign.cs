using System;
using System.Security.Cryptography;
using System.Text;

namespace WeiXinOpenPlatForm.Core.Security
{
    /// <summary>
    /// 微信校验
    /// </summary>
    public static class WeiXinSign
    {
        public static bool SHA1Encrypt(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            //字典排序
            Array.Sort(ArrTmp);
            //拼接
            string tmpStr = string.Join("", ArrTmp);
            //sha1验证
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(tmpStr));
            string shaStr = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return shaStr == signature;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace WeiXinOpenPlatForm.Core.Common
{
    public class ApiResult<T>
    {
        /// <summary>
        /// 返回代码（0：失败；1：成功）
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 返回信息代码
        /// </summary>
        public string MsgCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 返回结果条数（用于分页查询，其余接口固定为 -1）
        /// </summary>
        public int Total { get; set; } = -1;

        /// <summary>
        /// 将返回数据转换成 JSON 字符串
        /// </summary>
        /// <returns>转换得到的 JSON 字符串</returns>
        public string DataToJson()
        {
            return Data == null ? string.Empty : JsonConvert.SerializeObject(Data);
        }

        /// <summary>
        /// 将 FanjiaApiResult 转换成 JSON 字符串
        /// </summary>
        /// <returns>转换得到的 JSON 字符串</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// 将 FanjiaApiResult 转换成 HTTP 响应消息
        /// </summary>
        /// <returns>HTTP 响应消息</returns>
        public HttpResponseMessage ToHttpResponseMessage()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(ToJson(), Encoding.UTF8, "application/json")
            };
        }

        /// <summary>
        /// 创建一个表示成功的 API 返回结果
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="total">返回结果条数</param>
        /// <param name="resultCode"></param>
        /// <returns>成功的 API 返回结果</returns>
        public static ApiResult<T> CreateSuccessResult(T data, int total = -1, int resultCode = 1)
        {
            return new ApiResult<T>
            {
                ResultCode = resultCode,
                MsgCode = "200",
                Msg = "OK",
                Data = data,
                Total = total
            };
        }

        /// <summary>
        /// 创建一个表示错误请求的 API 返回结果
        /// </summary>
        /// <param name="messasge">错误信息</param>
        /// <param name="msgcode"></param>
        /// <param name="data">返回数据</param>
        /// <returns>错误请求的 API 返回结果</returns>
        public static ApiResult<T> CreateBadRequestResult(string messasge, string msgcode = "400", T data = default(T))
        {
            return new ApiResult<T>
            {
                ResultCode = 0,
                MsgCode = msgcode,
                Msg = messasge,
                Data = data
            };
        }
    }
}

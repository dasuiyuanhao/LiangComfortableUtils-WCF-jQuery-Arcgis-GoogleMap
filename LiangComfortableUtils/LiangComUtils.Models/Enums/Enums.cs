using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiangComUtils.Models.Enums
{
    //各种枚举,即使它是个类

    /// <summary>
    /// 服务返回结果状态
    /// DEFAULT,默认,用于表示未处理。
    ///OK，用于表示相关响应有效。
    ///ERROR，用于表示结果出错。
    ///NOT_FOUND，用于表示请求参数没有找到。
    ///ZERO_RESULTS，用于表示无查询结果。
    ///INVALID_REQUEST，表示缺少参数。
    ///OVER_QUERY_LIMIT，用于表示网页在允许的时间段内发送的请求过多。
    ///REQUEST_DENIED，用于表示不允许使用本服务。
    ///UNKNOWN_ERROR，用于表示请求因服务器出错而无法得到处理。如果您重试一次，该请求可能就会成功。
    ///其他内容，用于自定义接口契约的返回结果，例如数字。
    /// </summary>
    public static class EnumServiceResponseResultStatus
    {
        /// <summary>
        /// 默认,用于表示未处理。
        /// </summary>
        public static string StatusDEFAULT = "DEFAULT";
        /// <summary>
        /// 成功,用于表示相关响应有效。
        /// </summary>
        public static string StatusOK = "OK";
        /// <summary>
        /// 出错,错误信息在message中
        /// </summary>
        public static string StatusERROR = "ERROR";
        /// <summary>
        /// 用于表示请求参数没有找到。
        /// </summary>
        public static string StatusNOT_FOUND = "NOT_FOUND";
        /// <summary>
        /// 用于表示无查询结果。
        /// </summary>
        public static string StatusZERO_RESULTS = "ZERO_RESULTS";
        /// <summary>
        /// INVALID_REQUEST，表示缺少参数。
        /// </summary>
        public static string StatusINVALID_REQUEST = "INVALID_REQUEST";
        /// <summary>
        /// 用于表示网页在允许的时间段内发送的请求过多。
        /// </summary>
        public static string StatusOVER_QUERY_LIMIT = "OVER_QUERY_LIMIT";
        /// <summary>
        /// 用于表示不允许使用本服务。
        /// </summary>
        public static string StatusREQUEST_DENIED = "REQUEST_DENIED";
        /// <summary>
        /// 用于表示请求因服务器出错而无法得到处理。如果您重试一次，该请求可能就会成功。
        /// </summary>
        public static string StatusUNKNOWN_ERROR = "UNKNOWN_ERROR";
    }

}

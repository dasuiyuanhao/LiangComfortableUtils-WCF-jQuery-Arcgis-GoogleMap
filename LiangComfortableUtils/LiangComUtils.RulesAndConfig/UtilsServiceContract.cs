using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiangComUtils.Models.ServiceData;

namespace LiangComUtils.RulesAndConfig
{
    public class UtilsServiceContract
    {

        /// <summary>
        /// 创建请求返回结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="content"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResponseResult CreateResponseResult(string status, Object content, string message)
        {
            ResponseResult result = new ResponseResult();

            return result;
        }
    }
}

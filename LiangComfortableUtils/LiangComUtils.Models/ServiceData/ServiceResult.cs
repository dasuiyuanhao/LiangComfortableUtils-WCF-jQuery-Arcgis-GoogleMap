/**  版本信息模板在安装目录下，可自行修改。
* UserInfo.cs
*
* 功 能： N/A
* 类 名： UserInfo
*
* Ver    变更日期             负责人 刘晓亮 变更内容
* ───────────────────────────────────
* V0.01  2013/6/27 星期四 23:33:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为机密信息，未经同意禁止向第三方披露．　│
*│　版权所有：　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using LiangComUtils.Models.Enums;
namespace LiangComUtils.Models.ServiceData
{
	/// <summary>
	/// 服务返回结果数据模型
	/// </summary>
	[Serializable]
    [DataContract]
	public class ResponseResult
	{
        public ResponseResult() 
        {
            this.status = Enums.EnumServiceResponseResultStatus.StatusDEFAULT;
            this.message = string.Empty;
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="content"></param>
        public ResponseResult(Object content, string strMessage=null)
        {
            this.status = Enums.EnumServiceResponseResultStatus.StatusOK;
            this.contents = new List<object> { content };
            if (!string.IsNullOrEmpty(strMessage))
            {
                this.message = strMessage;
            }
        }

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="contents"></param>
        public ResponseResult(List<Object> contents, string strMessage = null)
        {
            this.status = Enums.EnumServiceResponseResultStatus.StatusOK;
            this.contents = contents;
            if (!string.IsNullOrEmpty(strMessage))
            {
                this.message = strMessage;
            }
        }
        
        public ResponseResult(string strStatus, Object content, string strMessage = null)
        {
            this.status = strStatus;
            this.contents = new List<object> { content };
            if (!string.IsNullOrEmpty(strMessage))
            {
                this.message = strMessage;
            }
        }

        public ResponseResult(string strStatus, List<Object> contents, string strMessage = null)
        {
            this.status = strStatus;
            this.contents = contents;
            if (!string.IsNullOrEmpty(strMessage))
            {
                this.message = strMessage;
            }
        }

        [DataMember]
        public string status { get; set; }
        [DataMember]
        public List<Object> contents { get; set; }
        [DataMember]
        public string message { get; set; }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="strStatus"></param>
        /// <param name="contents"></param>
        /// <param name="strMessage"></param>
        public void SetResult(string strStatus, Object content, string strMessage = null)
        {
            this.status = strStatus;
            this.contents = new List<object> { content };
            if (!string.IsNullOrEmpty(strMessage))
            {
                this.message = strMessage;
            }
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        /// <param name="strStatus"></param>
        /// <param name="contents"></param>
        /// <param name="strMessage"></param>
        public void SetResult(string strStatus, List<Object> contents, string strMessage = null)
        {
            this.status = strStatus;
            this.contents = contents;
            if (!string.IsNullOrEmpty(strMessage))
            {
                this.message = strMessage;
            }
        }        
	}

    /// <summary>
    /// 服务请求数据模型
    /// </summary>
    [Serializable]
    [DataContract]
    public class RequestResult
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string sessionId { get; set; }
        [DataMember]
        public string contents { get; set; }
        [DataMember]
        public string message { get; set; }
    }
}


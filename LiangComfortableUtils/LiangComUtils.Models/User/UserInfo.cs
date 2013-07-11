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
*│　此技术信息为本公司机密信息，未经本人书面同意禁止向第三方披露．　│
*│　版权所有：　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Runtime.Serialization;
namespace LiangComUtils.Models.User
{
	/// <summary>
	/// 用户账号信息表
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class UserInfo
	{
		public UserInfo()
		{}
		#region Model
		private int _sysid;
		private string _username;
		private string _psw;
		private int _status=0;
		private string _email;
		private string _cellphone;
		private string _cardno;
		private DateTime _registertime;
		private DateTime _lastlogintime;
		private DateTime _updatetime;
        private string _photo;
		/// <summary>
		/// 标识列
		/// </summary>
        [DataMember]
        public int SysID
		{
			set{ _sysid=value;}
			get{return _sysid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
        [DataMember]
        public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 用户密码
		/// </summary>
        [DataMember]
        public string Psw
		{
			set{ _psw=value;}
			get{return _psw;}
		}
		/// <summary>
		/// 用户状态
		/// </summary>
        [DataMember]
        public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 电子邮箱地址
		/// </summary>
        [DataMember]
        public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 手机号，可以存储多个，以分号;分割
		/// </summary>
        [DataMember]
        public string Cellphone
		{
			set{ _cellphone=value;}
			get{return _cellphone;}
		}
		/// <summary>
		/// 身份证ID
		/// </summary>
        [DataMember]
        public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 注册时间
		/// </summary>
        [DataMember]
        public DateTime RegisterTime
		{
			set{ _registertime=value;}
			get{return _registertime;}
		}
		/// <summary>
		/// 上次登陆时间
		/// </summary>
        [DataMember]
        public DateTime LastLoginTime
		{
			set{ _lastlogintime=value;}
			get{return _lastlogintime;}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
        [DataMember]
        public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
        /// <summary>
        /// 头像照片地址
        /// </summary>
        [DataMember]
        public string Photo
        {
            set { _photo = value; }
            get { return _photo; }
        }
		#endregion Model

	}
}


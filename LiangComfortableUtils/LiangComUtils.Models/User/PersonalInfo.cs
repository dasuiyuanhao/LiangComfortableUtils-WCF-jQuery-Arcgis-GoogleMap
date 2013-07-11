/**  版本信息模板在安装目录下，可自行修改。
* PersonalInfo.cs
*
* 功 能： N/A
* 类 名： PersonalInfo
*
* Ver    变更日期             负责人 刘晓亮 变更内容
* ───────────────────────────────────
* V0.01  2013/6/27 星期四 23:33:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：刘晓亮　　                      　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace LiangComUtils.Models.User
{
	/// <summary>
	/// 个人信息表
	/// </summary>
	[Serializable]
	public partial class PersonalInfo
	{
		public PersonalInfo()
		{}
		#region Model
		private int _sysid;
		private int _userid;
		private int _status=0;
		private string _cardno;
		private string _realname;
		private string _address;
		private int? _gender;
		private string _telephone;
		private DateTime _createtime;
		private DateTime _updatetime;
		private string _cityareacode;
		/// <summary>
		/// 标识列
		/// </summary>
		public int SysID
		{
			set{ _sysid=value;}
			get{return _sysid;}
		}
		/// <summary>
		/// 关联用户ID
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 身份证ID
		/// </summary>
		public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 真实姓名
		/// </summary>
		public string RealName
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// 地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public int? Gender
		{
			set{ _gender=value;}
			get{return _gender;}
		}
		/// <summary>
		/// 固定电话号，可以存储多个，以分号;分割
		/// </summary>
		public string Telephone
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 城市编码
		/// </summary>
		public string CityAreaCode
		{
			set{ _cityareacode=value;}
			get{return _cityareacode;}
		}
		#endregion Model

	}
}


/**  版本信息模板在安装目录下，可自行修改。
* BookmarkCategory.cs
*
* 功 能： N/A
* 类 名： BookmarkCategory
*
* Ver    变更日期             负责人 刘晓亮  变更内容
* ───────────────────────────────────
* V0.01  2013/6/27 星期四 23:25:01   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：刘晓亮　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
/*
 * Liang
 * 原生态实体类Model
 * 
 */
using System;
namespace LiangComUtils.Models.Bookmark
{
	/// <summary>
	/// BookmarkCategory:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BookmarkCategory
	{
		public BookmarkCategory()
		{}
		#region Model
		private int _sysid;
		private string _title;
		private int _accountid;
		private int _accounttype;
		private int _status=0;
		private string _iconurl;
		private int _orders=0;
		private string _describe;
		private DateTime _addtime;
		private DateTime _updatetime;
		/// <summary>
		/// 标识列
		/// </summary>
		public int SysID
		{
			set{ _sysid=value;}
			get{return _sysid;}
		}
		/// <summary>
		/// 书签标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 账号ID
		/// </summary>
		public int AccountID
		{
			set{ _accountid=value;}
			get{return _accountid;}
		}
		/// <summary>
		/// 账户类型
		/// </summary>
		public int AccountType
		{
			set{ _accounttype=value;}
			get{return _accounttype;}
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
		/// 书签网址图标
		/// </summary>
		public string IconUrl
		{
			set{ _iconurl=value;}
			get{return _iconurl;}
		}
		/// <summary>
		/// 排序序号
		/// </summary>
		public int Orders
		{
			set{ _orders=value;}
			get{return _orders;}
		}
		/// <summary>
		/// 描述、备注
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		#endregion Model

	}
}


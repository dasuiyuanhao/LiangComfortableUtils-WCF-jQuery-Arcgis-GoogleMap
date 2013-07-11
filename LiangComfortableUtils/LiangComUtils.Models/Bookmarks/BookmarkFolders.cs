/**  版本信息模板在安装目录下，可自行修改。
* BookmarkFolders.cs
*
* 功 能： N/A
* 类 名： BookmarkFolders
*
* Ver    变更日期             负责人 刘晓亮  变更内容
* ───────────────────────────────────
* V0.01  2013/6/27 星期四 23:25:02   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：刘晓亮　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace LiangComUtils.Models.Bookmark
{
	/// <summary>
	/// 收藏书签文件夹
	/// </summary>
	[Serializable]
	public partial class BookmarkFolders
	{
		public BookmarkFolders()
		{}
		#region Model
		private int _sysid;
		private string _title;
		private int _status=0;
		private int _level=0;
		private int _bookmarkbasicid;
		private int? _parentid;
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
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
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
		/// 文件夹级别
		/// </summary>
		public int Level
		{
			set{ _level=value;}
			get{return _level;}
		}
		/// <summary>
		/// 关联书签ID
		/// </summary>
		public int BookMarkBasicID
		{
			set{ _bookmarkbasicid=value;}
			get{return _bookmarkbasicid;}
		}
		/// <summary>
		/// 书签父亲ID
		/// </summary>
		public int? ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		/// 描述
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


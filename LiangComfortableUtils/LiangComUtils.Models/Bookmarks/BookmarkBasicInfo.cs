/*
 * Liang
 * 原生态实体类Model
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;

namespace LiangComUtils.Models.Bookmark
{
    /// <summary>
    /// 书签基本信息
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class BookmarkBasicInfo
    {
        public BookmarkBasicInfo()
        { }
        #region Model
        private int _sysid;
        private string _title;
        private int _status = 0;
        private int _accounttype;
        private int _accountid;
        private int? _amount;
        private string _browsersinfo;
        private string _describe;
        private DateTime _createtime;
        private DateTime _updatetime;
        private int? _scores;
        /// <summary>
        /// 标识列
        /// </summary>
        [DataMember]
        public int SysID
        {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 用户状态
        /// </summary>
        [DataMember]
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 账号类型
        /// </summary>
        [DataMember]
        public int AccountType
        {
            set { _accounttype = value; }
            get { return _accounttype; }
        }
        /// <summary>
        /// 关联账号ID
        /// </summary>
        [DataMember]
        public int AccountID
        {
            set { _accountid = value; }
            get { return _accountid; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int? Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        [DataMember]
        public string BrowsersInfo
        {
            set { _browsersinfo = value; }
            get { return _browsersinfo; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 积分
        /// </summary>
        [DataMember]
        public int? Scores
        {
            set { _scores = value; }
            get { return _scores; }
        }
        #endregion Model

    }
}

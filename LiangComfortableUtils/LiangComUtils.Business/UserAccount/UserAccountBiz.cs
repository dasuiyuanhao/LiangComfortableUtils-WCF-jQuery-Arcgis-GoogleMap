using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LiangComUtils.Business
{
    public class UserAccountBiz
    {
        private readonly LiangComUtils.DataAccess.UserAccountDal dalUserAccountOperate =
            new LiangComUtils.DataAccess.UserAccountDal();

        public UserAccountBiz()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool ExistsUserInfo(int SysID)
		{
            return dalUserAccountOperate.Exists(SysID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int AddUserInfo(LiangComUtils.Models.User.UserInfo model)
		{
            model.Photo = RulesAndConfig.AppConfig.DefaultUserPhoto;
            model.Status = 0;
            model.RegisterTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;
            model.LastLoginTime = DateTime.Now;
            return dalUserAccountOperate.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool UpdateUserInfo(LiangComUtils.Models.User.UserInfo model)
		{
            return dalUserAccountOperate.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool DeleteUserInfo(int SysID)
		{
            return dalUserAccountOperate.Delete(SysID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
        public bool DeleteListUserInfos(string SysIDlist)
		{
            return dalUserAccountOperate.DeleteList(SysIDlist);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public LiangComUtils.Models.User.UserInfo GetModelUserInfo(int SysID)
		{
            Models.User.UserInfo resultUI = dalUserAccountOperate.GetModel(SysID);
            if (resultUI != null)
            {
                resultUI.Psw = null;
            }
            return resultUI;
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LiangComUtils.Models.User.UserInfo GetModelUserInfoWithPsw(int SysID)
        {
            return dalUserAccountOperate.GetModel(SysID);
        }

        /*
		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        public LiangComUtils.Models.User.UserInfo GetModelByCache(int SysID)
		{
			
			string CacheKey = "UserInfoModel-" + SysID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
                    objModel = dalUserAccountOperate.GetModel(SysID);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (LiangComUtils.Model.User.UserInfo)objModel;
		}
         */

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public DataSet GetListUserInfos(string strWhere)
		{
            return dalUserAccountOperate.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
        public DataSet GetListUserInfos(int Top, string strWhere, string filedOrder)
		{
            return dalUserAccountOperate.GetList(Top, strWhere, filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<LiangComUtils.Models.User.UserInfo> GetModelListUserInfos(string strWhere)
		{
            DataSet ds = dalUserAccountOperate.GetList(strWhere);
            return DataTableToListUserInfos(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<LiangComUtils.Models.User.UserInfo> DataTableToListUserInfos(DataTable dt)
		{
            List<LiangComUtils.Models.User.UserInfo> modelList = new List<LiangComUtils.Models.User.UserInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                LiangComUtils.Models.User.UserInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
                    model = dalUserAccountOperate.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
            return GetListUserInfos("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public int GetRecordCountUserInfos(string strWhere)
		{
            return dalUserAccountOperate.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetListUserInfosByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
            return dalUserAccountOperate.GetListByPage(strWhere, orderby, startIndex, endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod

		#region  ExtensionMethod

        /// <summary>
        /// 是否已存在用户名
        /// </summary>
        public bool ExistsUserName(string userName)
        {
            return dalUserAccountOperate.ExistsUserName(userName);
        }

        /// <summary>
        /// 是否已存在邮箱
        /// </summary>
        public bool ExistsUserEmail(string userEmail)
        {
            return dalUserAccountOperate.ExistsUserEmail(userEmail);
        }
        /// <summary>
        /// 是否已存在用户名或者邮箱
        /// </summary>
        public bool ExistsUserNameOrEmail(string userName, string userEmail)
        {
            return dalUserAccountOperate.ExistsUserNameOrEmail(userName,userEmail);
        }

        


        #region 用户登录相关

        /// <summary>
        /// 根据用户名(或者邮箱)和密码取得用户信息
        /// </summary>
        public IList<Models.User.UserInfo> GetUserInfoListByUserNameOrEmailAndPsw(string userName, string userEmail, string psw)
        {
            return dalUserAccountOperate.GetUserInfoListByUserNameOrEmailAndPsw(userName, userEmail, psw);
        }

        /// <summary>
        /// 根据用户ID、用户名和邮箱取得用户信息列表
        /// </summary>
        public IList<Models.User.UserInfo> GetUserInfoListBySysIDAndUserNameAndEmail(int sysID, string userName, string userEmail)
        {
            return dalUserAccountOperate.GetUserInfoListBySysIDAndUserNameAndEmail(sysID, userName, userEmail);
        }

        #endregion 用户登录相关

        #endregion  ExtensionMethod
    }
}

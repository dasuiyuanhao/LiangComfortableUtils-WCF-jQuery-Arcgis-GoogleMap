using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using LiangComUtils.Common;

namespace LiangComUtils.DataAccess
{
    public class UserAccountDal
    {
        public UserAccountDal()
        {
            
        }

        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SysID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where SysID=@SysID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SysID", SqlDbType.Int,4)
            };
            parameters[0].Value = SysID;
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(LiangComUtils.Models.User.UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserInfo(");
            strSql.Append("UserName,Psw,Status,Email,Cellphone,CardNo,RegisterTime,LastLoginTime,UpdateTime,Photo)");
            strSql.Append(" values (");
            strSql.Append("@UserName,@Psw,@Status,@Email,@Cellphone,@CardNo,@RegisterTime,@LastLoginTime,@UpdateTime,@Photo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Psw", SqlDbType.NVarChar,200),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Email", SqlDbType.NVarChar,500),
                    new SqlParameter("@Cellphone", SqlDbType.NVarChar,50),
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@RegisterTime", SqlDbType.DateTime),
                    new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@Photo",SqlDbType.NVarChar,2000)
            };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Psw;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Email;
            parameters[4].Value = model.Cellphone;
            parameters[5].Value = model.CardNo;
            parameters[6].Value = model.RegisterTime;
            parameters[7].Value = model.LastLoginTime;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.Photo;

            int newSysID = -1;
            //SqlHelper.ExecuteNonQuery(strSql.ToString(), parameters, out newSysID);
            string result = SqlHelper.ExcuteScaler(strSql.ToString(), parameters);
            if (!string.IsNullOrEmpty(result))
            {
                Int32.TryParse(result, out newSysID);
            }
            return newSysID;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LiangComUtils.Models.User.UserInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserInfo set ");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Psw=@Psw,");
            strSql.Append("Status=@Status,");
            strSql.Append("Email=@Email,");
            strSql.Append("Cellphone=@Cellphone,");
            strSql.Append("CardNo=@CardNo,");
            //strSql.Append("RegisterTime=@RegisterTime,");
            strSql.Append("LastLoginTime=@LastLoginTime,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append("Photo=@Photo");
            strSql.Append(" where SysID=@SysID");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Psw", SqlDbType.NVarChar,200),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Email", SqlDbType.NVarChar,500),
                    new SqlParameter("@Cellphone", SqlDbType.NVarChar,50),
                    new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@RegisterTime", SqlDbType.DateTime),
                    new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
                    new SqlParameter("@Photo", SqlDbType.NVarChar,2000),
                    new SqlParameter("@SysID", SqlDbType.Int,4)
            };
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Psw;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.Email;
            parameters[4].Value = model.Cellphone;
            parameters[5].Value = model.CardNo;
            parameters[6].Value = model.RegisterTime;
            parameters[7].Value = model.LastLoginTime;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.Photo;
            parameters[10].Value = model.SysID;

            return SqlHelper.ExcuteNonQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int SysID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo ");
            strSql.Append(" where SysID=@SysID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SysID", SqlDbType.Int,4)
            };
            parameters[0].Value = SysID;

            return SqlHelper.ExcuteNonQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string SysIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserInfo ");
            strSql.Append(" where SysID in (" + SysIDlist + ")  ");

            return SqlHelper.ExcuteNonQuery(strSql.ToString());
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LiangComUtils.Models.User.UserInfo GetModel(int SysID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SysID,UserName,Psw,Status,Email,Cellphone,CardNo,RegisterTime,LastLoginTime,UpdateTime,Photo from UserInfo ");
            strSql.Append(" where SysID=@SysID");
            SqlParameter[] parameters = {
                    new SqlParameter("@SysID", SqlDbType.Int,4)
            };
            parameters[0].Value = SysID;

            LiangComUtils.Models.User.UserInfo model = new LiangComUtils.Models.User.UserInfo();
            DataSet ds = SqlHelper.ExecuteDataSet(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LiangComUtils.Models.User.UserInfo DataRowToModel(DataRow row)
        {
            LiangComUtils.Models.User.UserInfo model = new LiangComUtils.Models.User.UserInfo();
            if (row != null)
            {
                if (row["SysID"] != null && row["SysID"].ToString() != "")
                {
                    model.SysID = int.Parse(row["SysID"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["Psw"] != null)
                {
                    model.Psw = row["Psw"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Email"] != null)
                {
                    model.Email = row["Email"].ToString();
                }
                if (row["Cellphone"] != null)
                {
                    model.Cellphone = row["Cellphone"].ToString();
                }
                if (row["CardNo"] != null)
                {
                    model.CardNo = row["CardNo"].ToString();
                }
                if (row["RegisterTime"] != null && row["RegisterTime"].ToString() != "")
                {
                    model.RegisterTime = DateTime.Parse(row["RegisterTime"].ToString());
                }
                if (row["LastLoginTime"] != null && row["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(row["LastLoginTime"].ToString());
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["Photo"] != null && row["Photo"].ToString() != "")
                {
                    model.Photo = row["Photo"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SysID,UserName,Psw,Status,Email,Cellphone,CardNo,RegisterTime,LastLoginTime,UpdateTime,Photo ");
            strSql.Append(" FROM UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" SysID,UserName,Psw,Status,Email,Cellphone,CardNo,RegisterTime,LastLoginTime,UpdateTime,Photo ");
            strSql.Append(" FROM UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteDataSet(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM UserInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = SqlHelper.ExcuteScaler(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.SysID desc");
            }
            strSql.Append(")AS Row, T.*  from UserInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return SqlHelper.ExecuteDataSet(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "UserInfo";
            parameters[1].Value = "SysID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 是否存在该用户名
        /// </summary>
        public bool ExistsUserName(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where UserName=@UserName");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,500)
            };
            parameters[0].Value = userName;
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该邮箱
        /// </summary>
        public bool ExistsUserEmail(string userEmail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where Email=@Email");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,500)
            };
            parameters[0].Value = userEmail;
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该用户名或者邮箱
        /// </summary>
        public bool ExistsUserNameOrEmail(string userName,string userEmail)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserInfo");
            strSql.Append(" where UserName=@UserName or Email=@Email;");
            SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Email", SqlDbType.NVarChar,500)
            };
            parameters[0].Value = userName;
            parameters[1].Value = userEmail;
            return SqlHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据用户名(或者邮箱)和密码取得用户信息
        /// </summary>
        public IList<Models.User.UserInfo> GetUserInfoListByUserNameOrEmailAndPsw(string userName, string userEmail, string psw)
        {
            IList<Models.User.UserInfo> result = new List<Models.User.UserInfo>();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select SysID,UserName,Status,Email,Cellphone,CardNo,RegisterTime,LastLoginTime,UpdateTime,Photo from UserInfo ");
                strSql.Append(" where (UserName=@UserName or Email=@Email) and Psw=@Psw ;");
                SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Email", SqlDbType.NVarChar,500),
                    new SqlParameter("@Psw", SqlDbType.Int,4)
            };
                parameters[0].Value = userName;
                parameters[1].Value = userEmail;
                parameters[2].Value = psw;
                DataTable dt = SqlHelper.ExecuteDataTable(strSql.ToString(), parameters);
                if (dt!=null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        result = Common.UtilsCommon.DataTableToList<Models.User.UserInfo>(dt);
                    }
                }
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 根据用户ID、用户名和邮箱取得用户信息列表
        /// </summary>
        public IList<Models.User.UserInfo> GetUserInfoListBySysIDAndUserNameAndEmail(int sysID,string userName, string userEmail)
        {
            IList<Models.User.UserInfo> result = new List<Models.User.UserInfo>();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select SysID,UserName,Status,Email,Cellphone,CardNo,RegisterTime,LastLoginTime,UpdateTime,Photo from UserInfo ");
                strSql.Append(" where UserName=@UserName and Email=@Email and SysID=@SysID ;");
                SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,500),
                    new SqlParameter("@Email", SqlDbType.NVarChar,500),
                    new SqlParameter("@SysID", SqlDbType.Int,4)
            };
                parameters[0].Value = userName;
                parameters[1].Value = userEmail;
                parameters[2].Value = sysID;
                DataTable dt = SqlHelper.ExecuteDataTable(strSql.ToString(), parameters);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        result = Common.UtilsCommon.DataTableToList<Models.User.UserInfo>(dt);
                    }
                }
            }
            catch { }
            return result;
        }

        #endregion  ExtensionMethod
    }
}

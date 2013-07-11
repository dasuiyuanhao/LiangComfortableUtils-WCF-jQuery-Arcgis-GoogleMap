using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace LiangComUtils.DataAccess
{
    public abstract class SqlHelper
    {        
        public static readonly string ConnectionStringLocal = LiangComUtils.RulesAndConfig.AppConfig.ConnectionString;
        public static readonly string ConnectionStringWMS = LiangComUtils.RulesAndConfig.AppConfig.ConnectionString;
                
        #region 执行sql查询

        public static object ExecuteScalar(string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            return ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, cmd);
        }

        public static object ExecuteScalar(SqlCommand cmd)
        {
            return ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, cmd);
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;
                cmd.CommandType = cmdType;

                return cmd.ExecuteScalar();
            }
        }

        public static int ExecuteNonQuery(string cmdText)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            return ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, cmd);
        }

        public static int ExecuteNonQuery(SqlCommand cmd)
        {
            return ExecuteNonQuery(SqlHelper.ConnectionStringLocal, cmd.CommandType, cmd);
        }

        public static int ExecuteNonQuery(string connectionString, SqlCommand cmd)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, cmd);
        }


        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, SqlCommand cmd)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;
                cmd.CommandType = cmdType;

                int rowsAffected = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return rowsAffected;
            }
        }

        public static int ExecuteNonQuery(SqlCommand cmd, out int sysno)
        {
            return ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, cmd, out sysno);
        }

        public static int ExecuteNonQuery(string connectionString, SqlCommand cmd, out int sysno)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, cmd, out sysno);
        }

        public static int ExecuteNonQuery(string cmdText, CommandType cmdType)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            return ExecuteNonQuery(SqlHelper.ConnectionStringLocal, cmdType, cmd);
        }

        /// <summary>
        /// 适用于增加一条记录
        /// </summary>
        /// <param name="sqlString"></param>
        /// <param name="paras"></param>
        /// <param name="sysno"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sqlString, SqlParameter[] paras, out int sysno)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringLocal))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sqlString;
                cmd.CommandType = CommandType.Text;
                if (paras != null && paras.Length > 0)
                    cmd.Parameters.AddRange(paras);

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                sysno = rowsAffected;
                return rowsAffected;
            }
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, SqlCommand cmd, out int sysno)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd.Connection = conn;
                cmd.CommandType = cmdType;

                int rowsAffected = cmd.ExecuteNonQuery();

                //必须符合下面的条件
                if (cmd.Parameters.Contains("@SysID") && cmd.Parameters["@SysID"].Direction == ParameterDirection.Output)
                    sysno = (int)cmd.Parameters["@SysID"].Value;
                else
                {
                    throw new Exception("SqlHelper: Does not contain SysNo or ParameterDirection is Not Output");
                }

                cmd.Parameters.Clear();
                return rowsAffected;
            }
        }
        public static SqlDataReader ExecuteDataReader(string cmdText)
        {
            return ExecuteDataReader(SqlHelper.ConnectionStringLocal, cmdText, null);
        }
        public static SqlDataReader ExecuteDataReader(string connectionString, string cmdText, SqlParameter[] paras)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            if (conn.State != ConnectionState.Open)
                conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (paras != null && paras.Length > 0)
                cmd.Parameters.AddRange(paras);

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public static DataSet ExecuteDataSet(string cmdText)
        {
            return ExecuteDataSet(SqlHelper.ConnectionStringLocal, cmdText, null);
        }

        public static DataSet ExecuteDataSet(string connectionString, string cmdText)
        {
            return ExecuteDataSet(connectionString, cmdText, null);
        }

        public static DataSet ExecuteDataSet(string cmdText, SqlParameter[] paras)
        {
            return ExecuteDataSet(SqlHelper.ConnectionStringLocal, cmdText, paras);
        }

        public static DataSet ExecuteDataSet(string connectionString, string cmdText, SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                if (paras != null && paras.Length > 0)
                    cmd.Parameters.AddRange(paras);

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                sqlDA.Fill(dataSet, "Anonymous");

                return dataSet;
            }
        }

        
        #region sql执行

        public static SqlConnection conn = null;

        ///<summary>
        ///获取数据库连接
        ///</summary>
        public static SqlConnection getCon()
        {
            // TODO：conn == null 处理待定
            if (conn == null)
            {
                string sConnectionString = ConnectionStringLocal;
                return new SqlConnection(sConnectionString);
            }
            else
            {
                return conn;
            }

        }

        ///<summary>
        ///SQL得到DATASET
        ///</summary>
        public static DataSet DataSetRunSql(string sql, SqlParameter[] parameters)
        {
            SqlConnection con = getCon();
            SqlCommand cmd = new SqlCommand(sql, con);

            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            con.Open();
            //执行操作
            adapter.Fill(ds);
            con.Close();
            return ds;
        }

        /// <summary>
        /// 适用于返回单行单列的查询语句
        /// </summary>
        /// <param name="sql">要执行的语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="paras">参数集合</param>
        /// <returns>返回的结果</returns>
        public static string ExcuteScaler(string sql, CommandType type, params SqlParameter[] paras)
        {
            SqlConnection cn = getCon();
            object obj = null;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = type;
                if (paras != null && paras.Length > 0)
                    //将参数集合添加到命令对象的参数集合中去
                    cmd.Parameters.AddRange(paras);
                obj = cmd.ExecuteScalar();
            }
            catch
            { }
            finally
            {
                cn.Close();
            }
            if (obj != null)
                return obj.ToString();
            return "";
        }

        /// <summary>
        /// 适用于增、删、改的方法
        /// </summary>
        /// <param name="sql">要执行的语句</param>
        /// <param name="type">命令类型</param>
        /// <param name="paras">参数集合</param>
        /// <returns>true:成功;false:失败</returns>
        public static bool ExecuteNonQueryIsSuccess(string sql, CommandType type, params SqlParameter[] paras)
        {
            SqlConnection cn = getCon();
            int rows = 0;
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(sql, cn);
                //设置命令类型
                cmd.CommandType = type;
                if (paras != null && paras.Length > 0)
                    //将参数集合添加到命令对象的参数集合中去
                    cmd.Parameters.AddRange(paras);

                rows = cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                cn.Close();
            }
            return rows > 0 ? true : false;
        }
        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExcSql(string sql)
        {
            int rows = -1;

            SqlConnection conn = getCon();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                SqlCommand comm = conn.CreateCommand();

                comm.Connection = conn;
                comm.Transaction = tran;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sql;

                rows = comm.ExecuteNonQuery();

                tran.Commit();

            }
            catch (SqlException)
            {
                tran.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
        /// <summary>
        /// 事务执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExcSql(List<string> sqlList)
        {
            int rows = -1;

            SqlConnection conn = getCon();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                SqlCommand comm = conn.CreateCommand();

                comm.Connection = conn;
                comm.Transaction = tran;
                comm.CommandType = CommandType.Text;
                foreach (string sql in sqlList)
                {
                    comm.CommandText = sql;

                    rows = comm.ExecuteNonQuery();

                    if (rows < 0)
                    {
                        tran.Rollback();
                        break;
                    }
                }
                tran.Commit();

            }
            catch (SqlException)
            {
                rows = -1;
                tran.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }
        ///// <summary>
        ///// sql取得结果
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <returns></returns>
        //public static DataTable loadSql(string sql)
        //{
        //    DataSet ds = new DataSet();

        //    DateTime now = DateTime.Now;
        //    StringBuilder log = new StringBuilder();
        //    log.AppendLine("start:" + now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
        //    log.AppendLine(sql);

        //    SQLHelper.FillDataset(getCon(), CommandType.Text, sql, ds, "table");

        //    log.AppendLine("end:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
        //    TimeSpan ts = DateTime.Now - now;
        //    int m = ts.Milliseconds;
        //    log.AppendLine("time1:" + m);
        //    log.AppendLine("time2:" + ts.ToString());
        //    if (m > 100)
        //    {
        //        log.AppendLine("************************************");
        //    }
        //    SaveNote(log.ToString());

        //    return ds.Tables["table"];
        //}

        ///<summary>
        ///多表查询，返回dataset
        ///</summary>
        public static DataSet GetDataSet(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, getCon());
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch (Exception e)
            {

            }

            return ds;

        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pras"></param>
        /// <returns></returns>
        public static DataSet ExecuteProcdure(string p, SqlParameter[] pras)
        {
            SqlConnection conn = getCon();
            conn.Open();
            SqlCommand comm = new SqlCommand(p, conn);
            comm.CommandType = CommandType.StoredProcedure;

            foreach (var v in pras)
            {
                comm.Parameters.Add(v);
            }

            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(comm);
            sda.Fill(ds);
            return ds;
        }

        #endregion sql执行


        #region liang添加

        public static DataTable ExecuteDataTable(string cmdText)
        {
            return ExecuteDataTable(SqlHelper.ConnectionStringLocal, cmdText, null);
        }

        public static DataTable ExecuteDataTable(string connectionString, string cmdText)
        {
            return ExecuteDataTable(connectionString, cmdText, null);
        }

        public static DataTable ExecuteDataTable(string cmdText, SqlParameter[] paras)
        {
            return ExecuteDataTable(SqlHelper.ConnectionStringLocal, cmdText, paras);
        }

        public static DataTable ExecuteDataTable(string connectionString, string cmdText, SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                if (paras != null && paras.Length > 0)
                    cmd.Parameters.AddRange(paras);

                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = cmd;
                DataSet dataSet = new DataSet();
                sqlDA.Fill(dataSet, "Anonymous");
                if (dataSet != null)
                {
                    if (dataSet.Tables != null)
                    {
                        if (dataSet.Tables.Count > 0)
                        {
                            return dataSet.Tables[0];
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 适用于返回单行单列的查询语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static string ExcuteScaler(string cmdText)
        {
            return ExcuteScaler(cmdText, null);
        }
        /// <summary>
        /// 适用于返回单行单列的查询语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string ExcuteScaler(string cmdText, SqlParameter[] paras)
        {
            return ExcuteScaler(cmdText, CommandType.Text, paras);
        }

        public static bool Exists(string cmdText)
        {
            return Exists(cmdText, null);
        }

        public static bool Exists(string cmdText, SqlParameter[] paras)
        {
            int excuteResult = 0;
            if (Int32.TryParse(ExcuteScaler(cmdText, CommandType.Text, paras), out excuteResult))
            {
                if (excuteResult > 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 适用于增、删、改的方法
        /// </summary>
        /// <param name="sql">要执行的语句</param>
        /// <param name="paras">参数集合</param>
        /// <returns>true:成功;false:失败</returns>
        public static bool ExcuteNonQuery(string sql, params SqlParameter[] paras)
        {
            return ExecuteNonQueryIsSuccess(sql, CommandType.Text, paras);
        }

        /// <summary>
        /// 适用于增、删、改的方法
        /// </summary>
        /// <param name="sql">要执行的语句</param>
        /// <returns>true:成功;false:失败</returns>
        public static bool ExcuteNonQuery(string sql)
        {
            return ExecuteNonQueryIsSuccess(sql, CommandType.Text,null);
        }



        #endregion liang添加



        #endregion
    }
}

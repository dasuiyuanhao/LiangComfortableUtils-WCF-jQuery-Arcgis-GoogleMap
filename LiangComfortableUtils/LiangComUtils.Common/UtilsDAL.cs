using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using System.Net.Json;

namespace LiangComUtils.Common
{
    public partial class UtilsCommon
    {
        #region  数据和实体对象转换工具方法

        /// <summary>
        /// 转换datatable为指定类型的list对象
        /// </summary>
        /// <typeparam name="T">指定的转换类型</typeparam>
        /// <param name="dt">需要转换的datatable对象</param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            IList<T> list = new List<T>();

            Type type = typeof(T);
            string tempName = "";
            PropertyInfo[] propertys = type.GetProperties();

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    try
                    {
                        tempName = pi.Name;

                        if (dt.Columns.Contains(tempName))
                        {
                            if (!pi.CanWrite) continue;

                            object value = dr[tempName];
                            if (value != DBNull.Value)
                            {
                                value = GetDeaultTypeValue(value, pi.PropertyType.ToString());
                                pi.SetValue(t, value, null);
                            }
                        }
                    }
                    catch { }
                }

                list.Add(t);
            }

            return list;
        }

        /// <summary>
        /// 将datatable转换为指定数据模型
        /// </summary>
        /// <typeparam name="T">模型类型</typeparam>
        /// <param name="dt">数据源</param>
        /// <returns></returns>
        public static T DataTableToModel<T>(DataTable dt) where T : new()
        {
            T t = new T();

            if (dt.Rows.Count > 0)
            {
                Type type = typeof(T);
                string tempName = "";
                PropertyInfo[] propertys = t.GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;

                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;

                        object value = GetDeaultTypeValue(dt.Rows[0][tempName], pi.PropertyType.ToString());
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
            }

            return t;
        }

        /// <summary>
        /// 将object转换为指定类型数据
        /// </summary>
        /// <param name="p"></param>
        /// <param name="tpname"></param>
        /// <returns></returns>
        private static object GetDeaultTypeValue(object p, string tpname)
        {
            if (tpname == "System.Int32")
                return Convert.ToInt32(p);
            if (tpname == "System.Single")
                return Convert.ToSingle(p);
            if (tpname == "System.DateTime")
                return Convert.ToDateTime(p);
            if (tpname == "System.Decimal")
                return Convert.ToDecimal(p);
            if (tpname == "System.Double")
                return Convert.ToDouble(p);

            return Convert.ToString(p);
        }

        ///  <summary>  
        /// 分析用户请求是否正常(检测SQL注入)
        ///  </summary>  
        ///  <param name="Str">传入用户提交数据  </param>  
        ///  <returns>返回是否含有SQL注入式攻击代码  </returns>  
        public static bool ProcessSqlStr(string Str)
        {
            bool ReturnValue = true;
            try
            {
                if (Str.Trim() != "")
                {
                    string SqlStr = "and |exec |insert |select |delete |update |count |* |chr |mid |master |truncate |char |declare";

                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.ToLower().IndexOf(ss) >= 0)
                        {
                            ReturnValue = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }

        

        #endregion 数据和实体对象转换工具方法
        
        #region 执行SQL相关

        public static bool HasMoreRow(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool HasMoreRow(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;
            else
                return true;
        }

        //整理字符串到安全格式
        public static string SafeFormat(string strInput)
        {
            return strInput.Trim().Replace("'", "''");
        }

        public static string ToSqlString(string paramStr)
        {
            return "'" + SafeFormat(paramStr) + "'";
        }

        public static string ToSqlLikeString(string paramStr)
        {
            return "'%" + SafeFormat(paramStr) + "%'";
        }
        public static string ToSqlLikeStringR(string paramStr)
        {
            return "'" + SafeFormat(paramStr) + "%'";
        }

        /// <summary>
        /// 对象转字符型，null变空串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string objectToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString().Trim();
        }
        /// <summary>
        /// 对象转字符型，null变空串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string objectToNumbString(object obj)
        {
            return objectToString(obj) == "" ? "0" : objectToString(obj);
        }



        #endregion 执行SQL相关

        #region 转换工具

        /// <summary>
        /// DataSet 转成 XML
        /// </summary>
        /// <param name="xmlDS"></param>
        /// <returns></returns>
        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            System.IO.MemoryStream stream = null;
            System.Xml.XmlTextWriter writer = null;

            try
            {
                stream = new System.IO.MemoryStream();
                //从stream装载到XmlTextReader
                writer = new System.Xml.XmlTextWriter(stream, System.Text.Encoding.Unicode);

                //用WriteXml方法写入文件.
                xmlDS.WriteXml(writer, XmlWriteMode.WriteSchema);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                stream.Read(arr, 0, count);

                System.Text.UnicodeEncoding utf = new System.Text.UnicodeEncoding();
                return utf.GetString(arr).Trim();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        /// <summary>
        /// 组装json对象返回
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string getJSONFromDataTable(DataTable dt, string Status)
        {
            JsonArrayCollection resultJC = new JsonArrayCollection();
            try
            {
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        try
                        {
                            JsonObjectCollection temp = new JsonObjectCollection();
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                try
                                {
                                    string columnName = dt.Columns[i].ColumnName;
                                    temp.Add(new JsonStringValue(columnName, dr[columnName].ToString()));
                                }
                                catch { }
                            }
                            resultJC.Add(temp);
                        }
                        catch
                        { }
                    }
                }
            }
            catch { }
            JsonObjectCollection json = new JsonObjectCollection();
            json.Add(new JsonStringValue("Status", Status));
            json.Add(new JsonArrayCollection("Result", resultJC));
            return json.ToString();
        }

        /// <summary>
        /// 根据路径获取当前网站的绝对路径
        /// </summary>
        /// <param name="path">相对路径</param>
        public static string GetAbsoluteUriOfCurrentServer(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (!path.Contains(@"http://"))
                {
                    string absoluteUri = BuildAbsolute(path);
                    return absoluteUri;
                }
            }
            return path;
        }

        /// <summary>
        /// 将对象转换成JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJSONFromObjects(Object obj)
        {
            StringBuilder sbResult = new StringBuilder();
            try
            {
                if (obj != null)
                {
                    Newtonsoft.Json.JsonSerializerSettings settings = new JsonSerializerSettings();
                    var tt = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settings);
                    tt = tt.Replace("\r\n", "");
                    sbResult.Append(tt);
                }
            }
            catch { }
            return sbResult.ToString();
        }

        #endregion 转换工具

    }
}

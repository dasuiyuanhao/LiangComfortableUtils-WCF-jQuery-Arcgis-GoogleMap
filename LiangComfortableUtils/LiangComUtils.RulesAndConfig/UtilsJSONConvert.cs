using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Data;
using System.IO;

namespace LiangComUtils.RulesAndConfig
{
    public class UtilsJSONConvert
    {
        /// <summary>
        /// 将对象转换成JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJSONFromObject(Object obj)
        {
            StringBuilder sbResult = new StringBuilder();
            try
            {
                if (obj != null)
                {
                    Newtonsoft.Json.JsonSerializerSettings settings = new JsonSerializerSettings();
                    //var tt = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settings);
                    var tt = JsonConvert.SerializeObject(obj, settings);
                    sbResult.Append(tt);
                }
            }
            catch { }
            return sbResult.ToString();
        }

        /// <summary>
        /// 将对象转换成JSON字符串
        /// 忽略为null的属性或者字段
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJSONFromObjectWithBlankSpaceTab(Object obj)
        {
            StringBuilder sbResult = new StringBuilder();
            try
            {
                if (obj != null)
                {
                    Newtonsoft.Json.JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    var tt = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settings);
                    //tt = tt.Replace("\r\n", "");
                    sbResult.Append(tt);
                }
            }
            catch { }
            return sbResult.ToString();
        }

        /// <summary>
        /// 将对象转换成JSON字符串
        /// 忽略为null的属性或者字段
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJSONFromObjectsIgnoreNull(Object obj)
        {
            StringBuilder sbResult = new StringBuilder();
            try
            {
                if (obj != null)
                {
                    Newtonsoft.Json.JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    var tt = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settings);
                    //tt = tt.Replace("\r\n", "");
                    sbResult.Append(tt);
                }
            }
            catch { }
            return sbResult.ToString();
        }

        /// <summary>
        /// 将DataTable转换成JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetJSONFromDataTable(DataTable dt)
        {
            StringBuilder sbResult = new StringBuilder();
            try
            {
                if (dt != null)
                {
                    StringWriter sw=new StringWriter(sbResult);
                    using(Newtonsoft.Json.JsonTextWriter jtw=new JsonTextWriter(sw))
                    {
                        Newtonsoft.Json.JsonSerializerSettings settings = new JsonSerializerSettings();

                        Newtonsoft.Json.Converters.DataTableConverter dtc = new Newtonsoft.Json.Converters.DataTableConverter();
                        dtc.WriteJson(jtw, dt,JsonSerializer.Create(settings));
                        jtw.Flush();
                        sw.Close();
                    }
                }
            }
            catch { }
            return sbResult.ToString();
        }

        /// <summary>
        /// 将转JSON字符串换成对象
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static Object GetObjectFromJSON(string strData)
        {
            try
            {
                if (strData != null)
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    var result = JsonConvert.DeserializeObject(strData, settings);
                    return result;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// 将转JSON字符串换成对象
        /// 泛型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static T GetObjectFromJSON<T>(string strData)
        {
            try
            {
                if (strData != null)
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    var result = JsonConvert.DeserializeObject<T>(strData, settings);
                    return result;
                }
            }
            catch { }
            return default(T);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace LiangComUtils.Common
{
    public partial class UtilsCommon
    {
        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }

        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (result == "127.0.0.1")
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
                if (result == "")
                { result = "127.0.0.1"; }
            }

            return result;

        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }

            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
            {
                return HttpContext.Current.Request.Cookies[strName][key].ToString();
            }

            return "";
        }

        #region 操作cookie的方法,带有默认时间的

        /// <summary>
        /// 创建cookie值
        /// 默认三天有效期
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">cookie值</param>
        public static void CreateCookieValue(string cookieName, string cookieValue)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = cookieValue;
            cookie.Expires = DateTime.Now.AddDays(3);//默认3天
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 创建cookie值
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="cookieValue">子信息cookie名称</param>
        /// <param name="subCookieValue">子信息cookie值</param>
        public static void CreateCookieValue(string cookieName, string subCookieName, string subCookieValue)
        {
            DateTime cookieTime = DateTime.Now.AddDays(3);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
                cookie[subCookieName] = subCookieValue;
                cookie.Expires = cookieTime;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                //判断原子cookie是否存在
                if (cookie[subCookieName] == null)
                {
                    cookie.Values.Add(subCookieName, subCookieValue);
                    cookie.Expires = cookieTime;
                }
                else
                {
                    cookie[subCookieName].Remove(0);
                    cookie[subCookieName] = subCookieValue;
                    cookie.Expires = cookieTime;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        ///<summary>
        ///创建cookie值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>
        ///<param name="cookieValue">cookie值</param>
        ///<param name="cookieTime">cookie有效时间</param>
        public static void CreateCookieValue(string cookieName, string cookieValue, DateTime cookieTime)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Value = cookieValue;
            cookie.Expires = cookieTime;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        ///<summary>
        ///创建cookie值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>   
        ///<param name="cookieValue">cookie值(如果为空则使用以前的值)</param>
        ///<param name="subCookieName">子信息cookie名称</param>
        ///<param name="subCookieValue">子信息cookie值</param>
        ///<param name="cookieTime">cookie有效时间</param>
        private void CreateCookieValue(string cookieName, string cookieValue, string subCookieName, string subCookieValue, DateTime cookieTime)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookieName);
                cookie.Value = cookieValue;
                cookie[subCookieName] = subCookieValue;
                cookie.Expires = cookieTime;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                if (!string.IsNullOrEmpty(cookieValue))
                {
                    cookie.Value = cookieValue;
                }
                //判断原子cookie是否存在
                if (cookie[subCookieName] == null)
                {
                    cookie[subCookieName] = subCookieValue;
                    cookie.Expires = cookieTime;
                }
                else
                {
                    cookie[subCookieName].Remove(0);
                    cookie[subCookieName] = subCookieValue;
                    cookie.Expires = cookieTime;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        ///<summary>
        ///取得cookie的值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>
        ///<returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            string cookieValue = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (null == cookie)
            {
                cookieValue = "";
            }
            else
            {
                cookieValue = cookie.Value;
            }
            return cookieValue;
        }

        ///<summary>
        ///取得cookie的值
        ///</summary>
        ///<param name="cookieName">cookie名称</param>
        ///<param name="subCookieName">子信息cookie名称</param>
        ///<returns></returns>
        public static string GetCookieValue(string cookieName, string subCookieName)
        {
            string cookieValue = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (null == cookie)
            {
                cookieValue = "";
            }
            else
            {
                cookieValue = cookie.Value;
                string[] words = cookieValue.Split('&');
                foreach (string s in words)
                {
                    if (s.IndexOf(subCookieName + "=") >= 0)
                    {
                        cookieValue = s.Split('=')[1];
                    }
                }
            }
            return cookieValue;
        }

        ///<summary>
        ///删除某个固定的cookie值[此方法一是在原有的cookie上再创建同样的cookie值，但是时间是过期的时间]
        ///</summary>
        ///<param name="cookieName"></param>
        public static void RemoveCookieValue(string cookieName)
        {
            string dt = "1900-01-01 12:00:00";
            CreateCookieValue(cookieName, "", Convert.ToDateTime(dt));
        }

        #endregion 操作cookie的方法

        #region http请求

        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFileFullPath(string strName)
        {
            if (HttpContext.Current.Server.MapPath(strName) == null)
            {
                return "";
            }
            return HttpContext.Current.Server.MapPath(strName);
        }

        /// <summary>
        /// 过滤html标签
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string FilterHtmlStr(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 
            html = regex6.Replace(html, ""); //过滤frameset 
            html = regex7.Replace(html, ""); //过滤frameset 
            html = regex8.Replace(html, ""); //过滤frameset 
            html = regex9.Replace(html, "");
            //html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }

        /// <summary>
        /// URL路径编码
        /// </summary>
        /// <returns></returns>
        public static string UrlEncode(string strHtml)
        {
            return HttpUtility.UrlEncode(strHtml, Encoding.Default);
        }

        /// <summary>
        /// URL路径解码
        /// </summary>
        /// <returns></returns>
        public static string UrlDecode(string strHtml)
        {
            return HttpUtility.UrlDecode(strHtml, Encoding.Default);
        }

        /// <summary>
        /// 创建绝对路径地址
        /// </summary>
        /// <param name="relativeUri"></param>
        /// <returns></returns>
        public static string BuildAbsolute(string relativeUri)
        {
            // 取得当前uri
            Uri uri = HttpContext.Current.Request.Url;
            // 获得绝对路径
            string app = HttpContext.Current.Request.ApplicationPath;
            //末尾(无"/")增加"/"
            if (!app.EndsWith("/")) app += "/";
            relativeUri = relativeUri.TrimStart('/');
            // 返回绝对路径
            return HttpUtility.UrlPathEncode(
              String.Format("http://{0}:{1}{2}{3}",
              uri.Host, uri.Port, app, relativeUri));
        }
        

        #endregion http请求        

        #region Web相关

        public static string RemoveHtmlTag(string str)
        {
            // Added by entlib@boy, Sept. 26, 2007
            str = str.Replace("'", "&rsquo;").Replace("\"", "&quot;");

            Regex reg = new Regex(@"<\/*[^<>]*>");
            return reg.Replace(str, "");
        }

        /// <summary>
        /// 判断是否为合法的数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidNumber(string value)
        {
            return Regex.IsMatch(value, @"^\d*$");
        }


        /// <summary>
        /// 将一个新的字符串添加到旧字符串的的最前面，并且将旧字符串中含有与新字符串相同字符串的去掉，
        /// 分隔符为自定义
        /// </summary>
        /// <param name="insertString"></param>
        /// <param name="oldString"></param>
        /// <param name="compartChar"></param>
        /// <returns></returns>
        public static string SortWithInsertNewString(string insertString, string oldString, char compartChar)
        {
            string[] oldStringList = oldString.Split(new char[] { compartChar });
            string newString = "";
            ArrayList newStringList = new ArrayList();

            newStringList.Add(insertString);

            for (int i = 0; i < oldStringList.Length; i++)
            {
                if (oldStringList[i] != insertString)
                {
                    newStringList.Add(oldStringList[i]);
                }
            }

            for (int i = 0; i < newStringList.Count; i++)
            {
                if (newString == "")
                {
                    newString = newStringList[i].ToString();
                }
                else
                {
                    newString += compartChar.ToString() + newStringList[i].ToString();
                }
                if (i == 5)
                {
                    break;
                }
            }
            return newString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetAbsoluteFilePath(string filePath)
        {
            string file = filePath;
            if (!filePath.Substring(1, 1).Equals(":")
                && !filePath.StartsWith("\\"))
            {
                file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            }

            return file.Replace("/", "\\");
        }

        #endregion Web相关
    }
}

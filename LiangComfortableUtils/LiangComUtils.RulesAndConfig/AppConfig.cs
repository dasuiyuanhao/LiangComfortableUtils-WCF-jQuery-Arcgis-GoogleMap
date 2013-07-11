using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LiangComUtils.RulesAndConfig
{
    public class AppConfig
    {
        private AppConfig()
        {
        }

        public static string EntLibCompanyName
        {
            get
            {
                return ConfigurationManager.AppSettings["EntLibCompanyName"];
            }
        }

        public static string EntLibWebSiteURL
        {
            get
            {
                string strUrl = ConfigurationManager.AppSettings["EntLibWebSiteURL"];
                char[] charsToTrim = { '/' };

                if (strUrl.EndsWith("/"))
                    return strUrl.TrimEnd(charsToTrim);
                else
                    return strUrl;
            }
        }

        public static string EntLibECVersion
        {
            get
            {
                return ConfigurationManager.AppSettings["EntLibECVersion"];
            }
        }

        public static string ConnectionString
        {
            get
            {
                //return ConfigurationManager.AppSettings["ConnectionString"];
                return ConfigurationManager.ConnectionStrings["MSSQLComfortableUtils"].ConnectionString;
            }
        }

        public static string ErrorLogFolder
        {
            get
            {
                return ConfigurationManager.AppSettings["ErrorLogFolder"];
            }
        }


        #region Web配置

        /// <summary>
        /// 用户默认头像地址
        /// </summary>
        public static string DefaultUserPhoto = ConfigurationManager.AppSettings["DefaultUserPhotoPathUrl"];


        #endregion Web配置


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace LiangComUtils.RulesAndConfig
{
    public class ConstantsWeb
    {
        #region 私密常量

        /// <summary>
        /// 默认加密解密的Key
        /// </summary>
        public const string DefaultEncryptKey = "liangxi";

        #endregion 私密常量

        #region Session相关常量

        public const string SessionKeyCurrentLoginingUserInfo = "SessionKeyCurrentLoginingUserInfo";



        #endregion Session相关常量

        #region Cookie相关常量

        public const string CookieKeyCurrentUserID = "CurrentUserID";
        public const string CookieKeyCurrentUserName = "CurrentUserName";
        public const string CookieKeyCurrentUserEmail = "CurrentUserEmail";
        public const string CookieKeyCurrentUserPhotoUrl = "CurrentUserPhotoUrl";
        public const string CookieKeyCurrentUserLevel = "CurrentUserLevel";

        /// <summary>
        /// Cookie加密解密的Key
        /// </summary>
        public const string CookieEncryptKey = "LIANGXI";

        #endregion Cookie相关常量

        #region 公用常量

        



        #endregion 公用常量
    }
}

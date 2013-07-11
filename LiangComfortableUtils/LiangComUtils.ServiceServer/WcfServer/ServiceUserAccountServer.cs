using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiangComUtils.Models.ServiceData;
using LiangComUtils.RulesAndConfig;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace LiangComUtils.ServiceServer.WcfServer
{
    public class ServiceUserAccountServer : IRequiresSessionState
    {
        #region 用户注册

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        public static string UserAccountRegister(string strNewUserInfo)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                Models.User.UserInfo newUserInfo = UtilsJSONConvert.GetObjectFromJSON<Models.User.UserInfo>(strNewUserInfo);
                if (newUserInfo == null)
                {
                    result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "注册信息为空");
                    //线程睡眠一秒
                    Thread.Sleep(1000);
                    return UtilsJSONConvert.GetJSONFromObject(result);
                }
                
                newUserInfo.UserName = Common.UtilsCommon.UrlDecode(newUserInfo.UserName);
                newUserInfo.Email = Common.UtilsCommon.UrlDecode(newUserInfo.Email);
                newUserInfo.Psw = Common.UtilsCommon.UrlDecode(newUserInfo.Psw);
                //检查注册信息                
                if (!CheckUserAccountRegister(newUserInfo,ref result))
                {
                    //线程睡眠一秒
                    Thread.Sleep(1000);
                    return UtilsJSONConvert.GetJSONFromObject(result);
                }
                //入库
                Business.UserAccountBiz uab = new Business.UserAccountBiz();
                int newSysID=uab.AddUserInfo(newUserInfo);
                if (newSysID == -1 || newSysID == 0)
                {
                    result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "存入数据库失败");
                    return UtilsJSONConvert.GetJSONFromObject(result);
                }
                //返回结果,从库里面再查一遍
                Models.User.UserInfo successUI = uab.GetModelUserInfo(newSysID);
                if (successUI == null)
                {
                    result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "存入数据库失败");
                    return UtilsJSONConvert.GetJSONFromObject(result);
                }
                if (!string.Equals(successUI.UserName, newUserInfo.UserName) || !string.Equals(successUI.Email, newUserInfo.Email))
                {
                    result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "存入数据库失败");
                    return UtilsJSONConvert.GetJSONFromObject(result);
                }
                //设置注册成功的返回结果
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusOK, successUI, "Congratulation!");                
            }
            catch(Exception e)
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, e.Message);
            }
            return UtilsJSONConvert.GetJSONFromObject(result);
        }

        /// <summary>
        /// 检查注册用户信息，并赋值
        /// 此为后台验证，true为验证通过
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool CheckUserAccountRegister(Models.User.UserInfo newUserInfo,ref ResponseResult result)
        {
            if (newUserInfo == null)
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "转换用户信息出错");
                return false;
            }
            if (string.IsNullOrEmpty(newUserInfo.UserName))
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "没有用户名");
                return false;
            }
            if (newUserInfo.UserName.Length < 4)
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "用户名太短");
                return false;
            }
            if (newUserInfo.UserName.Length>400)
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "用户名超出长度限制");
                return false;
            }
            if (string.IsNullOrEmpty(newUserInfo.Psw))
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "没有填写密码");
                return false;
            }
            if (newUserInfo.Psw.Length>100)
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "密码超出长度限制");
                return false;
            }
            //检查邮箱地址格式
            if (string.IsNullOrEmpty(newUserInfo.Email))
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "邮箱地址不能为空");
                return false;
            }
            if (newUserInfo.Email.Length > 400)
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "邮箱地址超出长度限制");
                return false;
            }
            if (!Common.UtilsCommon.IsEmailAddress(newUserInfo.Email))
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "邮箱地址格式不正确");
                return false;
            }
            //判断手机号码格式，可以为空,如果手机号格式不正确，则设置为null
            if (!string.IsNullOrEmpty(newUserInfo.Cellphone))
            {
                if (!Common.UtilsCommon.IsCellNumber(newUserInfo.Cellphone))
                {
                    newUserInfo.Cellphone = null;
                }
            }
            //判断身份证号码格式，可以为空,如果格式不正确，则设置为null
            if (!string.IsNullOrEmpty(newUserInfo.CardNo))
            {
                if (!Common.UtilsCommon.IsNumber(newUserInfo.CardNo))
                {
                    newUserInfo.CardNo = null;
                }
            }

            //最重要的
            //判断用户名是否已经注册，判断邮箱是否已经注册
            LiangComUtils.Business.UserAccountBiz uab = new Business.UserAccountBiz();
            if (uab.ExistsUserNameOrEmail(newUserInfo.UserName, newUserInfo.Email))
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR, null, "用户名称或者邮箱已经存在");
                return false;
            }
            //设置注册时间和修改时间
            newUserInfo.RegisterTime = DateTime.Now;
            newUserInfo.UpdateTime = DateTime.Now;
            //设置默认头像
            if (string.IsNullOrEmpty(newUserInfo.Photo))
            {
                newUserInfo.Photo = AppConfig.DefaultUserPhoto;
            }

            return true;
        }

        /// <summary>
        /// 检查用户名或者Email地址是否已经注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string CheckIsUserNameOrEmailRegistered(string name, string email)
        {
            ResponseResult result = new ResponseResult();
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email))
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusERROR,
                    "0","用户名和邮箱不能都为空");
                return UtilsJSONConvert.GetJSONFromObject(result);
            }
            //判断用户名是否已经注册，判断邮箱是否已经注册
            name = LiangComUtils.Common.UtilsCommon.UrlDecode(name);
            email = LiangComUtils.Common.UtilsCommon.UrlDecode(email);
            LiangComUtils.Business.UserAccountBiz uab = new Business.UserAccountBiz();
            if (uab.ExistsUserNameOrEmail(name, email))
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusOK,
                    "1", "已存在");
            }
            else
            {
                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusOK,
                    "0");
            }
            return UtilsJSONConvert.GetJSONFromObject(result);
        }


        #endregion 用户注册


        #region 用户登录相关

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginUserInfo"></param>
        /// <returns></returns>
        public static string UserLogin(string strLoginUserInfo)
        {
            Models.ServiceData.ResponseResult backResponse = new ResponseResult();
            if (string.IsNullOrEmpty(strLoginUserInfo))
            {
                backResponse.message = "输入的登录用户信息为空";
                backResponse.status = Models.Enums.EnumServiceResponseResultStatus.StatusERROR;
                return UtilsJSONConvert.GetJSONFromObject(backResponse);
            }
            try
            {
                Models.User.UserInfo loginUI = UtilsJSONConvert.GetObjectFromJSON<Models.User.UserInfo>(strLoginUserInfo);
                if (loginUI == null)
                {
                    backResponse.message = "转换登录用户信息出错";
                    backResponse.status = Models.Enums.EnumServiceResponseResultStatus.StatusERROR;
                    return UtilsJSONConvert.GetJSONFromObject(backResponse);
                }
                if (string.IsNullOrEmpty(loginUI.UserName) && string.IsNullOrEmpty(loginUI.Email))
                {
                    backResponse.message = "用户名或者邮箱不能为空";
                    backResponse.status = Models.Enums.EnumServiceResponseResultStatus.StatusERROR;
                    return UtilsJSONConvert.GetJSONFromObject(backResponse);
                }
                if(string.IsNullOrEmpty(loginUI.Psw))
                {
                    backResponse.message = "密码不能为空";
                    backResponse.status = Models.Enums.EnumServiceResponseResultStatus.StatusERROR;
                    return UtilsJSONConvert.GetJSONFromObject(backResponse);
                }
                //执行用户登陆
                return DoLogin(loginUI);
            }
            catch { }
            return UtilsJSONConvert.GetJSONFromObject(backResponse);
        }

        /// <summary>
        /// 执行用户登录
        /// </summary>
        /// <param name="loginUI"></param>
        public static string DoLogin(Models.User.UserInfo loginUI)
        {
            Models.ServiceData.ResponseResult backResponse = new ResponseResult();
            if (string.IsNullOrEmpty(loginUI.UserName) && string.IsNullOrEmpty(loginUI.Email))
            {
                backResponse.message = "用户名或者邮箱不能为空";
                backResponse.status = Models.Enums.EnumServiceResponseResultStatus.StatusERROR;
                return UtilsJSONConvert.GetJSONFromObject(backResponse);
            }
            if (string.IsNullOrEmpty(loginUI.Psw))
            {
                backResponse.message = "密码不能为空";
                backResponse.status = Models.Enums.EnumServiceResponseResultStatus.StatusERROR;
                return UtilsJSONConvert.GetJSONFromObject(backResponse);
            }
            Business.UserAccountBiz uab=new Business.UserAccountBiz();
            IList<Models.User.UserInfo> userInfos = uab.GetUserInfoListByUserNameOrEmailAndPsw(loginUI.UserName, loginUI.Email, loginUI.Psw);
            if (userInfos != null)
            {
                if (userInfos.Count > 0)
                {
                    if (userInfos.Count > 1)
                    {
                        backResponse.status = Models.Enums.EnumServiceResponseResultStatus.StatusERROR;
                        backResponse.message = "您的账户有问题，存在多个重复账户，请与网站管理员联系";
                    }
                    else
                    {
                        Models.User.UserInfo currentUI = userInfos[0];
                        if (currentUI.Status != 0)
                        {
                            backResponse.status = Models.Enums.EnumServiceResponseResultStatus.StatusERROR;
                            backResponse.message = "您的账户已经被锁定，请与网站管理员联系";
                            return UtilsJSONConvert.GetJSONFromObject(backResponse); ;
                        }
                        currentUI.Psw = "";
                        if(string.IsNullOrEmpty(currentUI.Photo)) //用户头像默认地址
                        {
                            currentUI.Photo=AppConfig.DefaultUserPhoto;
                        }
                        //设置用户头像的绝对地址
                        currentUI.Photo = Common.UtilsCommon.GetAbsoluteUriOfCurrentServer(currentUI.Photo);
                        backResponse.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusOK, currentUI);
                        
                        //初始化登录数据
                        LoginInitializeData(currentUI);
                        
                    }
                }
            }
            return UtilsJSONConvert.GetJSONFromObject(backResponse);
        }

        /// <summary>
        /// 初始化登陆用户的数据
        /// </summary>
        /// <param name="loginUI"></param>
        /// <returns></returns>
        public static void LoginInitializeData(Models.User.UserInfo currentUI)
        {
            //保存Session和Cookie
            HttpContext.Current.Session[ConstantsWeb.SessionKeyCurrentLoginingUserInfo] = currentUI;
            //保存Cookie
            //加密字符串
            //string encodeuserid = HttpUtility.UrlEncode(Common.UtilsEncryptionDES.DesEncrypt(currentUI.SysID.ToString(),ConstantsWeb.CookieEncryptKey));
            string encodeuserid = HttpUtility.UrlEncode(currentUI.SysID.ToString());
            string encodeusername = HttpUtility.UrlEncode(currentUI.UserName);
            string encodeuseremail = HttpUtility.UrlEncode(Common.UtilsEncryptionDES.DesEncrypt(currentUI.Email, ConstantsWeb.CookieEncryptKey));
            string encodeuserphoto = HttpUtility.UrlEncode(currentUI.Photo);
            //设置cookie过期时间
            DateTime datetimecookie = DateTime.Now.AddDays(7);
            //保存Cookie用户ID
            Common.UtilsCommon.CreateCookieValue(ConstantsWeb.CookieKeyCurrentUserID, encodeuserid, datetimecookie);
            //保存Cookie用户名
            Common.UtilsCommon.CreateCookieValue(ConstantsWeb.CookieKeyCurrentUserName, encodeusername, datetimecookie);
            //保存CookieEmail
            Common.UtilsCommon.CreateCookieValue(ConstantsWeb.CookieKeyCurrentUserEmail, encodeuseremail, datetimecookie);
            //保存Cookie用户头像地址
            Common.UtilsCommon.CreateCookieValue(ConstantsWeb.CookieKeyCurrentUserPhotoUrl, encodeuserphoto, datetimecookie);
            //保存Cookie用户Level
            //Common.UtilsCommon.CreateCookieValue(ConstantsWeb.CookieKeyCurrentUserLevel, encodeuserid, datetimecookie);
            
        }



        #endregion 用户登录相关


        #region 检查用户登录

        /// <summary>
        /// 检查用户是否登录
        /// </summary>
        /// <returns></returns>
        public static bool CheckIsLogining()
        {
            bool flag = false;            
            //如果session失效，则根据cookie登陆刷新
            if (HttpContext.Current.Session[ConstantsWeb.SessionKeyCurrentLoginingUserInfo] == null)
            {
                //取出cookie中的用户信息
                Models.User.UserInfo cookieUI = GetCurrentUserInfoFromCookie();
                if (cookieUI == null)
                {
                    return false;
                }
                if (cookieUI.SysID<1||string.IsNullOrEmpty(cookieUI.UserName) || string.IsNullOrEmpty(cookieUI.Email))
                {
                    return false;
                }
                //验证用户
                Business.UserAccountBiz uab = new Business.UserAccountBiz();
                IList<Models.User.UserInfo> getListUI = uab.GetUserInfoListBySysIDAndUserNameAndEmail(cookieUI.SysID, cookieUI.UserName, cookieUI.Email);
                if (getListUI.Count != 1)
                {
                    return false;
                }
                Models.User.UserInfo currrentUI=getListUI[0];
                if (string.IsNullOrEmpty(currrentUI.UserName))
                {
                    return false;
                }
                if (currrentUI.Status > 0)
                {
                    return false;
                }
                //重新初始化登录信息
                LoginInitializeData(currrentUI);
                flag = true;
            }
            else
            {
                string hd_UserID = GetCurrentUserIDFromCookie();
                if (string.IsNullOrEmpty(hd_UserID))
                {
                    HttpContext.Current.Session[ConstantsWeb.SessionKeyCurrentLoginingUserInfo] = null;
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// 从Cookie中获得用户信息
        /// </summary>
        /// <returns></returns>
        public static Models.User.UserInfo GetCurrentUserInfoFromCookie()
        {
            Models.User.UserInfo result = null;
            try
            {
                string userid = GetCurrentUserIDFromCookie();
                if (string.IsNullOrEmpty(userid))
                {
                    return result;
                }
                result.SysID = Int32.Parse(userid);
                result.UserName = GetCurrentUserNameFromCookie();
                result.Email = GetCurrentUserEmailFromCookie();
            }
            catch { }
            return result;
        }

        /// <summary>
        /// 返回当前登录的用户Name(已解密)
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserIDFromCookie()
        {
            string curretnuserid = string.Empty;
            string cookievalue = Common.UtilsCommon.GetCookieValue(ConstantsWeb.CookieKeyCurrentUserID);
            ////解密
            //if (!string.IsNullOrEmpty(cookievalue))
            //{
            //    curretnusername = CommonEncryptionDES.DesDecrypt(HttpUtility.UrlDecode(cookievalue), WEBConstants.COOKIE_EncryptKey);
            //}
            curretnuserid = HttpUtility.UrlDecode(cookievalue);
            return curretnuserid;
        }

        /// <summary>
        /// 返回当前登录的用户Name(已解密)
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserNameFromCookie()
        {
            string curretnusername = string.Empty;
            string cookievalue =Common.UtilsCommon.GetCookieValue(ConstantsWeb.CookieKeyCurrentUserName);
            ////解密
            //if (!string.IsNullOrEmpty(cookievalue))
            //{
            //    curretnusername = CommonEncryptionDES.DesDecrypt(HttpUtility.UrlDecode(cookievalue), WEBConstants.COOKIE_EncryptKey);
            //}
            curretnusername = HttpUtility.UrlDecode(cookievalue);
            return curretnusername;
        }

        /// <summary>
        /// 返回当前登录的用户Email(已解密)
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserEmailFromCookie()
        {
            string curretnuseremail = string.Empty;
            string cookievalue = Common.UtilsCommon.GetCookieValue(ConstantsWeb.CookieKeyCurrentUserEmail);
            //解密
            if (!string.IsNullOrEmpty(cookievalue))
            {
                curretnuseremail = Common.UtilsEncryptionDES.DesDecrypt(HttpUtility.UrlDecode(cookievalue), ConstantsWeb.CookieEncryptKey);
            }
            return curretnuseremail;
        }

        #endregion 检查用户登录


        #region 注销用户登录相关

        /// <summary>
        /// 注销登录
        /// </summary>
        public static string CancelLogining()
        {
            ResponseResult result = new ResponseResult();
            try
            {
                HttpContext.Current.Session[ConstantsWeb.SessionKeyCurrentLoginingUserInfo] = null;
                //清除Cookie中登录信息
                //Cookie用户ID
                Common.UtilsCommon.RemoveCookieValue(ConstantsWeb.CookieKeyCurrentUserID);
                //Cookie用户名
                Common.UtilsCommon.RemoveCookieValue(ConstantsWeb.CookieKeyCurrentUserName);
                //CookieEmail
                Common.UtilsCommon.RemoveCookieValue(ConstantsWeb.CookieKeyCurrentUserEmail);
                //Cookie用户头像地址
                Common.UtilsCommon.RemoveCookieValue(ConstantsWeb.CookieKeyCurrentUserPhotoUrl);

                result.SetResult(Models.Enums.EnumServiceResponseResultStatus.StatusOK, null);
            }
            catch { }
            return UtilsJSONConvert.GetJSONFromObject(result);
        }

        #endregion 注销用户登录相关

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using LiangComUtils.Models.ServiceData;
using LiangComUtils.ServiceServer.WcfServer;

namespace LiangComUtils.Web.Service
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceUserAccount
    {
        // 要使用 HTTP GET，请添加 [WebGet] 特性。(默认 ResponseFormat 为 WebMessageFormat.Json)
        // 要创建返回 XML 的操作，
        //     请添加 [WebGet(ResponseFormat=WebMessageFormat.Xml)]，
        //     并在操作正文中包括以下行:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebGet]
        public string DoWork()
        {
            // 在此处添加操作实现
            return "heheh";
        }

        [OperationContract]
        [WebGet]
        public string TestUserAccountRegister()
        {
            ResponseResult result = new ResponseResult(new Models.User.UserInfo() { CardNo = "111", SysID = 1, UpdateTime = DateTime.Now });
            return RulesAndConfig.UtilsJSONConvert.GetJSONFromObject(result);
        }

        // 在此处添加更多操作并使用 [OperationContract] 标记它们

        /// <summary>
        /// 用户注册账号
        /// </summary>
        /// <param name="newUserInfo"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet]
        public string UserAccountRegister(string newUserInfo)
        {
            return ServiceUserAccountServer.UserAccountRegister(newUserInfo);
        }

        /// <summary>
        /// 检查用户名或者Email地址是否已经注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns>bool</returns>
        [OperationContract]
        [WebGet]
        public string CheckIsUserNameOrEmailRegistered(string name, string email)
        {
            return ServiceUserAccountServer.CheckIsUserNameOrEmailRegistered(name, email);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginUserInfo"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet]
        public string UserLogin(string loginUserInfo)
        {
            return ServiceUserAccountServer.UserLogin(loginUserInfo);
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet]
        public string CancelLogining()
        {
            return ServiceUserAccountServer.CancelLogining();
        }
    }
}

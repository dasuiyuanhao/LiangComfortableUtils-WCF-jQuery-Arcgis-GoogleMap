using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using LiangComUtils.Models.ServiceData;

namespace LiangComUtils.WcfService.UserAccount
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
            return "hehe";
        }

        // 在此处添加更多操作并使用 [OperationContract] 标记它们

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet]
        public string UserAccountLogin()
        {
            // 在此处添加操作实现
            return "hehe";
        }


        [OperationContract]
        [WebGet]
        public ResponseResult UserAccountRegister()
        {
            ResponseResult result = new ResponseResult(new Models.User.UserInfo() { CardNo="111",SysID=1,UpdateTime=DateTime.Now});
            
            

            return result;
        }
    }
}

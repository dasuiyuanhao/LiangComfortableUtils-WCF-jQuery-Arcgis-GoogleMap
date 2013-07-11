/*
登录弹出框
使用方法：
1.引用该js；
2.调用方法弹出登陆框。
*/
var CurrentLoginingUser = null;
var Link;
$(function () { //初始化登录信息
    InitializeCurrentUserInfoData();
});
//显示登录框
function ShowLoginBox(link) {
    try {
        /*
        if ($("#divLoginBox").length == 0) {
            $("body")
		        .append('<div id="divLoginBox" style="width:400px;height:160px;z-index:1000;display:none;">\
                    <div id="divLopLoginBox" style="width:400px;height:20px;"></div>\
                        <div id="divContentLoginBox" style="width:400px;height:140px;">\
                            <ul class="pqwjg_right">\
        	                    <li class="kslfbvv">会员登录</li>\
                                <li><label>账号：</label><input id="inputUserName" type="text" maxlength="100" class="kskrngg" value="请输入昵称" onblur="if(this.value==\'\'){this.value=\'请输入昵称\';}" onfocus="if(this.value==\'请输入昵称\'){this.value=\'\';}" /></li>\
                                <li><label>密码：</label><input id ="inputPSW" maxlength="100" type="password" class="kskrngg" /></li>\
                                <li class="kstnrn"><label></label><input id ="rememberUserNameAndPSW" type="checkbox" />&nbsp;&nbsp;记住我下次自动登录</li><input type="button" id="buttonLogin" value="登录" onclick="login();" style="cursor:pointer;" />\
                                <li class="jsfujfr"><label></label>\
                                    <a href="#">忘记密码</a></li>\
                                <li class="nsrskr">还没有图途账号？</li>\
                                <li class="jstrbhss"><a href="register.aspx">立刻注册</a></li>\
                            </ul>\
                        </div>\
                    </div>');
        }
        */
        //弹出
        //popupDiv("divLoginBox");

        var h = window.document.documentElement.scrollHeight > window.document.documentElement.clientHeight ? window.document.documentElement.scrollHeight : window.document.documentElement.clientHeight; 
        document.getElementById("lele").style.height = h + 'px';
        document.getElementById("lele").style.width = window.document.documentElement.clientWidth + 'px';
        showLoginOrRegister("1");
        var showDiv = document.getElementById("showDiv");
        showDiv.style.display = "block";
        if (link != null) {
            Link = link;
        }
    } catch (e) {
        alert(e);
    }
}
//隐藏登录框
function HideLoginBox() {
    var showDiv = document.getElementById("showDiv");
    showDiv.style.display = "none";
    //window.location.reload();
}

//切换注册和登录
function showLoginOrRegister(str) {
    if (str == "1") {
        $("#aTitleLogin").addClass("ahover");
        $("#aTitleRegister").removeClass("ahover");
        $("#divBodyLogin").show();
        $("#divBodyRegister").hide();
    }
    else {
        $("#aTitleRegister").addClass("ahover");
        $("#aTitleLogin").removeClass("ahover");
        $("#divBodyRegister").show();
        $("#divBodyLogin").hide();
    }
}
//绑定方法到点击登录按钮,判断登录后，执行方法参数
function BindMethodToClickLoginButtion(Handler) {
    if ($.isFunction(Handler)) {
        $("#buttonLogin").bind("click", Handler);
    }
}
//登录成功后响应的方法(测试)
function handlerliangtest() {
    if ($("input[definedid='hiddenLoginStatus']").val() == "true") {
        $("#buttonLogin").unbind("click", liangtest);
        alert("谢谢登录");
    }    
}
/*
function BindMethodToClickLoginButtion(loginedHandler) {
    if ($.isFunction(loginedHandler)) {

        $("#buttonLogin").bind("click", { param1: loginedHandler }, HandlerClickLoginButton);
    }
}
//定义绑定成功登录后的事件方法
var HandlerClickLoginButton = function (event) {
    if ($("input[definedid='hiddenLoginStatus']").val() == "true") {
        $("#buttonLogin").unbind("click", HandlerClickLoginButton);
        alert("hehe");
        var p = event.data.param1;
        p();
    }
}
*/

//登录
function login() {
    if (CheckIsUserNamePSWCorrect()) {
        //登录成功
        LoginSuccess();
    }
    else {
        //登录失败
        //alert('用户名或密码错误！');
    }
}

//登录框验证
function CheckIsUserNamePSWCorrect() {
    var jqInputUserName = $("#inputUserName");
    var jqInputPSW = $("#inputPSW");
    //检查是否非空
    if (jqInputUserName.val() == "" || jqInputUserName.val() == "请输入昵称") {
        alert("用户名不能为空！");
        return false;
    }
    if (!checkStringIsEmptyOrContainSpace(jqInputUserName.val())) {
        alert("用户名不能包含空格！");
        return false;
    }
    if (jqInputPSW.val() == "") {
        alert("密码不能为空！");
        return false;
    }
    if (!checkStringIsEmptyOrContainSpace(jqInputPSW.val())) {
        alert("密码不能包含空格！");
        return false;
    }
    //检查用户名密码
    if (checkStringIsEmptyOrContainSpace(jqInputUserName.val()) && checkStringIsEmptyOrContainSpace(jqInputPSW.val())) {
        var stringIsRemember = "";
        if ($("input[definedid='rememberUserNameAndPSW']").attr("checked") == true) {
            stringIsRemember = "true";
        }
        //var stringCheckIsUserNamePSWCorrectUrl = "../Service/Login.ashx?Type=1&UserName=" + encodeURIComponent(jqInputUserName.val()) + "&PSW=" + encodeURIComponent(jqInputPSW.val()) + "&IR=" + stringIsRemember + "&Random=" + Math.random();
        var stringCheckIsUserNamePSWCorrectUrl = "../../Service/ServiceUserAccount.svc/UserLogin?Random=" + Math.random();
        var willLoginUI=new LX.Models.UserInfo();
        willLoginUI.UserName=encodeURIComponent(jqInputUserName.val());
        willLoginUI.Email=encodeURIComponent(jqInputUserName.val());
        willLoginUI.Psw=encodeURIComponent(jqInputPSW.val());
        var result = "";
        $.ajax({ url: stringCheckIsUserNamePSWCorrectUrl,
            type: "get",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: { "loginUserInfo": JSON.stringify(willLoginUI) },
            async: false,
            success: function (data) {
                result = JSON.parse(data.d);
            }
        });
        if (result == null) {
            alert("登录出错！");
        }
        if (result.status == "OK") {
            if (result.contents != null) {
                if (result.contents.length > 0) {
                    CurrentLoginingUser = result.contents[0];
                }
            }
            return true;
        }
        else {
            alert(result.message);
            return false;
        }
    }
    else {
        return false;
    }
}

/*
//弹出div方法
function popupDiv(div_id) {
    var div_obj = $("#" + div_id);
//    var windowWidth = document.documentElement.clientWidth;
    //    var windowHeight = document.documentElement.clientHeight;
    var windowWidth = $(document).width();
    var windowHeight = $(document).height();
    var popupHeight = div_obj.height();
    var popupWidth = div_obj.width();
    //添加并显示遮罩层 
    $("<div id='mask' style='background-color:#FFFFFF;position:absolute;top:0px;left:0px;z-index:999;filter: Alpha(Opacity=60);-moz-opacity: 0.7;opacity:.70;filter: alpha(opacity=70);'></div>")
            //.width(windowWidth * 0.99)
    //.height(windowHeight * 0.99)
            .css({
                height: windowHeight,
                width: windowWidth,
                display: "block"
            })
            .click(function () { hideDiv(div_id); })
            .appendTo("body")
            .fadeIn(200);
    div_obj.css({ "position": "absolute" })
            .animate({ left: windowWidth / 2 - popupWidth / 2,
                //top: windowHeight / 2 - popupHeight / 2, 
                top: popupHeight * 2, 
                opacity: "show"
            }, "slow");
}
function hideDiv(div_id) {
    $("#mask").remove();
    $("#" + div_id).animate({ left: 0, top: 0, opacity: "hide" }, "slow");
}
*/

//登录成功
function LoginSuccess() {
    //InitializeCurrentUserInfoData();
    //隐藏弹出框
    //HideLoginBox();
    //刷新当前页面
    //window.location.href = window.location.href;
    if (Link != null) {
        window.location.href = Link;
        Link = null;
    }
    else {

        if (this.location.href.substring(this.location.href.lastIndexOf('/') + 1) == "Login.aspx") {
            window.location.href = "Personal.aspx";
        }
        else {
            window.location.reload();
        } 
    }

}
//初始化用户信息数据-从cookie读取用户信息
function InitializeCurrentUserInfoData() {
    try {
        var userID = $.cookie("CurrentUserID");
        if (LX.Func.IsStringObjectEmptyOrNull(userID)) {
            CurrentLoginingUser = null;
            $("#spanHeaderLoginingUser").hide();
            $("#aButtonHeaderLogin").show();
        }
        else { //已经登录
            var userName = $.cookie("CurrentUserName");
            var userPhotoUrl = $.cookie("CurrentUserPhotoUrl");
            CurrentLoginingUser = new LX.Models.UserInfo();
            CurrentLoginingUser.SysID = userID;
            CurrentLoginingUser.UserName = userName;
            CurrentLoginingUser.Photo = userPhotoUrl;
            //隐藏登录按钮
            $("#aButtonHeaderLogin").hide();
            //显示个人用户名
            var stringlipersonal = '<span id="userLogin" definedid="litoppersonnelmsg">\
                    <a href="../../Main/UserAccount/Personal.aspx">';
            stringlipersonal += CurrentLoginingUser.UserName;
            stringlipersonal += '</a>\
                    <a href="javascript:CancelLogin();" id="back" >退出</a>|\
                </span>';
            $("#spanHeaderLoginingUser").children().remove();
            $("#spanHeaderLoginingUser").append($(stringlipersonal));
            $("#spanHeaderLoginingUser").show();
        }
    }
    catch (e) {

    }  
}
//刷新当前用户的信息（隐藏字段）
function RefreshCurrentUserInfo(loginstatus, username, userphotourl) {
    $("input[definedid='hiddenLoginStatus']").val(loginstatus);
    $("input[definedid='hiddenCurrentUserName']").val(username);
    $("input[definedid='hiddenCurrentUserPhotoUrl']").val(userphotourl);
}

//注销登录
function CancelLogin() {
    //var stringCancelLoginUrl = "../Service/Login.ashx?Type=2&UserName=current&Random=" + Math.random();
    var stringCancelLoginUrl = "../../Service/ServiceUserAccount.svc/CancelLogining?Random=" + Math.random();
    var result = "";
    $.ajax({ url: stringCancelLoginUrl,
        type: "get",
        async: false,
        success: function (data) {
            result = JSON.parse(data.d);
        }
    });
    if (result.status == "OK") {
        InitializeCurrentUserInfoData();
        /*
        //隐藏个人账号，显示登录链接
        jqtoppersonnelmsg.hide();
        var stringlilogin='<li id="login" definedid="litoplogin">\
        <a id="aTopLogin" href="javascript:ShowLoginBox(null);">登录</a>|\
        </li>';
        jqtoppersonnelmsg.after($(stringlilogin));
        //更新隐藏字段
        RefreshCurrentUserInfo("false", "", "");
        */
        //刷新当前页面
        //window.location.href = window.location.href;
        window.location.reload();
    }
}

/////////////////////////////////////////////////////////
//注册
///////////////////////////////////////////////////////
//显示注册框
function ShowRegisterBox() {
    ShowLoginBox(null);
    showLoginOrRegister("2");
}

//检查用户名
function checkRegisterUserName() {
    var jqNewUserName = $("#textRegisterUserName");
    if (jqNewUserName.val() == "") {
        document.getElementById("errRegisterUserName").innerHTML = "用户名不能为空！";
        return false;
    }
    if (checkStringIsEmptyOrContainSpace(jqNewUserName.val())) {
        var stringNewUserName = $.trim(jqNewUserName.val());
//        var asyCheckNewUserNameUrl = "../../Service/ServiceUserAccount.svc/CheckIsUserNameOrEmailRegistered?Email=&Name=" 
        //            + encodeURIComponent(stringNewUserName) + "&Random=" + Math.random();
        var asyCheckNewUserNameUrl = "../../Service/ServiceUserAccount.svc/CheckIsUserNameOrEmailRegistered?Random=" + Math.random();
        var result = "";
        $.ajax({ url: asyCheckNewUserNameUrl,
            type: "get",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            async: false,
            data: { "name": encodeURIComponent(stringNewUserName), "email": "" },
            success: function (data) {
                result = data.d;
            }
        });
        var objResult = LX.Func.GetFirstContentFromRresponseJSON(result);
        if (objResult == "0") {
            document.getElementById("errRegisterUserName").innerHTML = "";
            return true;
        }
        else {
            document.getElementById("errRegisterUserName").innerHTML = "该用户名已存在！";
            return false;
        }
    }
    else {
        document.getElementById("errRegisterUserName").innerHTML = "用户名不能包含空格！";
        return false;
    }
}
//检查密码
function checkRegisterPassword() {
    var jqNewPSW = $("#textRegisterPSW");
    if (jqNewPSW.val() == "") {
        document.getElementById("errRegisterPSW").innerHTML = "请输入密码！";
        return false;
    }
    if (checkStringIsEmptyOrContainSpace(jqNewPSW.val())) {
        document.getElementById("errRegisterPSW").innerHTML = "";
        return true;
    }
    else {
        document.getElementById("errRegisterPSW").innerHTML = "密码不能包含空格！";
        return false;
    }
}
//检查确认密码
function checkRegisterConfirmPassword() {
    var jqConfirmPSW = $("#textRegisterConfirmPSW");
    if (checkStringIsEmptyOrContainSpace(jqConfirmPSW.val())) {
        var stringConfirmPSW = jqConfirmPSW.val();
        if (stringConfirmPSW == $("#textRegisterPSW").val()) {
            document.getElementById("errRegisterConfirmPSW").innerHTML = "";
            return true;
        }
        else {
            document.getElementById("errRegisterConfirmPSW").innerHTML = "输入确认密码与密码不同！";
            return false;
        }
    }
    else {
        document.getElementById("errRegisterConfirmPSW").innerHTML = "确认密码输入有误！";
        return false;
    }
}
//检查邮箱
function checkRegisterEmail() {
    var jqEmail = $("#textRegisterEmail");
    if (jqEmail.val() == "") {
        document.getElementById("errRegisterEmail").innerHTML = "请输入Eamil！";
        return false;
    }
    if (checkStringIsEmptyOrContainSpace(jqEmail.val())) {
        var stringNewEmail = $.trim(jqEmail.val());
        var Email = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
        if (!Email.test(stringNewEmail)) {
            document.getElementById("errRegisterEmail").innerHTML = "Eamil格式不正确！";
            return false;
        }
        else {
            var asyCheckEmailUrl = "../../Service/ServiceUserAccount.svc/CheckIsUserNameOrEmailRegistered?Random=" + Math.random();
            var result = "";
            $.ajax({ url: asyCheckEmailUrl,
                type: "get",
                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                data: { "name": "", "email": encodeURIComponent(stringNewEmail) },
                async: false,
                success: function (data) {
                    result = LX.Func.GetFirstContentFromRresponseJSON(data.d);                    
                }
            });
            if (result == "0") {
                document.getElementById("errRegisterEmail").innerHTML = "";
                return true;
            }
            else {
                document.getElementById("errRegisterEmail").innerHTML = "该邮箱已存在！";
                return false;
            }           
        }
    }
    else {
        return false;
    }
}

//检查网站协议是否勾选
function CheckRegisterCheckBoxAgree() {
    if ($("#checkboxRegisterAgree").attr("checked") != true) {
        $("#errRegisterCheckbox").html("请勾选同意网站协议");
        return false;
    }
    else {
        $("#errRegisterCheckbox").html("");
        return true;
    }
}

//请求注册
function register() {
    if (checkRegisterUserName() && checkRegisterPassword() && checkRegisterConfirmPassword() && checkRegisterEmail() && CheckRegisterCheckBoxAgree()) {
        //准备数据
        var stringusername = $("#textRegisterUserName").val();
        var stringpsw = $("#textRegisterPSW").val();
        var stringemail = $("#textRegisterEmail").val();
        //注册用户信息
        var newUser = new LX.Models.UserInfo();
        //newUser.UserName = encodeURIComponent(stringusername);
        newUser.UserName = encodeURIComponent(stringusername);
        newUser.Psw = encodeURIComponent(stringpsw);
        newUser.Email = encodeURIComponent(stringemail);

        var stringRegisterUrl = "../../Service/ServiceUserAccount.svc/UserAccountRegister?Random=" + Math.random();
        var result = {};
        $.ajax({ url: stringRegisterUrl,
            type: "get",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: { "newUserInfo": JSON.stringify(newUser) },
            async: false,
            success: function (data) {
                result = JSON.parse(data.d);
            }
        });
        if (result.status == "OK") {
            RegisterSuccess();
        }
        else {
            alert(result.message);
        }
    }
}

//注册成功
function RegisterSuccess() {
    //刷新隐藏字段
    //RefreshCurrentUserInfo("true", $("#textRegisterUserName").val(), $.cookie("userphotourl"));
    //隐藏弹出框
    HideLoginBox();
    //刷新当前页面
    //window.location.href = window.location.href;
    if (this.location.href.substring(this.location.href.lastIndexOf('/') + 1) == "Login.aspx") {
        window.location.href = "Personal.aspx";
    }
    else {
        //window.location.reload();
        window.location.href = window.location.href;
    }
}

//检查是否登录
function IsLogined() {
    var isresult=false;
    if ($("input[definedid='hiddenLoginStatus']").val() == "true") {
        if ($("input[definedid='hiddenCurrentUserName']").val() !="") {
            isresult = true;
        }
    }
    return isresult;
}

//检查字符串中是否非空并且不含有空格
function checkStringIsEmptyOrContainSpace(strCurrent) {
    if ($.trim(strCurrent) != "") {
        var arr = new Array();
        arr = strCurrent.split(" ");
        if (arr.length != 1) {
            return false;
        }
        return true;
    }
    else {
        return false;
    }
}

//绑定回车
function bindEnter(e) {
    e = e || window.event;
    if ($("#showDiv").css("display").toString() != "none") {
        if (e.keyCode == 13) {
            login();
            return false;
        }
    }
}

//document.onkeydown = bindEnter;
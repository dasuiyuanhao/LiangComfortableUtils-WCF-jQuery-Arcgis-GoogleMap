<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="LiangComUtils.Web.Controls.AllPage.Header" %>
<!--引用公共资源-->
<link rel="stylesheet" href="../../Styles/SkyStyle/css/reset.css" type="text/css" media="all" />
<link rel="stylesheet" href="../../Styles/SkyStyle/css/style.css" type="text/css" media="all" />
<script type="text/javascript" src="../../Scripts/js-facade/jquery-1.4.2.min.js"></script>
<script src="../../Scripts/jQuery.cookie.js" type="text/javascript"></script>
<script type="text/javascript" src="../../Scripts/js-facade/cufon-yui.js"></script>
<script type="text/javascript" src="../../Scripts/js-facade/Humanst521_BT_400.font.js"></script>
<script type="text/javascript" src="../../Scripts/js-facade/Humanst521_Lt_BT_400.font.js"></script>
<script type="text/javascript" src="../../Scripts/js-facade/roundabout.js"></script>
<script type="text/javascript" src="../../Scripts/js-facade/roundabout_shapes.js"></script>
<script type="text/javascript" src="../../Scripts/js-facade/gallery_init.js"></script>
<!--我的全局对象初始化-->
<script src="../../Scripts/LX.js" type="text/javascript"></script>
<script src="../../Scripts/js/LX.Models.js" type="text/javascript"></script>
<script src="../../Scripts/js/LX.Func.js" type="text/javascript"></script>
<!--登录-->
<script src="../../Scripts/js/login-box.js" type="text/javascript"></script>

<!-- header -->
<header>
    <div class="container">
    	<h1><a href="../../Main/HomePage/Default.aspx">趁手工具箱</a></h1>
      <nav>
        <ul>
          <asp:HiddenField ID="HiddenFieldMainNavigationIndex" Value="0" runat="server" />
          <%--<li><a id="aMainNavigation1" href="../../Main/HomePage/Default.aspx" class="current">首页</a></li>--%>
          <li><a id="aMainNavigation0" href="../../Main/HomePage/Default.aspx">首页</a></li>
          <li><a id="aMainNavigation1" href="../../Main/Bookmark/UserBookmark.aspx">收藏夹</a></li>
          <li><a id="aMainNavigation2" href="#">便签</a></li>
          <li><a id="aMainNavigation3" href="../../Main/Map/ArcgisForJavascript.aspx">地图</a></li>
          <li><a id="aMainNavigation4" href="#">联系我们</a></li>
          <li><a id="aMainNavigation5" href="#">关于</a></li>
        </ul>
      </nav>
    </div>
</header>

<!--登录与注册-->
<div id="headerHeaderLoginAndRegist" class="headerTop-actions">
    <span id="spanHeaderLoginingUser" style="display:none;">
        
    </span>
    <a class="button primary" href="javascript:ShowRegisterBox();">注册</a>
    <a id="aButtonHeaderLogin" class="button"  href="javascript:ShowLoginBox(null);">登录</a>
</div>
<div id="showDiv" style="display: none;">
    <div class="srfg" id="lele">
    </div>
    <div class="wjhh">
        <div class="wjfhg">
            <div class="xjjhw">
                <a href="#" onclick="HideLoginBox();" class="jhuu_3">&nbsp;</a>用户登录</div>
            <ul class="login_1">
                <li><a href="#" id="aTitleLogin" class="ahover" onclick="showLoginOrRegister('1');"
                    onfocus="this.blur()">登录</a></li>
                <li><a href="#" id="aTitleRegister" onclick="showLoginOrRegister('2');" onfocus="this.blur()">
                    注册</a></li>
            </ul>
            <div id="divBodyLogin" style="display: block;">
                <ul class="login_2">
                    <li>
                        <label>
                            账号：</label>
                        <input id="inputUserName" type="text" maxlength="100" class="kskrngg" value="请输入昵称"
                            onblur="if(this.value==''){this.value='请输入昵称';}" onfocus="if(this.value=='请输入昵称'){this.value='';}" /></li>
                    <li>
                        <label>
                            密码：</label>
                        <input id="inputPSW" maxlength="100" type="password" value="" class="kskrngg" /></li>
                    <li>
                        <label>
                        </label>
                        <input id="rememberUserNameAndPSW" runat="server" definedid="rememberUserNameAndPSW"
                            type="checkbox" />
                        &nbsp;&nbsp;记住下次自动登录</li>
                    <li>
                        <label>
                        </label>
                        <input type="button" id="buttonLogin" value="登录" onclick="login();" class="login_hka" /></li>
                </ul>
                <p class="login_3">
                    你还可以使用以下登录</p>
                <ul class="login_4">
                    <li><a href="#"></a></li>
                    <li class="login_4_1"><a href="#" onfocus="this.blur()">
                    </a></li>
                    <li class="login_4_2"><a href="#" onfocus="this.blur()">
                    </a></li>
                    <li class="login_4_3"><a href="#" onfocus="this.blur()">
                    </a></li>
                    <li class="login_4_4"><a href="#" onfocus="this.blur()">
                    </a></li>
                    <li class="login_4_5"><a href="#" onfocus="this.blur()">
                    </a></li>
                </ul>
            </div>
            <div id="divBodyRegister" style="display: none;">
                <ul class="login_2 fsfdx">
                    <li>
                        <label>
                            用户名：</label><input id="textRegisterUserName" type="text" class="kskrngg" onblur="checkRegisterUserName();" />
                        <span id="errRegisterUserName" style="color: red;"></span></li>
                    <li>
                        <label>
                            密&nbsp;&nbsp;&nbsp;&nbsp;码：</label><input id="textRegisterPSW" type="password" class="kskrngg"
                                onblur="checkRegisterPassword();" />
                        <span id="errRegisterPSW" style="color: red;"></span></li>
                    <li>
                        <label>
                            确认密码：</label><input id="textRegisterConfirmPSW" type="password" class="kskrngg" onblur="checkRegisterConfirmPassword();" />
                        <span id="errRegisterConfirmPSW" style="color: red;"></span></li>
                    <li>
                        <label>
                            邮&nbsp;&nbsp;&nbsp;&nbsp;箱：</label><input id="textRegisterEmail" type="text" class="kskrngg"
                                onblur="checkRegisterEmail();" />
                        <span id="errRegisterEmail" style="color: red;"></span></li>
                    <li>
                        <label>
                        </label>
                        <input type="checkbox" id="checkboxRegisterAgree" checked="checked" />&nbsp;&nbsp;我已阅读且同意《<a
                            href="#">网站协议</a>》 <span id="errRegisterCheckbox" style="color: red;"></span>
                    </li>
                    <li>
                        <label>
                        </label>
                        <input type="button" value="注册" onclick="register();" class="login_hka" />
                    </li>
                </ul>
            </div>
            <%--<div class="wjfhg_ds">
                <img src="../../Styles/SkyStyle/images/FloatingLoginBox/bg-jh_q.png" /></div>--%>
            <div style="clear:both;"></div>
        </div>
    </div>
</div>


<script type="text/javascript">
    //导航
    try {
        $(function () {        
                $("#aMainNavigation<%=HiddenFieldMainNavigationIndex.Value %>").addClass("current");        
        });
    }
    catch (e) { }
</script>

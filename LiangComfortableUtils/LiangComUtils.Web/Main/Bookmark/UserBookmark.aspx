<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBookmark.aspx.cs" Inherits="LiangComUtils.Web.Main.Bookmark.UserBookmark" %>
<%@ Register TagPrefix="MyControls" TagName="Header" Src="../../Controls/AllPageControls/Header.ascx" %>
<%@ Register TagPrefix="MyControls" TagName="Footer" Src="../../Controls/AllPageControls/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=7, IE=9, IE=10">
    <meta name="viewport" content="initial-scale=1, maximum-scale=1,user-scalable=no"/>
    <title>我的收藏夹</title>

    <%--<script src="../../Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    
</head>
<body>
    <form id="form1" runat="server">
    <MyControls:Header ID="Header1" runat="server" MainNavigationIndex="1" ></MyControls:Header>

    <!-- /#gallery -->
    <div class="main-box">
        <div class="container">
            <div class="inside">
                <div class="wrapper" style="height:600px; background-image:">
                    <!--放置主体内容-->


                </div>
            </div>
        </div>
    </div>

    <MyControls:Footer ID="Footer1" runat="server" ></MyControls:Footer>
    </form>
</body>
</html>

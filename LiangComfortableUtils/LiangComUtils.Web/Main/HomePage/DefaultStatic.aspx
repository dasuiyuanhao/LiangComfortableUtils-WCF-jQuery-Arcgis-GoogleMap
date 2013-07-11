<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DefaultStatic.aspx.cs" Inherits="LiangComUtils.Web.Main.DefaultStatic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>趁手工具箱</title>
    <link rel="stylesheet" href="../../Styles/SkyStyle/css/reset.css" type="text/css" media="all" />
    <link rel="stylesheet" href="../../Styles/SkyStyle/css/style.css" type="text/css" media="all" />
    <script type="text/javascript" src="../../Scripts/js-facade/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/js-facade/cufon-yui.js"></script>
    <script type="text/javascript" src="../../Scripts/js-facade/Humanst521_BT_400.font.js"></script>
    <script type="text/javascript" src="../../Scripts/js-facade/Humanst521_Lt_BT_400.font.js"></script>
    <script type="text/javascript" src="../../Scripts/js-facade/roundabout.js"></script>
    <script type="text/javascript" src="../../Scripts/js-facade/roundabout_shapes.js"></script>
    <script type="text/javascript" src="../../Scripts/js-facade/gallery_init.js"></script>
    <%--<script type="text/javascript" src="../Scripts/js-facade/cufon-replace.js"></script>--%>
    <!--[if lt IE 7]>
  	<link rel="stylesheet" href="css/ie/ie6.css" type="text/css" media="all">
  <![endif]-->
    <!--[if lt IE 9]>
  	<script type="text/javascript" src="js/html5.js"></script>
    <script type="text/javascript" src="js/IE9.js"></script>
  <![endif]-->
  <script type="text/javascript">
      $(function () {
          $("#aMainNavigation0").addClass("current");
      });
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <!-- header -->
    <header>
    <div class="container">
    	<h1><a href="../../Main/HomePage/Default.aspx">Design Company</a></h1>
      <nav>
        <ul>
            <li><a href="../../Main/HomePage/Default.aspx" class="current">首页</a></li>
          <li><a href="../../Main/Bookmark/UserBookmark.aspx">收藏夹</a></li>
          <li><a href="#">便签</a></li>
          <li><a href="#">地图</a></li>
          <li><a href="#">联系我们</a></li>
          <li><a href="#">关于</a></li>
        </ul>
      </nav>
      
    </div>  
     
	</header>
    <div class="headerTop-actions">
        <a class="button primary" href="#">注册</a>
        <a class="button" href="#">登录</a>
    </div> 
    <!-- #gallery -->
    <section id="gallery">
  	<div class="container">
    	<ul id="myRoundabout">
      	<li><img src="../../Styles/SkyStyle/images/slide3.jpg" alt=""></li>
        <li><img src="../../Styles/SkyStyle/images/slide2.jpg" alt=""></li>
        <li><img src="../../Styles/SkyStyle/images/slide5.jpg" alt=""></li>
        <li><img src="../../Styles/SkyStyle/images/slide1.jpg" alt=""></li>
        <li><img src="../../Styles/SkyStyle/images/slide4.jpg" alt=""></li>
      </ul>
  	</div>
  </section>
    <!-- /#gallery -->
    <div class="main-box">
        <div class="container">
            <div class="inside">
                <div class="wrapper">
                    <!-- aside -->
                    <aside>
            <h2>Recent <span>News</span></h2>
            <!-- .news -->
            <ul class="news">
            	<li>
              	<figure><strong>22</strong>June</figure>
                <h3><a href="#">Sed ut perspiciatis unde</a></h3>
                Domnis iste natus error sit voluptam accusa doloremque <a href="#">...</a>
              </li>
              <li>
              	<figure><strong>09</strong>June</figure>
                <h3><a href="#">Totam rem aperiam</a></h3>
                Eaqueipsa quae abillo inventoretis et quasi architecto beatae <a href="#">...</a>
              </li>
              <li>
              	<figure><strong>31</strong>May</figure>
                <h3><a href="#">Inventore veritatis et quasi</a></h3>
                Architecto beatae vitae dicta sunt explicabo <a href="#">...</a>
              </li>
              <li>
              	<figure><strong>25</strong>May</figure>
                <h3><a href="#">Nemo enim ipsam</a></h3>
                Voluptatem quia voluptas sit asper natur aut odit aut fugit <a href="#">...</a>
              </li>
            </ul>
            <!-- /.news -->
          </aside>
                    <!-- content -->
                    <section id="content">
            <article>
            	<h2>Welcome to <span>Our Design Company!</span></h2>
              <p>Design Company is a free web template created by TemplateMonster.com team. This website template is optimized for 1024X768 screen resolution. It is also HTML5 &amp; CSS3 valid.</p>
              <figure><a href="#"><img src="../../Styles/SkyStyle/images/banner1.jpg" alt=""></a></figure>
              <p>This website template has several pages: <a href="index.html">Home</a>, <a href="about.html">About us</a>, <a href="privacy.html">Privacy Policy</a>, <a href="gallery.html">Gallery</a>, <a href="contacts.html">Contact us</a> (note that contact us form – doesn’t work), <a href="sitemap">Site Map</a>.</p>
              This website template can be delivered in two packages - with PSD source files included and without them. If you need PSD source files, please go to the template download page at TemplateMonster to leave the e-mail address that you want the template ZIP package to be delivered to.
            </article> 
          </section>
                </div>
            </div>
        </div>
    </div>
    <!-- footer -->
    <footer>
    <div class="container">
    	<div class="wrapper">
        <div class="fleft">Copyright - Type in your name here</div>
        <div class="fright"><!--<a rel="nofollow" href="http://www.templatemonster.com/" target="_blank">Website template</a> designed by TemplateMonster.com&nbsp; &nbsp; |&nbsp; &nbsp; <a href="http://templates.com/product/3d-models/" target="_blank">3D Models</a> provided by Templates.com--></div>
      </div>
    </div>
  </footer>
    <script type="text/javascript">        Cufon.now(); </script>
    </form>
</body>
</html>

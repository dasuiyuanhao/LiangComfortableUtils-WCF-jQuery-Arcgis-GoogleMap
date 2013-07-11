<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArcgisForJavascript.aspx.cs" Inherits="LiangComUtils.Web.Main.Map.ArcgisForJavascript" %>
<%@ Register TagPrefix="MyControls" TagName="Header" Src="../../Controls/AllPageControls/Header.ascx" %>
<%@ Register TagPrefix="MyControls" TagName="Footer" Src="../../Controls/AllPageControls/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=7, IE=9, IE=10">
    <meta name="viewport" content="initial-scale=1, maximum-scale=1,user-scalable=no"/>
    <title>地图-ArcgisForJavascript</title>

    <!--样式-->
    <link rel="stylesheet" href="../../Styles/SkyStyle/css/Map/nihilo.css" />
    <link rel="stylesheet" href="../../Styles/SkyStyle/css/Map/esri.css" />
    <style type="text/css">        
        #divInfo
        {
            top: 20px;
            color: #444;
            height: auto;
            font-family: arial;
            right: 20px;
            margin: 5px;
            padding: 10px;
            position: absolute;
            width: 125px;
            z-index: 40;
            border: solid 2px #ccc;
            border-radius: 4px;
            background-color: #fff;
        }
    </style>

    <script src="../../Scripts/ArcgisForJavascript/jsapi_vsdoc10_v35.js" type="text/javascript"></script>
    <script src="../../Scripts/ArcgisForJavascript/esri-dojo.js" type="text/javascript"></script>
    <script src="../../Scripts/js/LX.Map.js" type="text/javascript"></script>

    <script type="text/javascript">
        //初始化地图工具栏
        function initToolbar() {
            tb = new esri.toolbars.Draw(map);
            dojo.connect(tb, "onDrawEnd", addGraphic);

            //hook up the button click events
            dojo.connect(dojo.byId("clearGraphics"), "click", function () {
                map.graphics.clear();
            });

            dojo.connect(dojo.byId("drawPoint"), "click", function () {
                tb.activate(esri.toolbars.Draw.POINT);
            });

            dojo.connect(dojo.byId("drawMultipoint"), "click", function () {
                tb.activate(esri.toolbars.Draw.MULTI_POINT);
            });

            dojo.connect(dojo.byId("drawExtent"), "click", function () {
                tb.activate(esri.toolbars.Draw.EXTENT);
            });

            dojo.connect(dojo.byId("drawPolyline"), "click", function () {
                tb.activate(esri.toolbars.Draw.POLYLINE);
            });
            dojo.connect(dojo.byId("drawFreehandPolyline"), "click", function () {
                tb.activate(esri.toolbars.Draw.FREEHAND_POLYLINE);
            });
            dojo.connect(dojo.byId("drawPolygon"), "click", function () {
                tb.activate(esri.toolbars.Draw.POLYGON);
            });
            dojo.connect(dojo.byId("drawFreehandPolygon"), "click", function () {
                tb.activate(esri.toolbars.Draw.FREEHAND_POLYGON);
            });
            dojo.connect(dojo.byId("drawLine"), "click", function () {
                tb.activate(esri.toolbars.Draw.LINE);
            });
        }

        function addGraphic(geometry) {
            //deactivate the toolbar and clear existing graphics 
            tb.deactivate();
            //map.graphics.clear();


            //Marker symbol used for point and multipoint created using svg path. See this site for more examples
            // http://raphaeljs.com/icons/#talkq. You could also create marker symbols using the SimpleMarkerSymbol class
            //to define color, size, style or the PictureMarkerSymbol class to specify an image to use for the symbol. 
            var markerSymbol = new esri.symbol.SimpleMarkerSymbol();
            markerSymbol.setPath("M16,4.938c-7.732,0-14,4.701-14,10.5c0,1.981,0.741,3.833,2.016,5.414L2,25.272l5.613-1.44c2.339,1.316,5.237,2.106,8.387,2.106c7.732,0,14-4.701,14-10.5S23.732,4.938,16,4.938zM16.868,21.375h-1.969v-1.889h1.969V21.375zM16.772,18.094h-1.777l-0.176-8.083h2.113L16.772,18.094z");
            markerSymbol.setColor(new dojo.Color("#00FFFF"));

            var pictureMarkerSymbol = new esri.symbol.PictureMarkerSymbol({
                "url": "../../Styles/SkyStyle/images/Map/UnSelect.png",
                "height": 28,
                "width": 16,
                "type": "esriPMS"
            });

            //line symbol used for freehand polyline, polyline and line. In this example we'll use a cartographic line symbol
            //Try modifying the cartographic line symbol properties like CAP and JOIN. For CAP try CAP_ROUND or CAP_SQUARE
            //for JOIN try JOIN_BEVEL or JOIN_MITER or JOIN_ROUND
            var lineSymbol = new esri.symbol.CartographicLineSymbol(
              esri.symbol.CartographicLineSymbol.STYLE_SOLID,
              new dojo.Color([255, 0, 0]), 10,
              esri.symbol.CartographicLineSymbol.CAP_ROUND,
              esri.symbol.CartographicLineSymbol.JOIN_MITER, 5
            );

            //fill symbol used for extent, polygon and freehand polygon. In this example we use a picture fill symbol
            //the images folder contains additional fill images - try swapping mangrove out for one of the other options
            //(sand, swamp or stiple)
            var fillSymbol = new esri.symbol.PictureFillSymbol("../../Styles/SkyStyle/images/Map/UnSelect.png",
               new esri.symbol.SimpleLineSymbol(
                esri.symbol.SimpleLineSymbol.STYLE_SOLID,
                 new dojo.Color('#000'), 1
                ), 25, 42
            );


            var type = geometry.type, symbol;
            if (type === "point" || type === "multipoint") {
                //symbol = markerSymbol;
                symbol = pictureMarkerSymbol;
            }
            else if (type === "line" || type === "polyline") {
                symbol = lineSymbol;
            }
            else {
                //symbol = fillSymbol;
                symbol = pictureMarkerSymbol;
            }

            //Add the graphic to the map 
            map.graphics.add(new esri.Graphic(geometry, symbol));

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <MyControls:Header ID="Header1" runat="server" MainNavigationIndex="3"></MyControls:Header>

    <!-- /#gallery -->
    <div class="main-box">
        <div class="container">
            <div class="inside">
                <div class="wrapper" style="height: 600px; background-image: ">
                    <!--放置主体内容-->
                    <div id="divMap" style="width: 1000px; height: 600px;">
                    </div>
                    <!--工具箱-->
                    <div id="divInfo">
                        <div>
                            <input type="button" id="clearGraphics" value="清除" data-dojo-type="dijit.form.Button" /><br />
                        </div>
                        <div>
                            选择一个形状以添加 graphic</div>
                        <input type="button" id="drawPoint" data-dojo-type="dijit.form.Button" value="Point-点" />
                        <br />
                        <input type="button" id="drawMultipoint" data-dojo-type="dijit.form.Button" value="Multipoint-多点" />
                        <br />
                        <input type="button" id="drawExtent" value="Extent-区域" data-dojo-type="dijit.form.Button" />
                        <br />
                        <input type="button" id="drawPolyline" value="Polyline-线" data-dojo-type="dijit.form.Button" />
                        <br />
                        <input type="button" id="drawFreehandPolyline" value="Freehand Polyline" data-dojo-type="dijit.form.Button" />
                        <br />
                        <input type="button" id="drawPolygon" value="Polygon-多边形" data-dojo-type="dijit.form.Button" /><br />
                        <input type="button" id="drawFreehandPolygon" value="Freehand Polygon" data-dojo-type="dijit.form.Button" /><br />
                        <input type="button" id="drawLine" value="Line" data-dojo-type="dijit.form.Button" /><br />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <MyControls:Footer ID="Footer1" runat="server" ></MyControls:Footer>
    </form>
</body>
</html>

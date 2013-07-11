/// <reference path="ArcgisForJavascript/jsapi_vsdoc10_v35.js" />
/*
* 地图相关初始化
*/
dojo.require("esri.map");
dojo.require("esri.layers.agstiled");
dojo.require("esri.layers.wmts");
dojo.require("esri.layers.WebTiledLayer");
dojo.require("esri.toolbars.draw");

dojo.require("dijit.layout.BorderContainer");
dojo.require("dijit.layout.ContentPane");

var map, tb,layerDisplay;
function init() {
    map = new esri.Map("divMap", {
        center: [-89.985, 29.822],
        zoom: 8
    });
    //绑定事件
    dojo.connect(map, "onLoad", initToolbar);

    var wtl = esri.layers.WebTiledLayer;
    var cycleMap = new wtl("http://${subDomain}.google.cn/vt/v=w2.116&hl=zh-CN&gl=cn&x=${col}&y=${row}&z=${level}&s=Ga", {
        "copyright": "Liang-Open Cycle Map",
        "id": "Liang-Open Cycle Map",
        "subDomains": ["mt2", "mt1", "mt3"]
    });
    map.addLayer(cycleMap);
    var layerDisplay = new esri.layers.DynamicLayerInfo();
    map.addLayer(layerDisplay);
    console.log(layerDisplay);
    
}

dojo.ready(init);
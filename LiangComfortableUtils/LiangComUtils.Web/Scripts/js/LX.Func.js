/*
* 我的全局自定义公用函数
*/

//解析WCF返回结果json字符串成对象,返回内容
LX.Func.GetContentsFromRresponseJSON = function (strJSON) {
    var result = null;
    if (strJSON) {
        try {
            var objRe = JSON.parse(strJSON);
            if (objRe) {
                result = new Array(objRe.contents);
            }
        } catch (e) { }
    }
    return result;
}
//解析WCF返回结果json字符串成对象,返回内容
LX.Func.GetFirstContentFromRresponseJSON = function (strJSON) {
    //    var objContents = LX.Func.GetContentsFromRresponseJSON(strJSON);
    //    if(objContents){
    //        if (objContents.length > 0) {
    //            return objContents[0];
    //        }
    //    }
    if (strJSON) {
        try {
            var objRe = JSON.parse(strJSON);
            if (objRe) {
                if (objRe.contents.length > 0) {
                    return objRe.contents[0];
                }
            }
        } catch (e) { }
    }
    return null;
}
//判断string类型对象是否为空或者null
LX.Func.IsStringObjectEmptyOrNull = function (str) {
    if (str == null) {
        return true;
    }
    if (str == "") {
        return true;
    }
    return false;
}



















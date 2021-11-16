//时间戳
function unixtime() {
    var dt = new Date();
    var ux = Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDay(), dt.getHours(), dt.getMinutes(), dt.getSeconds()) / 1000;
    return ux;
}
//获取querystring参数值
function getQueryStringByName(name) {
    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
}
//跳转到上一页
function GoBack() {
    var url = document.referrer;
    if (url.indexOf("?") > -1) {
        url += "&time=" + unixtime();
    } else {
        url += "?time=" + unixtime();
    }
    window.location = url;
}
// 时、分、秒不足2位用0补
function TimeFillZeroInDateDes(obj) {
    var result = obj;
    if (obj < 10) {
        result = "0" + obj;
    }

    return result;
}
////获取网站跟路径
//function getRootPath() {
//    var pathName = window.location.pathname.substring(1);
//    var webName = pathName == '' ? '' : pathName.substring(0, pathName.indexOf('/'));
//    return window.location.protocol + '//' + window.location.host + '/' + webName + '/';
//    //return window.location.protocol + '//' + window.location.host + '/' ;

//}
//alert(getRootPath());
//function getRootPath() {
//    var strFullPath = window.document.location.href;
//    var strPath = window.document.location.pathname;
//    var pos = strFullPath.indexOf(strPath);
//    var prePath = strFullPath.substring(0, pos);
//    var postPath = strPath.substring(0, strPath.substr(1).indexOf('/') + 1);
//    return (prePath + postPath);
//}
//alert(getRootPath());
//function getRootPath() {
//    var pathName = window.location.pathname.substring(1);
//    var webName = pathName == '' ? '' : pathName.substring(0, pathName.indexOf('/'));
//    //return window.location.protocol + '//' + window.location.host + '/'+ webName + '/';
//    return window.location.protocol + '//' + window.location.host + '/' + webName;
//}
//alert(getRootPath());

//跳转到引用页
function goTo(url) {
    var a = document.createElement("a");
    if (!a.click) { //only IE has this (at the moment);
        window.location = url;
        return;
    }
    a.setAttribute("href", url);
    a.style.display = "none";
    document.body.appendChild(a);
    a.click();
}
//显示弹出框
function ShowMessBox(mess, width, height) {
    jQuery("#dialog").html(mess);
    if (width != null && height != null)
        $("#dialog").dialog({
            height: height,
            width: width
        });
    else
        jQuery("#dialog").dialog();
}

//jQuery(function () {
//    $("#dialog").ajaxError(function (event, request, settings) {
//        //if (request.status == "12029" || request.status == "12004") {
//        //    loadingHide();
//        //    return;
//        //}
//        //var html = "ajax请求出错<br/>";
//        //html += "url:" + settings.url + "<br/>";
//        //html += "status:" + request.status + "<br/>";
//        //html += request.responseText;
//        //ShowMessBox(html);
//        loadingHide();
//    });
//});

var loadingstatus = 0;
function loadingShow() {
    //loadingstatus++;
    //jQuery("#loading").show();
}
function loadingHide() {
    //loadingstatus--;
    //var xtmp_f=function () {
    //    if (loadingstatus == 0)
    //    {
    //        jQuery("#loading").hide();
    //    } else {
    //        setTimeout(xtmp_f, 300)
    //    }
    //};
    // setTimeout(xtmp_f, 300);
}
//Post访问返回值为JsonResult的action
jQuery.jsonPost = function (controller, action, data, callback, postJson) {
    jQuery.jsonPost2(controller, action, data, callback, postJson, function () { });
}
jQuery.jsonPost2 = function (controller, action, data, callback, postJson,errcall) {
    data = cloneAll(data);
    //将postJson对象序列化成字符串，并post到服务器
    if (postJson != undefined && postJson != null) {
        data.ModelJson = JSON.stringify(postJson);
    }
    jQuery.post("/" + controller + "/" + action + "?jsonPost=1&t=" + unixtime(), data, function (data) {
        if (typeof (data) == "string") {
            eval("var data=" + data);
        }
        if (data.IsSucceed == true) {
            callback(data.Data);
        } else {
            if (data.Mess + "" == "404") {
                return;
            }
            if (typeof (data) != "undefined" && typeof (data.Mess) != "undefined") {
                alert(data.Mess);
                errcall();
            }
        }
    });
}
////Post访问html内容
//jQuery.htmlPost = function (controller, action, data, callback, postJson) {
//    //将postJson对象序列化成字符串，并post到服务器
//    if (postJson != undefined && postJson != null) {
//        data.ModelJson = JSON.stringify(postJson);
//    }
//    loadingShow();
//    jQuery.post("/" + controller + "/" + action + "?htmlPost=1&t=" + unixtime(), data, function (data) {
//        loadingHide();
//        if (data.IsSucceed==false) {
//            ShowMessBox(data.Mess);
//        }
//        else {
//            callback(data);
//        }
//    });
//}
////Get访问html内容
//jQuery.htmlGet = function (controller, action, data, callback) {
//    loadingShow();
//    jQuery.post("/" + controller + "/" + action + "?htmlGet=1&t=" + unixtime(), data, function (data) {
//        loadingHide();
//        if (data.IsSucceed == false) {
//            ShowMessBox(data.Mess);
//        }
//        else {
//            callback(data);
//        }
//    });
//}



//js对象拷贝
function cloneAll(fromObj) {
    var str = JSON.stringify(fromObj);
    return  JSON.parse(str);
}
//拷贝集合
function CopyArray(arry) {
    var output = [];
    for (var i = 0; i < arry.length; i++) {
        output.push(arry[i]);
    }
    return output;
}

//框架页面用
var winds = {};
function SetWindows(wind, t) {
    winds[t]= wind;
}
function GetWindows(t) {
    return winds[t];
}

//框架页面提供的打开新页面的函数
function BaseOpen_lyout(name, url, listid, w, h,uit) {
    if (!w) w = 500;
    if (!h) h = 500;
    jQuery("body").append("<div id=\"dlg_" + uit + "\" style=\"display: none\"></div>");
    jQuery("#dlg_" + uit).dialog({
        title: name,
        content: "<iframe src=\"" + url + "\"  style='width: 99%;height: 99%;border: 0;' frameborder='no'></iframe>",
        //closable: false,
        width: w,
        height: h,
        modal: true,
        cache: false
    }).dialog("center");
}
//框架页面提供的关闭弹出层函数
function Closedialog_lyout(uit) {
    jQuery("#dlg_" + uit).dialog("close");
    jQuery("#dlg_" + uit).detach();
}

//当前页面用 新页面打开函数
function openPage(name, url, listid, w, h) {
    var uit = unixtime();
    GetParentBase().SetWindows(window, uit);
    url += "&uit=" + uit;
    if (listid) url = url + "&listid=" + listid;
    GetParentBase().BaseOpen_lyout(name, url, listid, w, h, uit);
}
//每个页面提供的访问顶层页面的关闭页面函数
function Closedialog(uit) {
    if (uit) GetParentBase().Closedialog_lyout(uit);
    else GetParentBase().ClosedialogTab();
}

//从顶层页面查找指定页面并调用目标页面的reload列表
function Gridreload(str, uit) {
    GetParentBase().GetWindows(uit).Gridreload_page(str);
}
//每个页面提供的刷新当前页面list的接口
function Gridreload_page(str) {
    $("#" + str).datagrid("reload");
}

function GetParentBase() {
    var url = "";
    var wind = window;
    for (var i = 0; i < 10000; i++) {
        if (url === wind.location.href) {
            break;
        }
        url = wind.location.href;
        wind = wind.parent;
    }
    return wind;
}



//数值验证
$.extend($.fn.validatebox.defaults.rules, {
    Number: {
        validator: function (value, param) {
            if (isNaN(value)) return false;
            if (param[0] && (value - 0) < (param[0] - 0)) {
                return false;
            }
            if (param[1] && (value - 0) > (param[1] - 0)) {
                return false;
            }
            return true;
        },
        message: '必须是数字,并且值必须在{0}到{1}范围内'
    }
});

//数值验证
$.extend($.fn.validatebox.defaults.rules, {
    lengthExt: {
        validator: $.fn.validatebox.defaults.rules["length"].validator,
        message: '{2}'
    },
    NumberExt: {
        validator: $.fn.validatebox.defaults.rules["Number"].validator,
        message: '{2}'
    },
    remoteExt: {
        validator: $.fn.validatebox.defaults.rules["remote"].validator,
        message: '{2}'
    }
});
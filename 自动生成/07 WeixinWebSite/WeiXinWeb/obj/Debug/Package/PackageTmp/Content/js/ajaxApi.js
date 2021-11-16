var isTest = false;
//ajax封装
$.post = function (url, data) {
    var deferred = new $.promise.Promise();
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (result) {
            if (result.code === 1) {
                deferred.done(result.data);
            } else {
                if (result.errorMsg === "未登录") {
                    apix.getuser(true).then(function (d) { });
                    return;
                }
                if (result.errorMsg === "未注册") {
                    apix.getuser(true).then(function (d) {
                        apix.goto("Regist.html");
                    });
                    return;
                }
                alert(result.errorMsg);
            }
        },
        error: function (e) {
            console.log(e);
            alert("http错误: \r\n[url:"+url+"]\r\n返回：\r\n[code:" + e.status + "][" + e.statusText+"]");
            $.hideIndicator();
        }
    });
    return deferred;
};
//生产测试数据
$.df = function (data) {
    var deferred = new $.promise.Promise();
    deferred.done(data);
    return deferred;
};



//cookie 缓存
$.post_cookie = function (url, data, cookieKey) {
    var deferred = new $.promise.Promise();
    if ($.cookie.getCookie(cookieKey) !== "") {
        deferred.done(JSON.parse($.cookie.getCookie(cookieKey)));
    } else {
        $.post(url, data).then(function (data) { $.cookie.setCookie(cookieKey, JSON.stringify(data)); deferred.done(data); });
    }
    return deferred;    
};

//js 缓存
var post_js_cache = {};
$.post_js = function (url, data, key) {
    var deferred = new $.promise.Promise();
    if (post_js_cache[key]) {
        deferred.done(post_js_cache[key]);
    } else {
        $.post(url, data).then(function (data) { post_js_cache[key]=data; deferred.done(data); });
    }
    return deferred;
};



//测试用代码,使用123123 openid登陆
//$.cookie.setCookie("sys_codeg", "123123");

var apix = {};

//封装页面导航函数,方便未来判断是微信公众号还是微信小程序，做不同跳转行为
apix.goto = function (path) {
    if (window.__wxjs_environment === 'miniprogram') {
        alert("不支持微信小程序");
        return;
        //wx.miniProgram.navigateTo({ url: '/pages/lanCard/lanCard-quick?year=4&yearid=504&ks=1' });
    } else {
        window.location.href = path;
    }
};

//查询对象
apix.Find = function (list, func) {
    for (var i = 0; i < list.length; i++) {
        var isok = func(list[i]);
        if (isok) {
            return list[i];
        }
    }
};

////读取微信回调code，并写入cookie
//if ($.getQueryString("code")) {
//    $.cookie.setCookie("sys_codeg", $.getQueryString("code"));
//}

//获取公司信息
apix.GetCompanys = function () {
    return $.post_js("/home/JSSetting", {}, "companysdata");
};

//根据回调Code获取ipenid（在服务器登陆）
apix.getOpenID = function () {
    return apix.GetCompanys().then(function (d) {
        //检查cookie中是否有存在code,存在说明登陆过，不存在跳转到认证页面进行认证
        if (!isTest && ($.getQueryString("code") == null || $.getQueryString("code")=="")) {
            //跳转到微信平台认证
            window.location.href = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + d.sAppID + "&redirect_uri=" + encodeURIComponent(window.location.href) + "&response_type=code&scope=snsapi_userinfo#wechat_redirect";
            return;
        } else {
            var code = $.getQueryString("code");
            if (isTest) code = "mytest2";
            return $.post_js("/home/GetOpenID", { code: code }, "getOpenID");
        }
    });
};

//获取用户信息(如果未登录则会自动登陆)，reload定义表示无缓存刷新
apix.getuser = function (reload) {
    return $.post("/home/GetUser", {}, "userdata").then(function (g) {
        if (g == null) {
            //需要登陆
            return apix.getOpenID().then(function (d) {
                return $.post("/home/GetUser", {}, "userdata").then(function (f) {
                    if (f.ID == null && (window.location.href + "").indexOf("Regist.html") == -1) {
                        apix.goto("Regist.html");
                    } else {
                        return $.df(f);
                    }
                });
            });
        } else {
            if (g.ID == null && (window.location.href + "").indexOf("Regist.html") == -1) {
                apix.goto("Regist.html");
            }
            else {
                return $.df(g);
            }
        }
    });
};

//注册
apix.SaveRegist = function (data) {
    return $.post("/Home/MeRegist", data);
};

//会员中心数据
apix.GetMember = function () {
    return $.post("/Member/GetMember", {});
};

//门店数据
apix.GetStores = function () {
    //return $.testData([{ StoreName: 'xxx1' }, { StoreName: 'xxx1' }, { StoreName: 'xxx1' }, { StoreName: 'xxx1' }]);
    return $.post("/Member/GetStores", {});
};
//项目数据（项目id）
apix.GetProjects = function (id) {
    //return $.testData([{ StoreName: 'xxx1' }, { StoreName: 'xxx1' }, { StoreName: 'xxx1' }, { StoreName: 'xxx1' }]);
    return $.post("/Member/GetProjects", { storeName:id});
};
//门店理疗师
apix.GetEmployees = function (id) {
    return $.post("/Member/GetEmployees", { StoreID: id });
};
//预约
apix.SaveMemberOrders = function (obj) {
    return $.post("/Member/SaveMemberOrders", obj);
};
//订单列表
apix.GetOrder = function (type) {
    return $.post("/Member/GetOrder", { type: type });
};
//订单详情
apix.GetOrderOne = function (id) {
    return $.post("/Member/GetOrderOne", { id: id });
};
//取消订单
apix.CancalOrder = function (id) {
    return $.post("/Member/CancalOrder", { id: id });
};
//获取门店详情(包含技师信息，宣传图册信息)
apix.GetStoreOne = function (id) {
    return $.post("/Member/GetStoreOne", { id: id });
};
//项目数据（项目id）
apix.GetProjectOne = function (id) {
    return $.post("/Member/GetProjectOne", { id: id });
};
//项目数据（项目id）
apix.GetProjectsByType = function (id) {
    //return $.testData([{ StoreName: 'xxx1' }, { StoreName: 'xxx1' }, { StoreName: 'xxx1' }, { StoreName: 'xxx1' }]);
    return $.post("/Member/GetProjects", { storeName: id });
};
//获取公司产品数据列表
apix.GetProducts = function () {
    return $.post("/Member/GetProducts", {  });
};
//获取公司产品数据
apix.GetProductsOne = function (id) {
    return $.post("/Member/GetProductsOne", { id: id });
};


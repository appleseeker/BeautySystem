$(function () {
    var openid = $.cookie.getCookie("openid"),
        code = $.getQueryString("code"),
        memberInfo,
        authInfo,
        $tpl = $("#tpl").html(),
        toast_time = new Date(),
        toast_twice,
        supermember;
    toast_time = (toast_time.getMonth() + 1) + ':' + toast_time.getDate();
    toast_twice = localStorage.getItem('toast_twice') || 0;
    if (sessionStorage.getItem('actStatus') === undefined) {
        if (window.screen.width === 320) {
            $('.act-minProgram ').css('height', '18.975rem');
        }
    }
    getOpenid(openid, code);
    var gg = $.cookie.getCookie("gg");

     function auth() {
         $.showIndicator();
         var deferred = new $.promise.Promise();
         $.ajax({
             type: "GET",
             url: "/orchid/card/auth?openid=" + openid,
             success: function (result) {
                 if (!result.data.auth) {
                     var ready0 = function () {
                         if (window.__wxjs_environment === 'miniprogram') {
                             wx.miniProgram.navigateTo({ url: '/pages/lanCard/lanCard-register' });
                         } else {
                             window.location.href = "register.html";
                         }
                     };
                     if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
                         document.addEventListener('WeixinJSBridgeReady', ready0, false);
                     } else {
                         ready0();
                     }
                     return;
                 }
                 authInfo = result.data;
                 deferred.done(result);
             },
             error: function () {
                 $.hideIndicator();
             }
         });
         return deferred;
     }
     function getMemInfo(){
       $.showIndicator();
         var deferred = new $.promise.Promise();
         $.ajax({
             type: "POST",
             url: "/Home/GetUser",
             data: {
                 //openid: openid
             },
             success: function (result) {
                 if (result.status !== 0) {
                     alert(result.message);
                     return;
                 }
                 memberInfo = result.data;
                 memberInfo.ourEmp ? supermember = 0 : memberInfo.member ? supermember = 1 : supermember = 2
                 deferred.done(result);
             },
             error: function () {
                 $.hideIndicator();
             }
         });
         return deferred;
     }
    function initPage() {
        //getMemInfo.then(function () {
            $.hideIndicator();
        $(".member").html($.template($tpl)({ auth: {}, member: {}, supermember: {} }));
            if (toast_twice) {
                // 日期等 不显示
                if (toast_twice === toast_time) {
                    $(".card_mask1").addClass("hidden");
                    $(".yearca").addClass("hidden");
                } else {
                    // 日期不等 显示
                    $(".card_mask1").removeClass("hidden");
                    $(".yearca").removeClass("hidden");
                    localStorage.setItem('toast_twice', toast_time);
                }
            } else {
                // 显示
                localStorage.setItem('toast_twice', toast_time);
                $(".card_mask1").removeClass("hidden");
                $(".yearca").removeClass("hidden");
            }
            // if(gg!=1){
            // $.cookie.setCookie("gg", "1",{expires:1});
       // });

    }
     if (code) {
         Lan.getOpenID(code).then(function (result) {
             openid = result.data.openid;
             $.cookie.setCookie("openid", openid);
             initPage();
         });
     } else if (openid) {
         initPage();
     }
    initPage();
    /* 
    $.delegateEvents({
        events: {
            "click  .balance": "balance",
            "click  .memdetail": "memdetail",
            "click  .memberRuler": "memberRuler",
            "click  .task": "task",
            "click  .order": "order",
            "click  .shop-order": "shopOrder",
            "click  .appointment": "appointment",
            "click  .exchange": "exchange",
            "click  .card-mask": "update",
            "click  .exchangenow": "exchangenow",
            "click  .super_shop": "exchangenow",
            "click  .qui": "getCoupon"
        },
        appointment: function() {
            function ready1() {
              if (window.__wxjs_environment === 'miniprogram') {
                wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-appointment'})
              } else {
                window.location.href = "appointment.html";
              }
            }
            if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
              document.addEventListener('WeixinJSBridgeReady', ready1, false)
            } else {
             ready1()
            }
        },
        update: function() {
            if (authInfo && !authInfo.complete) {
                function ready2() {
                    store.set("authInfo", authInfo);
                    if (window.__wxjs_environment === 'miniprogram') {
                        wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-bind'})
                    } else {
                        window.location.href = "bind.html";
                    }
                    }
                    if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
                        document.addEventListener('WeixinJSBridgeReady', ready2, false)
                    } else {
                        ready2()
                    }
            }
        },
        memberRuler: function () {
          window.location.href = "member_ruler.html"
        },
        balance: function() {
            function ready3() {
              if (window.__wxjs_environment === 'miniprogram') {
                wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-balance'})
              } else {
                window.location.href = "balance.html";
              }
            }
            if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
              document.addEventListener('WeixinJSBridgeReady', ready3, false)
            } else {
              ready3()
            }
        },
        exchangenow:function(){
            function ready4() {
              if (window.__wxjs_environment === 'miniprogram') {
                wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-integralShop'})
              } else {
                window.location.href="integral/integralShop.html";
              }
            }
            if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
              document.addEventListener('WeixinJSBridgeReady', ready4, false)
            } else {
             ready4()
            }
        },
        order: function() {
          function ready5() {
              if (window.__wxjs_environment === 'miniprogram') {
              wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-order'})
              } else {
              window.location.href = "order.html";
              }
          }
          if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
              document.addEventListener('WeixinJSBridgeReady', ready5, false)
          } else {
              ready5()
          }
        },
        shopOrder: function () {
          function ready() {
              if (window.__wxjs_environment === 'miniprogram') {
                wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-youzan'})
              } else {
                window.location.href = "https://orchid.royalorchid.cn/youzanorders/";
              }
          }
          if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
              document.addEventListener('WeixinJSBridgeReady', ready, false)
          } else {
            ready()
          }
        },
        memdetail: function() {
            function ready6() {
                store.set("memberInfo", memberInfo);
                  if (window.__wxjs_environment === 'miniprogram') {
                      wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-memdetail'})
                  } else {
                    window.location.href = "memdetail.html";
                  }
            }
            if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
              document.addEventListener('WeixinJSBridgeReady', ready6, false)
              } else {
                  ready6()
           }
            
        },
        task: function() {
            function ready7() {
              if (window.__wxjs_environment === 'miniprogram') {
                wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-task'})
              } else {
                window.location.href = "task.html";
              }
            }
            if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
              document.addEventListener('WeixinJSBridgeReady', ready7, false)
            } else {
              ready7()
            }
        },
        exchange: function() {
          function ready8() {
             if (window.__wxjs_environment === 'miniprogram') {
                wx.miniProgram.navigateTo({url: '/pages/lanCard/lanCard-exchange'})
             } else {
                window.location.href = "exchange.html";
             }
          }
          if (!window.WeixinJSBridge || !WeixinJSBridge.invoke) {
             document.addEventListener('WeixinJSBridgeReady', ready8, false)
          } else {
             ready8()
          }
        }
    }) */
    // 分享
    //Lan.share()
});
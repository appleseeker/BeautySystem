using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinWeb.Code
{
    public class LoginHelper
    {
        public static string sessionKey = "234n2ir332nr23r";
        public static Common.Members GetUser()
        {
            var tmp= System.Web.HttpContext.Current.Session[sessionKey] as Common.Members;
            //if (tmp == null)
            //{
            //    System.Web.HttpContext.Current.Session["lastUrl"] = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            //    System.Web.HttpContext.Current.Response.Redirect($"https://open.weixin.qq.com/connect/oauth2/authorize?appid={Code.ConstStr.sAppID()}&redirect_uri={System.Web.HttpContext.Current.Server.UrlEncode("http://" + Code.ConstStr.SiteDomain() + "/Home/LoginCallBack")}&response_type=code&scope=snsapi_base&state=123#wechat_redirect");
            //    //System.Web.HttpContext.Current.Response.Redirect($"/Home/LoginTest");
            //    System.Web.HttpContext.Current.Response.End();
            //    throw new Common.ViewException("登录中");
            //}
            return tmp;
        }
        public static void Login(Common.Members user)
        {
            System.Web.HttpContext.Current.Session[sessionKey] = user;
        }
        
    }
}
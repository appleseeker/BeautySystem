using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Routing;
using Common;

namespace WeiXinWeb.Code
{
    
    //[HandleError(ExceptionType = typeof(ViewException), View = "ViewError")]
    //[HandleError(ExceptionType = typeof(AuthorityException), View = "ViewError")]
    
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(Request.Url.AbsoluteUri.ToLower().IndexOf("/home/LoginTest".ToLower()) != -1
                || Request.Url.AbsoluteUri.ToLower().IndexOf("/home/CheckSignature".ToLower()) != -1
                || Request.Url.AbsoluteUri.ToLower().IndexOf("/home/GetOpenID".ToLower()) != -1
                || Request.Url.AbsoluteUri.ToLower().IndexOf("/home/GetUser".ToLower()) != -1
                || Request.Url.AbsoluteUri.ToLower().IndexOf("/home/JSSetting".ToLower()) != -1)
            {
                return;
            }
            //return;
            if (Code.LoginHelper.GetUser() == null)
            {
                throw new Common.ViewException("未登录");
            }
            //if (Code.LoginHelper.GetUser().ID == null && Request.Url.AbsoluteUri.ToLower().IndexOf("/home/MeRegist".ToLower()) == -1)
            //{
            //    throw new Common.ViewException("未注册");
            //}
        }
        
    }
}
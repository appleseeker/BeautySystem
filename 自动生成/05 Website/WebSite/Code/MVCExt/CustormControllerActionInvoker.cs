using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebSite.Code.MVCExt
{
    public class CustormControllerActionInvoker : ControllerActionInvoker
    {
        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            try
            {
                var t = base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);


                if (!string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["htmlPost"])
                    || !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["htmlGet"])
                    || !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["jsonPost"]))
                {
                    var json = t as JsonResult;
                    var json2 = new JsonNetResult();
                    json2.Data = new JsonBizOut<object>() { IsSucceed = true, Mess = "成功", Data = json.Data };
                    t = json2;
                }
                return t;
            }
            catch (ViewException e)
            {

                if (!string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["htmlPost"])
                    || !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["htmlGet"])
                    || !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["jsonPost"]))
                {
                    JsonNetResult j = new JsonNetResult();
                    j.Data = new JsonBizOut<object>() { IsSucceed = false, Mess = e.Message, Data = null };
                    return j;
                }
                var typname = ((System.Web.Mvc.ReflectedActionDescriptor)actionDescriptor).MethodInfo.ReturnType.Name;//== "JsonResult"
                if (typname == "JsonResult")
                {
                    var tmp = new JsonResult();
                    tmp.Data = e.Message;
                    return tmp;
                }
                var view = new ViewResult();
                view.ViewName = "ViewError";
                view.ViewData = new ViewDataDictionary();
                view.ViewData.Add(new KeyValuePair<string, object>("Mess", e.Message));
                return view;
            }
            catch (AuthorityException e)
            {
                if (!string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["htmlPost"])
                    || !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["htmlGet"])
                    || !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["jsonPost"]))
                {
                    JsonNetResult j = new JsonNetResult();
                    j.Data = new JsonBizOut<object>() { IsSucceed = false, Mess = e.Message, Data = null };
                    return j;
                }
                var typname = ((System.Web.Mvc.ReflectedActionDescriptor)actionDescriptor).MethodInfo.ReturnType.Name;//== "JsonResult"
                if (typname == "JsonResult")
                {
                    var tmp = new JsonResult();
                    tmp.Data = e.Message;
                    return tmp;
                }
                var view = new ViewResult();
                view.ViewName = "ViewError";
                view.ViewData = new ViewDataDictionary();
                view.ViewData.Add(new KeyValuePair<string, object>("Mess", e.Message));
                return view;
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["htmlPost"])
                || !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["htmlGet"])
                || !string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString["jsonPost"]))
                {
                    JsonNetResult j = new JsonNetResult();
                    j.Data = new JsonBizOut<object>() { IsSucceed = false, Mess = "出现未知异常请联系管理员，异常信息：" + e.ToString(), Data = null };
                    return j;
                }
                var typname = ((System.Web.Mvc.ReflectedActionDescriptor)actionDescriptor).MethodInfo.ReturnType.Name;//== "JsonResult"
                if (typname == "JsonResult")
                {
                    var tmp = new JsonResult();
                    tmp.Data = e.ToString();
                    return tmp;
                }
                var view = new ViewResult();
                view.ViewName = "Error";
                view.ViewData = new ViewDataDictionary();
                view.ViewData.Add(new KeyValuePair<string, object>("Mess", e.ToString()));
                return view;
            }
        }

    }
}
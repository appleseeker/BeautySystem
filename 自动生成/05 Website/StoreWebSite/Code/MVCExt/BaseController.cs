using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace StoreWebSite.Code.MVCExt
{
    [HandleError(ExceptionType = typeof(ViewException), View = "ViewError")]
    [HandleError(ExceptionType = typeof(AuthorityException), View = "ViewError")]
    public class BaseController : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            return new CustormControllerActionInvoker();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (StoreWebSite.Controllers.HomeController.OpenAuth()
                && (!Code.LoginHelper.IsLogin())
                && filterContext.RouteData.Values["controller"].ToString().ToLower() != "Login".ToLower())
            {
                filterContext.HttpContext.Response.Redirect("/Home/login");
                return;
            }
            if (StoreWebSite.Controllers.HomeController.OpenAuth() && Code.LoginHelper.IsLogin() && filterContext.RouteData.Values["controller"].ToString().ToLower() == "Login".ToLower())
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        ///从http请求的指定key中通过json反射对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModeFromPostData<T>() where T:class
        {
            return GetModeFromPostData<T>("ModelJson");
        }
        /// <summary>
        /// 从http请求的指定key中通过json反射对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetModeFromPostData<T>(string key) where T : class
        {
            JavaScriptSerializer s = new JavaScriptSerializer();
                var t = s.Deserialize<T>(""+Request[key]);
                return t;
        }
        public JsonNetResult JsonNet(object o)
        {
            return new JsonNetResult(o);
        }
        /// <summary>
        /// 从requst中反射生成指定模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetRequestData<T>(T obj) where T : class
        {
            var type = obj.GetType();
            // obj = type.Assembly.CreateInstance(type.FullName);
            var pi = type.GetProperties();
            foreach (var item in pi)
            {
                if (!string.IsNullOrEmpty(Request[item.Name]))
                {
                    try
                    {
                        if (item.PropertyType.Equals(typeof(DateTime?)))
                        {
                            DateTime? time = string.IsNullOrEmpty(Request[item.Name]) ? (DateTime?)null : Convert.ToDateTime(Request[item.Name]);
                            item.SetValue(obj, time, null);
                        }
                        else if (item.PropertyType.Equals(typeof(Guid)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(Request[item.Name]) ? new Guid() : new Guid(Request[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(int)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(Request[item.Name]) ? 0 : Convert.ToInt32(Request[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(bool)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(Request[item.Name]) ? false : Convert.ToBoolean(Request[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(Guid)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(Request[item.Name]) ? new Guid() : new Guid(Request[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(Guid?)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(Request[item.Name]) ? null : (Guid?)new Guid(Request[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(bool?)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(Request[item.Name]) ? null : (bool?)Convert.ToBoolean(Request[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(int?)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(Request[item.Name]) ? null : (int?)Convert.ToInt32(Request[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(decimal?)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(Request[item.Name]) ? null : (decimal?)Convert.ToDecimal(Request[item.Name]), null);
                        }
                        else
                        {
                            item.SetValue(obj, Convert.ChangeType(Request[item.Name], item.PropertyType), null);
                        }
                    }
                    catch (Exception e) { }
                }
            }
            return (T)obj;
        }
        /// <summary>
        /// 读取request中form序列化之后的信息并返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetRequestData<T>(T obj, string key) where T : class
        {
            var type = obj.GetType();
            // obj = type.Assembly.CreateInstance(type.FullName);
            var pi = type.GetProperties();
            Dictionary<string, string> rq = new Dictionary<string, string>();
            var tmpstr = Request[key] + "";
            foreach (var item in tmpstr.Split("&".ToArray(),  StringSplitOptions.RemoveEmptyEntries))
            {
                rq[item.Split('=')[0]] = System.Web.HttpContext.Current.Server.UrlDecode( item.Split('=')[1]);
            }
            foreach (var item in pi)
            {
                if (rq.ContainsKey(item.Name) && !string.IsNullOrEmpty(rq[item.Name]))
                {
                    try
                    {
                        if (item.PropertyType.Equals(typeof(DateTime?)))
                        {
                            DateTime? time = string.IsNullOrEmpty(rq[item.Name]) ? (DateTime?)null : Convert.ToDateTime(rq[item.Name]);
                            item.SetValue(obj, time, null);
                        }
                        else if (item.PropertyType.Equals(typeof(Guid)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(rq[item.Name]) ? new Guid() : new Guid(rq[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(int)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(rq[item.Name]) ? 0 : Convert.ToInt32(rq[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(bool)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(rq[item.Name]) ? false : Convert.ToBoolean(rq[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(Guid)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(rq[item.Name]) ? new Guid() : new Guid(rq[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(Guid?)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(rq[item.Name]) ? null : (Guid?)new Guid(rq[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(bool?)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(rq[item.Name]) ? null : (bool?)Convert.ToBoolean(rq[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(int?)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(rq[item.Name]) ? null : (int?)Convert.ToInt32(rq[item.Name]), null);
                        }
                        else if (item.PropertyType.Equals(typeof(decimal?)))
                        {
                            item.SetValue(obj, string.IsNullOrEmpty(rq[item.Name]) ? null : (decimal?)Convert.ToDecimal(rq[item.Name]), null);
                        }
                        else
                        {
                            item.SetValue(obj, Convert.ChangeType(rq[item.Name], item.PropertyType), null);
                        }
                    }
                    catch (Exception e) { }
                }
            }
            return (T)obj;
        }
        /// <summary>
        /// 读取request中form序列化之后的信息并返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="loadDefault"></param>
        /// <returns></returns>
        public T GetRequestData<T>(T obj, bool loadDefault) where T : class
        {
            return GetRequestData<T>(obj,"formdata");
        }
    }
}
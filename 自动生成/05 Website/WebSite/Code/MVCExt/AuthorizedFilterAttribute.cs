using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Code.MVCExt
{
    /// <summary>
    /// 权限验证过滤器
    /// </summary>
    public class AuthorizedFilterAttribute : ActionFilterAttribute
    {
        public AuthorizedFilterAttribute(params string[] AuthorityName)
        {
            AuthorityNames = AuthorityName;
        }
        public string[] AuthorityNames { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!userAuthorizedHelper.UserHaveAuthorized(AuthorityNames))
            {
                throw new AuthorityException(string.Format("权限验证失败,没有'{0}'权限。", string.Join(",", AuthorityNames.Select(xxvv => xxvv.ToString()).ToArray())));
            }

            base.OnActionExecuting(filterContext);
        }
    }
    /// <summary>
    /// 用户权限验证帮助类
    /// </summary>
    public class userAuthorizedHelper
    {
        public static bool UserHaveAuthorized(params string[] authorizedName)
        {
            if (!WebSite.Controllers.HomeController.OpenAuth()) return true;
            var user = Code.LoginHelper.GetUser();
            if (user == null) return false;
            return user.AuthList.Any(f => authorizedName.Any(e=>e==f.Auth));
        }
        

        /// <summary>
        /// 过滤无权限菜单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, List<WebSite.Models.MenuItem>>> FilterMenu(List<KeyValuePair<string, List<WebSite.Models.MenuItem>>> list)
        {
            if (!Controllers.HomeController.OpenAuth()) return list;
            var user=Code.LoginHelper.GetUser();
            if (user == null) return new List<KeyValuePair<string, List<Models.MenuItem>>>();
            list.ForEach(f => {
                f.Value.RemoveAll(e => user.AuthList.All(g => g.Auth != e.AuthKey));
            });
            return list;
        }

    }



}
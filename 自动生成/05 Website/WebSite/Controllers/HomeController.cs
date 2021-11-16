using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public partial class HomeController :Code.MVCExt.BaseController
    {
        public  ActionResult Index()
        {
            if (!Code.LoginHelper.IsLogin())
            {
                return RedirectToAction("/Login/Login");
            }

            var menu = new List<KeyValuePair<string, List<WebSite.Models.MenuItem>>>();
            menu.Add(new KeyValuePair<string, List<WebSite.Models.MenuItem>>("业务管理", new List<WebSite.Models.MenuItem>() {
                new WebSite.Models.MenuItem("员工管理","/Users/Search","员工账户/员工管理/访问"),
                new WebSite.Models.MenuItem("会员管理","/Members/Search","会员/会员管理/访问"),
                new WebSite.Models.MenuItem("公司管理","/Companys/Search","公司/公司管理/访问"),
                new WebSite.Models.MenuItem("门店管理","/Stores/Search","门店/门店管理/访问"),
                new WebSite.Models.MenuItem("产品管理","/Projects/Search","产品/产品管理/访问"),
                new WebSite.Models.MenuItem("预约管理","/MemberOrders/Search","预约/预约管理/访问"),
            }));
            menu.Add(new KeyValuePair<string, List<WebSite.Models.MenuItem>>("系统管理", new List<WebSite.Models.MenuItem>() {
                new WebSite.Models.MenuItem("角色管理","/Roles/RoleList","角色管理/角色管理/访问"),
            }));
            return View(Code.MVCExt.userAuthorizedHelper.FilterMenu(menu));
        }
        public static bool OpenAuth()
        {
            return false;
        }

    }
}



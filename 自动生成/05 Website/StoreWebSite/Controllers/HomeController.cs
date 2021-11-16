using StoreWebSite.Code.MVCExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreWebSite.Controllers
{
    public partial class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (!Code.LoginHelper.IsLogin())
            {
                return RedirectToAction("Login");
            }

            var menu = new List<KeyValuePair<string, List<StoreWebSite.Models.MenuItem>>>();
            menu.Add(new KeyValuePair<string, List<StoreWebSite.Models.MenuItem>>("业务管理", new List<StoreWebSite.Models.MenuItem>() {
                new StoreWebSite.Models.MenuItem("员工管理","/Users/Search","员工账户/员工管理/访问"),
                new StoreWebSite.Models.MenuItem("会员管理","/Members/Search","会员/会员管理/访问"),
                new StoreWebSite.Models.MenuItem("公司管理","/Companys/Search","公司/公司管理/访问"),
                new StoreWebSite.Models.MenuItem("门店管理","/Stores/Search","门店/门店管理/访问"),
                new StoreWebSite.Models.MenuItem("产品管理","/Projects/Search","产品/产品管理/访问"),
                new StoreWebSite.Models.MenuItem("预约管理","/MemberOrders/Search","预约/预约管理/访问"),
            }));
            menu.Add(new KeyValuePair<string, List<StoreWebSite.Models.MenuItem>>("系统管理", new List<StoreWebSite.Models.MenuItem>() {
                new StoreWebSite.Models.MenuItem("角色管理","/Roles/RoleList","角色管理/角色管理/访问"),
            }));
            return View(Code.MVCExt.userAuthorizedHelper.FilterMenu(menu));
        }
        public static bool OpenAuth()
        {
            return false;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string id)
        {
            Code.LoginHelper.LoginThrow(Request["CompanyCode"], Request["name"], Request["pwd"]);
            return Json(1);
        }
        public ActionResult Logout()
        {
            Code.LoginHelper.Logout();
            return RedirectToAction("Login");
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public class LoginController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string id)
        {
            Code.LoginHelper.LoginThrow(Request["name"], Request["pwd"]);
            return Json(1);
        }
        public ActionResult Logout()
        {
            Code.LoginHelper.Logout();
            return RedirectToAction("Login");
        }
    }
}
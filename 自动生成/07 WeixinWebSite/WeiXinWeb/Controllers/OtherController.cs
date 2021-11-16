using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeiXinWeb.Controllers
{
    public class OtherController : Controller
    {
        // GET: Other
        public ActionResult CreateMenu()
        {
            Code.MenuHelper.Create();
            return View();
        }
    }
}
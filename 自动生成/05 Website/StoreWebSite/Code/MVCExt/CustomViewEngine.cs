using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreWebSite.Code.MVCExt
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            ViewLocationFormats = new[]  
            {  
                "~/Views/{1}/{0}.cshtml",  
               "~/Views/Shared/{0}.cshtml"
            };
        }
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            string endstr = "";
            if (Code.LanguageHelper.GetLanguage() == LanguageHelper.Language.English)
            {
                endstr = "_en";
            }
            if (Code.LanguageHelper.GetLanguage() == LanguageHelper.Language.Chinesef)
            {
                endstr = "_tr";
            }

            var v = base.FindView(controllerContext, viewName + endstr, masterName, useCache);
            if (v == null || v.View==null)
            {
                return base.FindView(controllerContext, viewName, masterName, useCache);
            }
            return v;
        }

    }
}
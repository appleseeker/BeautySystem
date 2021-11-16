using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;

namespace StoreWebSite.Code
{
    public static class HtmlHelperExtend
    {
        public static string GetSiteUrl( this HtmlHelper her)
        {
            return "~/";
        }
        
        public static void SetLanguage(LanguageHelper.Language language){
            LanguageHelper.SetLanguage(language);
        }

        public static string GetLanguageByKey(this HtmlHelper her, string Key)
        {
            return LanguageHelper.GetLanguageByKey(Key);
        }

        public static string GetLanguageByZh(this HtmlHelper her, string Zh)
        {
            return LanguageHelper.GetLanguageByZh(Zh);
        }
    }
}
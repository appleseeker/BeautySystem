using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace StoreWebSite.Code
{
    public static class LanguageHelper
    {
        static Dictionary<string, string> KeyToZh = new Dictionary<string, string>();
        static Dictionary<string, string> KeyToEn = new Dictionary<string, string>();
        static Dictionary<string, string> KeyToZhf = new Dictionary<string, string>();
        static Dictionary<string, string> ZhToEn = new Dictionary<string, string>();
        const string languageSessionKey="fasdrwer321awer13awe512t2";
        static Language l = Language.Chinese;

        public enum Language
        {
            Chinese,Chinesef, English
        }

        static LanguageHelper()
        {
           string file= System.Web.HttpContext.Current.Server.MapPath( HtmlHelperExtend.GetSiteUrl(null)+"Language.config");
           if (!File.Exists(file))
           {
               throw new Exception(LanguageHelper.GetLanguageByKey("未能找到站点语言配置文件“Language.config”")+"。"+LanguageHelper.GetLanguageByKey("请确认根目录是否有此文件")+"！");
           }
           System.Xml.XmlDataDocument xml = new System.Xml.XmlDataDocument();
           try
           {
               xml.Load(file);

               var x = xml.ChildNodes[2];
               foreach (System.Xml.XmlNode item in x)
               {
                   try
                   {
                       ZhToEn[item.Attributes["zh"].Value] = item.Attributes["en"].Value;
                       KeyToZh[item.Attributes["key"].Value] = item.Attributes["zh"].Value;
                       KeyToZhf[item.Attributes["key"].Value] = item.Attributes["zhf"].Value;
                       KeyToEn[item.Attributes["key"].Value] = item.Attributes["en"].Value;
                   }
                   catch { }
               }
           }
           catch {
               throw new Exception(LanguageHelper.GetLanguageByKey("配置文件")+"“Language.config”"+LanguageHelper.GetLanguageByKey("加载失败")+"。"+LanguageHelper.GetLanguageByKey("请检查配置文件是否符合dtd约束")+"！");
           }

        }
        public static Language GetLanguage()
        {
          object mylanguage = System.Web.HttpContext.Current.Session[languageSessionKey];
            if(mylanguage==null){

                //2012-5-14 如果Session丢失尝试从cookie读取
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get("language");
                if (cookie != null)
                {
                    if (cookie.Value == "zh-cn")
                    {
                        System.Web.HttpContext.Current.Session[languageSessionKey] = (int)Language.Chinese;
                        return Language.Chinese;
                    }
                    else
                    {
                        System.Web.HttpContext.Current.Session[languageSessionKey] = (int)Language.English;
                        return Language.English;
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Session[languageSessionKey] = (int)l;
                    return l;
                }
            }
            else
            {
                Language mylan= (Language)(int)mylanguage;
                return mylan;
            }
        }
        public static void SetLanguage(Language language){
            l = language;
        }
        public static void SetUserLanguage(Language language)
        {
            System.Web.HttpContext.Current.Session[languageSessionKey] = (int)language;
        }


        public static string GetLanguageByKey( string Key)
        {
            string output = "";
            switch (GetLanguage())
            {
                case Language.Chinese:
                    KeyToZh.TryGetValue(Key, out output);
                    break;
                case Language.English:
                    KeyToEn.TryGetValue(Key, out output);
                    break;
                case Language.Chinesef:
                    KeyToZhf.TryGetValue(Key, out output);
                    break;
                default:
                    KeyToZh.TryGetValue(Key, out output);
                    break;
            }
            if (string.IsNullOrEmpty(output))
            {
                switch (l)
                {
                    case  Language.Chinese:
                        output = "错误的配置文件项。 key=“" + Key + "”";
                        break;
                    case Language.English:
                        output = "error config item. key=“" + Key + "”";
                        break;
                    default:
                        break;
                }
            }
            return output;
        }

        public static string GetLanguageByZh(string Zh)
        {
            string output ="";
            switch (GetLanguage())
            {
                case Language.Chinese:
                    output = Zh;
                    break;
                case Language.English:
                    ZhToEn.TryGetValue(Zh, out output);
                    break;
                default:
                    output = Zh;
                    break;
            }
            if (string.IsNullOrEmpty(output))
            {
                switch (l)
                {
                    case Language.Chinese:
                        output = "错误的配置文件项。 zh=“" + Zh + "”";
                        break;
                    case Language.English:
                        output = "error config item. zh=“" + Zh + "”";
                        break;
                    default:
                        break;
                }
            }
            return output;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WeiXinWeb.Code
{
    public class ConstStr
    {
        public static string sToken()
        {
            return System.Configuration.ConfigurationManager.AppSettings["sToken"];
        }
        public static string sAppID()
        {
            return GetCompany().sAppID; 
        }
        public static string sEncodingAESKey()
        {
            return GetCompany().sEncodingAESKey;
        }
        
        public static string secret()
        {
            return GetCompany().secret;
        }
        /// <summary>
        /// 接入点
        /// </summary>
        /// <returns></returns>
        public static string OpenAddr()
        {
            return System.Configuration.ConfigurationManager.AppSettings["OpenAddr"];
        }

        public static string GetMenuInit()
        {
            var tmp= File.ReadAllText(HttpContext.Current.Server.MapPath("~/Menu.json"));
            tmp = tmp.Replace("#SiteDomain#", SiteDomain());//转换域名
            return tmp;
        }

        public static string SiteDomain()
        {
            if (IsTest())
            {
                return "clearsk.beautydate.cn";
            }
            else
            {
                Business.SysHelper.AddLog("xxxx", HttpContext.Current.Request.Url.Host);
                return HttpContext.Current.Request.Url.Host;
            }
        }

        public static Common.Companys GetCompany()
        {
            var tmp = SiteDomain();
            var company= Business.Companys.GetCompanysBySiteDomain(tmp);
            return company;
        }

        public static bool IsTest()
        {
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Common
{
    public class IPHelper
    {
        public static string GetUserIP(HttpRequestBase request)
        {
            string result = String.Empty;
            result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //如果有代理
            if (!string.IsNullOrEmpty(result))
            {
                string[] temparyip = result.Replace(" ", "").Replace("'", "").Split(',');
                string ip = temparyip.FirstOrDefault(); //取最后个代理IP
                if (StringExtensions.IsIPAddress(ip)
                    && ip.Substring(0, 3) != "10."
                    && ip.Substring(0, 7) != "192.168"
                    && ip.Substring(0, 7) != "172.16.")
                    return ip;
                else
                    return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                if (HttpContext.Current.Request.Headers["Cdn-Src-Ip"] != null)
                    result = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                else if(HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                    result = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                else if(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                else
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }

        public static string GetAdminLoginIP(HttpRequestBase request)
        {
            string result = String.Empty;
            result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //如果有代理
            if (!string.IsNullOrEmpty(result))
            {
                //前缀
                string prefix = "daili"+new Random().Next(0, 100)+":";
                string[] temparyip = result.Replace(" ", "").Replace("'", "").Split(',');
                string ip = temparyip.FirstOrDefault(); //取最后个代理IP
                if (StringExtensions.IsIPAddress(ip)
                    && ip.Substring(0, 3) != "10."
                    && ip.Substring(0, 7) != "192.168"
                    && ip.Substring(0, 7) != "172.16.")
                    return prefix+ip;
                else
                    return prefix + HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                if (HttpContext.Current.Request.Headers["Cdn-Src-Ip"] != null)
                    result = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                else if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                    result = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                else
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }

        public static string GetUserIP(HttpRequest request)
        {
            string result = String.Empty;
            result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //如果有代理
            if (!string.IsNullOrEmpty(result))
            {
                string[] temparyip = result.Replace(" ", "").Replace("'", "").Split(',');
                string ip = temparyip.FirstOrDefault(); //取最后个代理IP
                if (StringExtensions.IsIPAddress(ip)
                    && ip.Substring(0, 3) != "10."
                    && ip.Substring(0, 7) != "192.168"
                    && ip.Substring(0, 7) != "172.16.")
                    return ip;
                else
                    return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                if (HttpContext.Current.Request.Headers["Cdn-Src-Ip"] != null)
                    result = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                else if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                    result = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                else
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }
    }
}

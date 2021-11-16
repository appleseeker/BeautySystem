using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinWeb.Code
{
    public class AccessTokenHelper
    {
        public static string AccessToken()
        {
           return Code.CacheHelper.Get<string>("AccessToken", () => {
                string url = $"https://{Code.ConstStr.OpenAddr()}/cgi-bin/token?grant_type=client_credential&appid={Code.ConstStr.sAppID()}&secret={Code.ConstStr.secret()}";
                System.Net.WebClient client = new System.Net.WebClient();
                string output = client.DownloadString(url);
                Business.SysHelper.AddLog("AccessToken", output);
                var map = Common.JsonHelper.Disable<Dictionary<string, string>>(output);
                if (!map.ContainsKey("access_token"))
                {
                    throw new Common.ViewException("AccessToken获取失败，请查阅日志");
                }
                return map["access_token"];
            }, 100 * 60);
        }
    }
}
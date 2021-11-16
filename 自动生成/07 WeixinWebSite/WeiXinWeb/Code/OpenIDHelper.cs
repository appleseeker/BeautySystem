using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinWeb.Code
{
    public class OpenIDHelper
    {
        public static Common.Members OpenID(string code)
        {
            var user = new Common.Members();
            string access_token = null;
            Business.SysHelper.AddLog("GetOpenID", code);
            {
                string url = $"https://{Code.ConstStr.OpenAddr()}/sns/oauth2/access_token?appid={Code.ConstStr.sAppID()}&secret={Code.ConstStr.secret()}&code={code}&grant_type=authorization_code";
                System.Net.WebClient client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                string output = client.DownloadString(url);
                Business.SysHelper.AddLog("GetOpenID", output);
                var map = Common.JsonHelper.Disable<Dictionary<string, string>>(output);
                if (!map.ContainsKey("openid") && !map.ContainsKey("access_token"))
                {
                    throw new Common.ViewException("openid/access_token获取失败，请查阅日志");
                }
                user.OpenID = map["openid"];
                access_token = map["access_token"];
            }
            var member = Business.Members.GetOneByOpenID(user.OpenID);
            if(member==null)
            {//获取用户基本信息
                string url = $"https://{Code.ConstStr.OpenAddr()}/sns/userinfo?access_token={access_token}&openid={user.OpenID}&lang=zh_CN";
                System.Net.WebClient client = new System.Net.WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                string output = client.DownloadString(url);
                Business.SysHelper.AddLog("Getuserinfo", output);
                MongoDB.Bson.BsonDocument.Parse(output);
                var map = MongoDB.Bson.BsonDocument.Parse(output);
                if (!map.Contains("openid"))
                {
                    throw new Common.ViewException("openid获取失败，请查阅日志");
                }
                user.NickName = map["nickname"].ToString();
                user.Sex = map["sex"].ToString() == "1" ? "男" : "女";
                user.Picture = map["headimgurl"].ToString();
                user.Address = $"{ map["country"].ToString()} {map["city"].ToString()} {map["province"].ToString()}";
            }
            return user;
        }
        
    }
}
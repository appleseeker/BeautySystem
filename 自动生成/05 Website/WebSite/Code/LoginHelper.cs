using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Code
{
    public class LoginHelper
    {
        private const string UserSessionKey = "xxxxxxx9342n342";
        /// <summary>
        /// 登陆用户，如果失败则抛出异常
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        public static void LoginThrow(string name,string pwd)
        {
            if (!Login(name, pwd))
            {
               throw new Common.AuthorityException("账户或密码不正确");
            }
        }
        public static bool Login(string name, string pwd)
        {
            var u=Business.Users.GetOneAuth(name);
            if (u == null) return false;
            if (u.Pwd != pwd) return false;

            HttpContext.Current.Session[UserSessionKey] = u;
            return true;
        }
        public static void Logout()
        {
            HttpContext.Current.Session[UserSessionKey] = null;
        }
        /// <summary>
        /// 判断用户是否登陆
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            if (GetUser() == null) return false;
            return true;
        }
        /// <summary>
        /// 获取登陆用户信息
        /// </summary>
        /// <returns></returns>
        public static Common.Users GetUser()
        {
            var sessionUser = HttpContext.Current.Session[UserSessionKey];
            if (sessionUser == null) return null;
            var user = sessionUser as Common.Users;
            if (user == null) return null;
            return user;
        }
    }
}
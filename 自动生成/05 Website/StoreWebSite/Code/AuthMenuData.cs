using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreWebSite.Models;

namespace StoreWebSite.Code
{
    public class AuthMenuData
    {
        public static KeyValuePair<string, List<MenuItem>> Get()
        {
            return new KeyValuePair<string, List<MenuItem>>("系统管理", new List<MenuItem>() {
                new MenuItem("系统账户","/Users/UserList","系统账户/系统账户/访问"),
                new MenuItem("角色管理","/Roles/RoleList","角色管理/角色管理/访问"),
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WeiXinWeb.Code
{
    public class MenuHelper
    {
        /// <summary>
        /// 创建微信菜单
        /// </summary>
        public static void Create()
        {
            try
            {
                var token = Code.AccessTokenHelper.AccessToken();
                string url = $"https://{Code.ConstStr.OpenAddr()}/cgi-bin/menu/create?access_token={token}";

                Common.HttpUtil httpUtil = new Common.HttpUtil();
                var output = httpUtil.DoPost2(url, Code.ConstStr.GetMenuInit(), Encoding.UTF8);
                Business.SysHelper.AddLog("CreateMenu", output);
                return;
            }
            catch (Exception ex)
            {
                Business.SysHelper.AddLog("异常", ex.ToString());
            }
        }
    }
}
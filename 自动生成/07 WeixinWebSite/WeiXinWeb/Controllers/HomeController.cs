using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WeiXinWeb.Controllers
{
    public class HomeController : Code.BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// 微信验证地址
        /// </summary>
        /// <returns></returns>

        public ActionResult CheckSignature()
        {
            Business.SysHelper.AddLog("CheckSignature", $"{Request["signature"]}|{Request["timestamp"]}|{Request["nonce"]}|{Request["echostr"]}");
            //从微信服务器 传递过来的数据
            string signature = Request.QueryString["signature"]; //微信加密签名
            string timestamp = Request.QueryString["timestamp"];//时间戳
            string nonce = Request.QueryString["nonce"];//随机数
            string echostr = Request.QueryString["echostr"];   //随机字符串

            //将token、timestamp、nonce三个参数进行字典序排序
            string[] arr = { Code.ConstStr.sToken(), timestamp, nonce };
            Array.Sort(arr);

            //将三个参数字符串拼接成一个字符串进行sha1加密
            string str = string.Join("", arr);
            str = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "sha1").ToLower();

            //开发者获得加密后的字符串可与signature对比，标识该请求来源于微信，如果一致则成功
            if (str == signature)
            {
                Response.Write(echostr);
                return null;
            }
            return null;
        }
        
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public JsonResult MeRegist(Common.Members members)
        {
            if (members != null && (!string.IsNullOrEmpty(members.Phone)))
            {
               return Json(Models.Bizout<string>.Action(() => {
                   Business.SysHelper.AddLog("Regist", Common.JsonHelper.Sazeable<Common.Members>(members));
                   var m = Code.LoginHelper.GetUser();
                   if (m == null) throw new Common.ViewException("未登录");
                   


                   if (!m.ID.HasValue)
                   {
                       members.OpenID = m.OpenID;
                       members.CreateTime = DateTime.Now;
                       members.Picture = m.Picture;
                       members.NickName = m.NickName;
                       members.CompanyID = Code.ConstStr.GetCompany().ID;
                       members.Address = m.Address;
                       members.ID = Guid.NewGuid();
                       Business.Members.Add(members);
                       Code.LoginHelper.Login(members);
                   }
                   else
                   {
                       m.Sex = members.Sex;
                       m.Phone = members.Phone;
                       m.RealName = members.RealName;
                       Business.Members.Edit(m);
                   }
                   if (string.IsNullOrEmpty(Session["lastUrl"] as string))
                   {
                       return "/Content/html/Member.html";
                   }
                   else
                   {
                       return Session["lastUrl"] as string;
                   }
               }));
            }
            return Json(new Models.Bizout<string>() { code = -1, errorMsg = "提交的数据不合理，无法处理", data = "" });
        }
        /// <summary>
        /// 测试登陆
        /// </summary>
        /// <returns></returns>

        public ActionResult LoginTest()
        {
            Code.LoginHelper.Login(Business.Members.GetOne(Guid.Parse("6825C9B9-CF49-4FE5-B5AD-609866421180")));
            if (string.IsNullOrEmpty(Session["lastUrl"] as string))
            {
                return Redirect("/Content/html/Index.html");
            }
            else
            {
                return Redirect(Session["lastUrl"] as string);
            }
        }
        
        /// <summary>
        /// 获取当前登陆用户的信息
        /// </summary>
        /// <returns></returns>

        public JsonResult GetUser()
        {
            //测试用代码
            return Json(Models.Bizout<Common.Members>.Action(() => {
                return Code.LoginHelper.GetUser();
            }));
        }
        /// <summary>
        /// 根据页面回调的code获取微信Openid
        /// 本逻辑相当于登陆，
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public JsonResult GetOpenID(string code)
        {
            return Json(Models.Bizout<string>.Action(() => {
                string openid = null;
                Common.Members tmpuser = null;
                if (Code.ConstStr.IsTest())
                {
                    openid = code;
                    tmpuser = new Common.Members() { OpenID = openid,NickName="test" };
                }
                else
                {
                    tmpuser= Code.OpenIDHelper.OpenID(code);
                    openid = tmpuser.OpenID;
                }
                var member = Business.Members.GetOneByOpenID(openid);
                if (member == null)
                {
                    //注册
                    Code.LoginHelper.Login(tmpuser);
                }
                else
                {
                    //登陆
                    Code.LoginHelper.Login(member);
                }
                return openid;
            }));
        }

        /// <summary>
        /// 获取当前域名对应的微信公众号配置信息
        /// </summary>
        /// <returns></returns>
        public JsonResult JSSetting()
        {
            return Json(Models.Bizout<Common.Companys>.Action(() => {
                var SiteDomain = Code.ConstStr.SiteDomain();
                var data = Code.ConstStr.GetCompany();
                data.sToken = "";
                data.sEncodingAESKey = "";
                data.secret = "";
                return data;
            }));
        }
    }
}
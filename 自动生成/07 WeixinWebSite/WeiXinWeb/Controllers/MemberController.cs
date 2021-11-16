using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WeiXinWeb.Controllers
{
    public class MemberController : Code.BaseController
    {
        /// <summary>
        /// 获取当前域名对应的微信公众号配置信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMember()
        {
            return Json(Models.Bizout<object>.Action(() => {
                var user = Code.LoginHelper.GetUser();
                var MemberOrders = Business.MemberOrders.GetListByMemberID(user.ID.Value);
                return new { member = user, paddingCount= MemberOrders.Count(g => g.Status == "未开始"), ProcessedCount=MemberOrders.Count(g=> g.Status != "未开始") };
            }));
        }

        public JsonResult GetOrder()
        {
            return new Code.JsonNetResult(Models.Bizout<object>.Action(() => {
                
                var user = Code.LoginHelper.GetUser();
                var MemberOrders = Business.MemberOrders.GetListByMemberID(user.ID.Value);
                if (string.IsNullOrEmpty(Request["type"]))
                {
                    return MemberOrders.Where(g => g.Status=="未开始").OrderBy(g=>g.OrderTime);
                }
                else
                {
                    return MemberOrders.Where(g => g.Status != "未开始").OrderBy(g => g.OrderTime);
                }
            }),"yyyy年MM月dd日 HH:mm");
        }

        public JsonResult GetStores()
        {
            return Json(Models.Bizout<object>.Action(() => {
                return Business.Stores.GetAll(Code.ConstStr.GetCompany().ID.Value);
            }));
        }

        public JsonResult GetProjects()
        {
            var storeName = Request["storeName"];
            return Json(Models.Bizout<object>.Action(() => {
                //if (string.IsNullOrEmpty(storeName))
                //{
                //    throw new Common.ViewException("请先选择门店");
                //}
                return Business.Projects.GetAll(storeName);
            }));
        }
        public JsonResult GetImages()
        {
            return Json(Models.Bizout<object>.Action(() => {
                return Business.Images.GetImagesByStoresID(Guid.Parse(Request["StoresID"])).GroupBy(g=>g.Type).ToList();
            }));
        }
        public JsonResult GetEmployees()
        {
            var StoreName = Request["StoreID"];
            return Json(Models.Bizout<object>.Action(() => {
                if (string.IsNullOrEmpty(StoreName))
                {
                    throw new Common.ViewException("请先选择门店");
                }
                var tmp= Business.Employees.GetAllbyStoreID(StoreName);
                tmp.ForEach(g =>
                {
                    g.Account = "";
                    g.Pwd = "";
                });
                return tmp;
            }));
        }

        public JsonResult SaveMemberOrders(Common.MemberOrders memberOrders)
        {
            return Json(Models.Bizout<object>.Action(() => {
                memberOrders.ID = Guid.NewGuid();
                memberOrders.MemberID = Code.LoginHelper.GetUser().ID;
                memberOrders.CreateTime = DateTime.Now;
                memberOrders.Source = "在线";
                memberOrders.Status = "未开始";
                //查询价格
                var pj=Business.Projects.GetOne(memberOrders.ProjectID.Value);
                memberOrders.Price = (decimal)pj.DiscountPrice.Value;
                memberOrders.NeedTime = pj.NeedTime;
                Business.MemberOrders.Add(memberOrders);
                return 1;
            }));
        }

        public JsonResult GetOrderOne()
        {
            return new Code.JsonNetResult(Models.Bizout<object>.Action(() => {
                var id = Guid.Parse(Request["id"]);
                return Business.MemberOrders.GetOne(id);
            }), "yyyy年MM月dd日 HH:mm");
        }

        public JsonResult CancalOrder()
        {
            return Json(Models.Bizout<object>.Action(() => {
                var id = Guid.Parse(Request["id"]);
                return Business.MemberOrders.quxiao(new Common.MemberOrders_quxiaoAction() {  ID=id});
            }));
        }

        public JsonResult GetStoreOne()
        {
            return Json(Models.Bizout<object>.Action(() => {
                var id = Guid.Parse(Request["id"]);
                var s1 = Business.Stores.GetOne(id);
                var s2 = Business.Employees.GetAllbyStoreID(s1.StoreName);
                var s3 = Business.Images.GetImagesByStoresID(id).GroupBy(g=>g.Type).ToList();
                return new { s1, s2, s3 };
            }));
        }

        public JsonResult GetProjectOne()
        {
            var storeName = Request["id"];
            return Json(Models.Bizout<object>.Action(() => {
                if (string.IsNullOrEmpty(storeName))
                {
                    throw new Common.ViewException("参数不正确。。。。");
                }
                return Business.Projects.GetOne(Guid.Parse(storeName));
            }));
        }

        public JsonResult GetProducts()
        {
            return Json(Models.Bizout<object>.Action(() => {
                return Business.CompanyProducts.GetAll(Code.ConstStr.GetCompany().ID.Value);
            }));
        }

        public JsonResult GetProductsOne()
        {
            var storeName = Request["id"];
            return Json(Models.Bizout<object>.Action(() => {
                if (string.IsNullOrEmpty(storeName))
                {
                    throw new Common.ViewException("参数不正确。。。。");
                }
                return Business.CompanyProducts.GetOne(Guid.Parse(storeName));
            }));
        }



    }
}
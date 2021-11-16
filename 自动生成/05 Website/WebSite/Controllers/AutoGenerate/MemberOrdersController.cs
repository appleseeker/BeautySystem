using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class MemberOrdersController :Code.MVCExt.BaseController
    {
        public  JsonResult StoresList_Source()
        {
            return JsonNet(DataBase.EnumList.StoresList());
        }
        public  JsonResult MemberList_Source()
        {
            return JsonNet(DataBase.EnumList.MemberList());
        }
        public  JsonResult EmployeeList_Source()
        {
            return JsonNet(DataBase.EnumList.EmployeeList());
        }
        public  JsonResult ProjectList_Source()
        {
            return JsonNet(DataBase.EnumList.ProjectList());
        }
        public  JsonResult MemberOrdersStatus_Source()
        {
            return JsonNet(DataBase.EnumList.MemberOrdersStatus());
        }
        [AuthorizedFilter("预约/添加预约/访问")]
        public  PartialViewResult Add()
        {
            return PartialView(new Common.MemberOrders() {ID=Guid.NewGuid(),Status="待确认"});
        }
        [AuthorizedFilter("预约/添加预约/访问")]
        public  JsonResult Add_(Common.MemberOrders memberorders)
        {
            return JsonNet(Business.MemberOrders.Add(memberorders));
        }
        [AuthorizedFilter("预约/编辑预约信息/访问")]
        public  PartialViewResult Edit()
        {
            return PartialView(Business.MemberOrders.GetOne(Guid.Parse(Request["ID"])));
        }
        [AuthorizedFilter("预约/编辑预约信息/访问")]
        public  JsonResult Edit_(Common.MemberOrders memberorders)
        {
            return JsonNet(Business.MemberOrders.Edit(memberorders));
        }
        [AuthorizedFilter("预约/预约管理/访问")]
        public  PartialViewResult Search()
        {
            return PartialView();
        }
        [AuthorizedFilter("预约/预约管理/访问")]
        public  JsonResult Search_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.MemberOrders_SearchQuery>(new Common.MemberOrders_SearchQuery());
            var data=Business.MemberOrders.Search(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.TotalCount });
        }
        [AuthorizedFilter("预约/预约管理/删除")]
        public  JsonResult Search_Delete(Common.MemberOrders_DeleteAction model)
        {
            return JsonNet(Business.MemberOrders.Delete(model));
        }
        [AuthorizedFilter("预约/预约管理/完成")]
        public  JsonResult Search_wancheng(Common.MemberOrders_wanchengAction model)
        {
            return JsonNet(Business.MemberOrders.wancheng(model));
        }
        [AuthorizedFilter("预约/预约管理/取消")]
        public  JsonResult Search_quxiao(Common.MemberOrders_quxiaoAction model)
        {
            return JsonNet(Business.MemberOrders.quxiao(model));
        }

    }
}



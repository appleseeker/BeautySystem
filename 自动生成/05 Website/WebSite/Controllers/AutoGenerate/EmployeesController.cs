using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class EmployeesController :Code.MVCExt.BaseController
    {
        public  JsonResult CompanyList_Source()
        {
            return JsonNet(DataBase.EnumList.CompanyList());
        }
        public  JsonResult StoresList_Source()
        {
            return JsonNet(DataBase.EnumList.StoresList());
        }
        public  JsonResult SexList_Source()
        {
            return JsonNet(DataBase.EnumList.SexList());
        }
        [AuthorizedFilter("员工/添加员工/访问")]
        public  PartialViewResult Add()
        {
            return PartialView(new Common.Employees() {ID=Guid.NewGuid()});
        }
        [AuthorizedFilter("员工/添加员工/访问")]
        public  JsonResult Add_(Common.Employees employees)
        {
            return JsonNet(Business.Employees.Add(employees));
        }
        [AuthorizedFilter("员工/编辑员工信息/访问")]
        public  PartialViewResult Edit()
        {
            return PartialView(Business.Employees.GetOne(Guid.Parse(Request["ID"])));
        }
        [AuthorizedFilter("员工/编辑员工信息/访问")]
        public  JsonResult Edit_(Common.Employees employees)
        {
            return JsonNet(Business.Employees.Edit(employees));
        }
        [AuthorizedFilter("员工/员工管理/访问")]
        public  PartialViewResult Search()
        {
            return PartialView();
        }
        [AuthorizedFilter("员工/员工管理/访问")]
        public  JsonResult Search_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.Employees_SearchQuery>(new Common.Employees_SearchQuery());
            var data=Business.Employees.Search(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.TotalCount });
        }
        [AuthorizedFilter("员工/员工管理/删除")]
        public  JsonResult Search_Delete(Common.Employees_DeleteAction model)
        {
            return JsonNet(Business.Employees.Delete(model));
        }

    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class UsersController :Code.MVCExt.BaseController
    {
        public  JsonResult Users_LoginNameOnly_AjaxVerify(Common.Users model)
        {
            model.LoginName=Request.Form["LoginName"];
            return JsonNet(DataBase.AjaxVerify.Users_LoginNameOnly(model));
        }
        public  JsonResult EnableDisable_Source()
        {
            return JsonNet(DataBase.EnumList.EnableDisable());
        }
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
        [AuthorizedFilter("员工账户/添加员工/访问")]
        public  PartialViewResult Add()
        {
            return PartialView(new Common.Users() {ID=Guid.NewGuid(),Status="启用"});
        }
        [AuthorizedFilter("员工账户/添加员工/访问")]
        public  JsonResult Add_(Common.Users users)
        {
            return JsonNet(Business.Users.Add(users));
        }
        [AuthorizedFilter("员工账户/编辑员工信息/访问")]
        public  PartialViewResult Edit()
        {
            return PartialView(Business.Users.GetOne(Guid.Parse(Request["ID"])));
        }
        [AuthorizedFilter("员工账户/编辑员工信息/访问")]
        public  JsonResult Edit_(Common.Users users)
        {
            return JsonNet(Business.Users.Edit(users));
        }
        [AuthorizedFilter("员工账户/员工管理/访问")]
        public  PartialViewResult Search()
        {
            return PartialView();
        }
        [AuthorizedFilter("员工账户/员工管理/访问")]
        public  JsonResult Search_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.Users_SearchQuery>(new Common.Users_SearchQuery());
            var data=Business.Users.Search(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.TotalCount });
        }
        [AuthorizedFilter("员工账户/员工管理/删除")]
        public  JsonResult Search_Delete(Common.Users_DeleteAction model)
        {
            return JsonNet(Business.Users.Delete(model));
        }
        [AuthorizedFilter("员工账户/员工管理/禁用")]
        public  JsonResult Search_Disable(Common.Users_DisableAction model)
        {
            return JsonNet(Business.Users.Disable(model));
        }
        [AuthorizedFilter("员工账户/员工管理/启用")]
        public  JsonResult Search_Enable(Common.Users_EnableAction model)
        {
            return JsonNet(Business.Users.Enable(model));
        }

    }
}



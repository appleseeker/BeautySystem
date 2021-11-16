using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class RolesController :Code.MVCExt.BaseController
    {
        public  JsonResult RoleOnly_AjaxVerify(Common.Roles model)
        {
            model.Name=Request.Form["Name"];
            return JsonNet(DataBase.AjaxVerify.RoleOnly(model));
        }
        [AuthorizedFilter("角色管理/角色管理/访问")]
        public  PartialViewResult RoleList()
        {
            return PartialView();
        }
        [AuthorizedFilter("角色管理/角色管理/访问")]
        public  JsonResult RoleList_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.Roles_RoleListQuery>(new Common.Roles_RoleListQuery());
            var data=Business.Roles.RoleList(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.Count });
        }
        [AuthorizedFilter("角色管理/角色管理/删除")]
        public  JsonResult RoleList_Delete(Common.Roles_DeleteAction model)
        {
            return JsonNet(Business.Roles.Delete(model));
        }
        [AuthorizedFilter("角色管理/新建角色/访问")]
        public  PartialViewResult RoleAdd()
        {
            return PartialView(new Common.Roles() {ID=Guid.NewGuid()});
        }
        [AuthorizedFilter("角色管理/新建角色/访问")]
        public  JsonResult RoleAdd_(Common.Roles roles)
        {
            return JsonNet(Business.Roles.RoleAdd(roles));
        }

    }
}



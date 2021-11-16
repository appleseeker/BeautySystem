using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class UsersRolesMapController :Code.MVCExt.BaseController
    {
        public  JsonResult RoleList_Source()
        {
            return JsonNet(DataBase.EnumList.RoleList());
        }
        public  JsonResult UserRolesMapOnly_AjaxVerify(Common.UsersRolesMap model)
        {
            model.RoleName=Request.Form["RoleName"];
            return JsonNet(DataBase.AjaxVerify.UserRolesMapOnly(model));
        }
        [AuthorizedFilter("账户角色/账户角色列表/访问")]
        public  PartialViewResult UsersRolesMapList()
        {
            return PartialView();
        }
        [AuthorizedFilter("账户角色/账户角色列表/访问")]
        public  JsonResult UsersRolesMapList_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.UsersRolesMap_UsersRolesMapListQuery>(new Common.UsersRolesMap_UsersRolesMapListQuery());
            var data=Business.UsersRolesMap.UsersRolesMapList(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.Count });
        }
        [AuthorizedFilter("账户角色/账户角色列表/删除")]
        public  JsonResult UsersRolesMapList_Delete(Common.UsersRolesMap_DeleteAction model)
        {
            return JsonNet(Business.UsersRolesMap.Delete(model));
        }
        [AuthorizedFilter("账户角色/添加角色/访问")]
        public  PartialViewResult AddRole()
        {
            return PartialView(new Common.UsersRolesMap() {ID=Guid.NewGuid(),UserID=Guid.Parse(Request["UserID"])});
        }
        [AuthorizedFilter("账户角色/添加角色/访问")]
        public  JsonResult AddRole_(Common.UsersRolesMap usersrolesmap)
        {
            return JsonNet(Business.UsersRolesMap.AddRole(usersrolesmap));
        }

    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class RoleAuthMapController :Code.MVCExt.BaseController
    {
        [AuthorizedFilter("角色权限/权限列表/访问")]
        public  PartialViewResult RoleAuthMapList()
        {
            return PartialView();
        }
        [AuthorizedFilter("角色权限/权限列表/访问")]
        public  JsonResult RoleAuthMapList_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.RoleAuthMap_RoleAuthMapListQuery>(new Common.RoleAuthMap_RoleAuthMapListQuery());
            var data=Business.RoleAuthMap.RoleAuthMapList(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.Count });
        }
        [AuthorizedFilter("角色权限/权限列表/删除")]
        public  JsonResult RoleAuthMapList_Delete(Common.RoleAuthMap_DeleteAction model)
        {
            return JsonNet(Business.RoleAuthMap.Delete(model));
        }
        [AuthorizedFilter("角色权限/编辑权限/访问")]
        public  PartialViewResult AddAuth()
        {
            return PartialView(new Common.RoleAuthMap() {ID=Guid.NewGuid(),RoleID=Guid.Parse(Request["RoleID"])});
        }
        //[AuthorizedFilter("角色权限/编辑权限/访问")]
        //public  JsonResult AddAuth_(Common.RoleAuthMap roleauthmap)
        //{
        //    return JsonNet(Business.RoleAuthMap.AddAuth(roleauthmap));
        //}
        [AuthorizedFilter("角色权限/编辑权限/访问")]
        public JsonResult AuthList(Common.RoleAuthMap roleauthmap)
        {
            return JsonNet(Business.RoleAuthMap.GetAuthTree(roleauthmap.RoleID));
        }
        [AuthorizedFilter("角色权限/编辑权限/访问")]
        public JsonResult AddAuth_(Common.RoleAuthMap roleauthmap)
        {
            roleauthmap.AuthList = Common.JsonHelper.Disable<List<string>>(Request["AuthList"]);
            return JsonNet(Business.RoleAuthMap.AddAuth(roleauthmap));
        }

    }
}



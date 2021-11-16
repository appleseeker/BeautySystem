using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class ProjectsController :Code.MVCExt.BaseController
    {
        public  JsonResult StoresList_Source()
        {
            return JsonNet(DataBase.EnumList.StoresList());
        }
        public  JsonResult ProjectStatus_Source()
        {
            return JsonNet(DataBase.EnumList.ProjectStatus());
        }
        [AuthorizedFilter("产品/添加产品/访问")]
        public  PartialViewResult Add()
        {
            return PartialView(new Common.Projects() {ID=Guid.NewGuid(),Status="上架"});
        }
        [AuthorizedFilter("产品/添加产品/访问")]
        public  JsonResult Add_(Common.Projects projects)
        {
            return JsonNet(Business.Projects.Add(projects));
        }
        [AuthorizedFilter("产品/编辑产品信息/访问")]
        public  PartialViewResult Edit()
        {
            return PartialView(Business.Projects.GetOne(Guid.Parse(Request["ID"])));
        }
        [AuthorizedFilter("产品/编辑产品信息/访问")]
        public  JsonResult Edit_(Common.Projects projects)
        {
            return JsonNet(Business.Projects.Edit(projects));
        }
        [AuthorizedFilter("产品/产品管理/访问")]
        public  PartialViewResult Search()
        {
            return PartialView();
        }
        [AuthorizedFilter("产品/产品管理/访问")]
        public  JsonResult Search_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.Projects_SearchQuery>(new Common.Projects_SearchQuery());
            var data=Business.Projects.Search(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.TotalCount });
        }
        [AuthorizedFilter("产品/产品管理/删除")]
        public  JsonResult Search_Delete(Common.Projects_DeleteAction model)
        {
            return JsonNet(Business.Projects.Delete(model));
        }
        [AuthorizedFilter("产品/产品管理/上架")]
        public  JsonResult Search_Up(Common.Projects_UpAction model)
        {
            return JsonNet(Business.Projects.Up(model));
        }
        [AuthorizedFilter("产品/产品管理/下架")]
        public  JsonResult Search_Down(Common.Projects_DownAction model)
        {
            return JsonNet(Business.Projects.Down(model));
        }

    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class CompanysController :Code.MVCExt.BaseController
    {
        public  JsonResult Companys_CompanyName_Ver_AjaxVerify(Common.Companys model)
        {
            model.CompanyName=Request.Form["CompanyName"];
            return JsonNet(DataBase.AjaxVerify.Companys_CompanyName_Ver(model));
        }
        [AuthorizedFilter("公司/添加公司/访问")]
        public  PartialViewResult Add()
        {
            return PartialView(new Common.Companys() {ID=Guid.NewGuid()});
        }
        [AuthorizedFilter("公司/添加公司/访问")]
        public  JsonResult Add_(Common.Companys companys)
        {
            return JsonNet(Business.Companys.Add(companys));
        }
        [AuthorizedFilter("公司/编辑公司信息/访问")]
        public  PartialViewResult Edit()
        {
            return PartialView(Business.Companys.GetOne(Guid.Parse(Request["ID"])));
        }
        [AuthorizedFilter("公司/编辑公司信息/访问")]
        public  JsonResult Edit_(Common.Companys companys)
        {
            return JsonNet(Business.Companys.Edit(companys));
        }
        [AuthorizedFilter("公司/公司管理/访问")]
        public  PartialViewResult Search()
        {
            return PartialView();
        }
        [AuthorizedFilter("公司/公司管理/访问")]
        public  JsonResult Search_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.Companys_SearchQuery>(new Common.Companys_SearchQuery());
            var data=Business.Companys.Search(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.TotalCount });
        }
        [AuthorizedFilter("公司/公司管理/删除")]
        public  JsonResult Search_Delete(Common.Companys_DeleteAction model)
        {
            return JsonNet(Business.Companys.Delete(model));
        }

    }
}



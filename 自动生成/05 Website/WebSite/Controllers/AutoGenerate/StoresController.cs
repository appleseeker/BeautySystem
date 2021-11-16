using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class StoresController :Code.MVCExt.BaseController
    {
        public  JsonResult CompanyList_Source()
        {
            return JsonNet(DataBase.EnumList.CompanyList());
        }
        public  JsonResult Companys_StoreName_Ver_AjaxVerify(Common.Stores model)
        {
            model.StoreName=Request.Form["StoreName"];
            return JsonNet(DataBase.AjaxVerify.Companys_StoreName_Ver(model));
        }
        [AuthorizedFilter("门店/添加门店/访问")]
        public  PartialViewResult Add()
        {
            return PartialView(new Common.Stores() {ID=Guid.NewGuid()});
        }
        [AuthorizedFilter("门店/添加门店/访问")]
        public  JsonResult Add_(Common.Stores stores)
        {
            return JsonNet(Business.Stores.Add(stores));
        }
        [AuthorizedFilter("门店/编辑门店信息/访问")]
        public  PartialViewResult Edit()
        {
            return PartialView(Business.Stores.GetOne(Guid.Parse(Request["ID"])));
        }
        [AuthorizedFilter("门店/编辑门店信息/访问")]
        public  JsonResult Edit_(Common.Stores stores)
        {
            return JsonNet(Business.Stores.Edit(stores));
        }
        [AuthorizedFilter("门店/门店管理/访问")]
        public  PartialViewResult Search()
        {
            return PartialView();
        }
        [AuthorizedFilter("门店/门店管理/访问")]
        public  JsonResult Search_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.Stores_SearchQuery>(new Common.Stores_SearchQuery());
            var data=Business.Stores.Search(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.TotalCount });
        }
        [AuthorizedFilter("门店/门店管理/删除")]
        public  JsonResult Search_Delete(Common.Stores_DeleteAction model)
        {
            return JsonNet(Business.Stores.Delete(model));
        }

    }
}



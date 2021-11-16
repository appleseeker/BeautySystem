using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Code.MVCExt;

namespace WebSite.Controllers
{
    public partial class MembersController :Code.MVCExt.BaseController
    {
        public  JsonResult Members_NickName_Ver_AjaxVerify(Common.Members model)
        {
            model.NickName=Request.Form["NickName"];
            return JsonNet(DataBase.AjaxVerify.Members_NickName_Ver(model));
        }
        public  JsonResult SexList_Source()
        {
            return JsonNet(DataBase.EnumList.SexList());
        }
        [AuthorizedFilter("会员/添加会员/访问")]
        public  PartialViewResult Add()
        {
            return PartialView(new Common.Members() {ID=Guid.NewGuid()});
        }
        [AuthorizedFilter("会员/添加会员/访问")]
        public  JsonResult Add_(Common.Members members)
        {
            return JsonNet(Business.Members.Add(members));
        }
        [AuthorizedFilter("会员/编辑会员信息/访问")]
        public  PartialViewResult Edit()
        {
            return PartialView(Business.Members.GetOne(Guid.Parse(Request["ID"])));
        }
        [AuthorizedFilter("会员/编辑会员信息/访问")]
        public  JsonResult Edit_(Common.Members members)
        {
            return JsonNet(Business.Members.Edit(members));
        }
        [AuthorizedFilter("会员/会员管理/访问")]
        public  PartialViewResult Search()
        {
            return PartialView();
        }
        [AuthorizedFilter("会员/会员管理/访问")]
        public  JsonResult Search_(Common.Easyui.EasyuiParam pager)
        {
            var model=base.GetRequestData<Common.Members_SearchQuery>(new Common.Members_SearchQuery());
            var data=Business.Members.Search(pager,model);
            return JsonNet(new Common.Easyui.EasyuiResult() { rows = data, total = data.TotalCount });
        }
        [AuthorizedFilter("会员/会员管理/删除")]
        public  JsonResult Search_Delete(Common.Members_DeleteAction model)
        {
            return JsonNet(Business.Members.Delete(model));
        }

    }
}



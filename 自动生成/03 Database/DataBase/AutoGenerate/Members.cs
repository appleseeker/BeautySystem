using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Common;

namespace DataBase
{
    public partial class Members 
    {
        public static int Add(Common.Members members)
        {
            if((members.Phone+"").Length<11 || (members.Phone+"").Length>11) throw new Common.ViewException("手机号长度必须为11位");
            if((members.NickName+"").Length<2 || (members.NickName+"").Length>20) throw new Common.ViewException("昵称长度必须在2到20之间");
            //if(!AjaxVerify.Members_NickName_Ver(members)) throw new Common.ViewException("昵称已经被使用，请换一个");
            if (DataBase.EnumList.SexList().All(f => f.Value != Convert.ToString(members.Sex))) { throw new Common.ViewException("‘性别’值无效，请通过下拉选择有效值。"); }
            return DbHelper.Execute("insert into [Members](ID,Phone,NickName,Sex,Picture,OpenID,CompanyID,RealName,Address,Points,VIPLevel,CreateTime) values(@ID,@Phone,@NickName,@Sex,@Picture,@OpenID,@CompanyID,@RealName,@Address,@Points,@VIPLevel,@CreateTime)", members);
        }
        public static int Edit(Common.Members members)
        {
            if((members.Phone+"").Length<11 || (members.Phone+"").Length>11) throw new Common.ViewException("手机号长度必须为11位");
            if((members.NickName+"").Length<2 || (members.NickName+"").Length>20) throw new Common.ViewException("昵称长度必须在2到20之间");
            //if(!AjaxVerify.Members_NickName_Ver(members)) throw new Common.ViewException("昵称已经被使用，请换一个");
            if (DataBase.EnumList.SexList().All(f => f.Value != Convert.ToString(members.Sex))) { throw new Common.ViewException("‘性别’值无效，请通过下拉选择有效值。"); }
            return DbHelper.Execute("update [Members] set ID=@ID,Phone=@Phone,NickName=@NickName,Sex=@Sex,Picture=@Picture where ID=@ID",members);
        }
        public static Common.Members GetOne(Guid ID)
        {
            return DbHelper.Query<Common.Members>("select top 1 * from [Members] where ID=@ID",new {ID}).First();
        }
        public static Common.Members GetOneByOpenID(string openID)
        {
            return DbHelper.Query<Common.Members>("select top 1 * from [Members] where OpenID=@openID", new { openID }).FirstOrDefault();
        }
        public static int Delete(Members_DeleteAction action)
        {
            return DbHelper.Execute("Delete Members where ID=@ID",action);
        }
        public static PagedList<Common.Members> Search(Common.Easyui.EasyuiParam pager,Common.Members_SearchQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.Members().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(!string.IsNullOrEmpty(query.Phone)) sqlwhere+=" and [Members].Phone = @Phone";
            if(!string.IsNullOrEmpty(query.NickName)) sqlwhere+=" and [Members].NickName like @NickName";
            query.NickName=$"%{query.NickName}%";
            var count=DbHelper.ExecuteScalar<int>($"select count(*) from [Members]  { sqlwhere} ",query);
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.Members>($"select top {pager.rows.ToString()} [Members].ID,[Members].Phone,[Members].NickName,[Members].Sex,[Members].Picture from [Members]  {sqlwhere} and [Members].ID not in (select top {((pager.page-1)*pager.rows).ToString()} ID from [Members] {sqlwhere} {order}) {order}",query).ToPagedList(count);
        }

    }
}



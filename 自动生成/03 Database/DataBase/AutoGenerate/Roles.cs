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
    public partial class Roles 
    {
        public static int Delete(Roles_DeleteAction action)
        {
            if(action.ID==null) throw new Common.ViewException("'ID'的值不能为空");
            return DbHelper.Execute("Delete Roles where ID=@ID",action);
        }
        public static List<Common.Roles> RoleList(Common.Easyui.EasyuiParam pager,Common.Roles_RoleListQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.Roles().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(!string.IsNullOrEmpty(query.Name)) sqlwhere+=" and [Roles].Name like @Name";
            query.Name=$"%{query.Name}%";
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.Roles>($"select [Roles].ID,[Roles].Name from [Roles]  {sqlwhere} {order}",query).ToList();
        }
        public static int RoleAdd(Common.Roles roles)
        {
            if((roles.Name+"").Length<2 || (roles.Name+"").Length>20) throw new Common.ViewException("'角色名称'的长度必须在2到20之间");
            if(!AjaxVerify.RoleOnly(roles)) throw new Common.ViewException("'角色名称'的值验证不通过");
            return DbHelper.Execute("insert into [Roles](ID,Name) values(@ID,@Name)",roles);
        }

    }
}



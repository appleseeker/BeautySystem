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
    public partial class UsersRolesMap 
    {
        public static int Delete(UsersRolesMap_DeleteAction action)
        {
            if(action.ID==null) throw new Common.ViewException("'ID'的值不能为空");
            return DbHelper.Execute("Delete UsersRolesMap where ID=@ID",action);
        }
        public static List<Common.UsersRolesMap> UsersRolesMapList(Common.Easyui.EasyuiParam pager,Common.UsersRolesMap_UsersRolesMapListQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.UsersRolesMap().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(query.UserID.HasValue) sqlwhere+=" and UserID = @UserID";
            if(!string.IsNullOrEmpty(query.RoleName)) sqlwhere+=" and [UsersRolesMap].RoleName like @RoleName";
            query.RoleName=$"%{query.RoleName}%";
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.UsersRolesMap>($"select [UsersRolesMap].ID,[UsersRolesMap].UserID,[UsersRolesMap].RoleName from [UsersRolesMap]  {sqlwhere} {order}",query).ToList();
        }
        public static int AddRole(Common.UsersRolesMap usersrolesmap)
        {
            if (DataBase.EnumList.RoleList().All(f => f.Value != Convert.ToString(usersrolesmap.RoleName))) { throw new Common.ViewException("‘账户角色’值无效，请通过下拉选择有效值。"); }
            if(!AjaxVerify.UserRolesMapOnly(usersrolesmap)) throw new Common.ViewException("'账户角色'的值验证不通过");
            return DbHelper.Execute("insert into [UsersRolesMap](ID,UserID,RoleName) values(@ID,@UserID,@RoleName)",usersrolesmap);
        }

    }
}



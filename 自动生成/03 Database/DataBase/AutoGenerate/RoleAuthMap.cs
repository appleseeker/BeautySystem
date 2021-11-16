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
    public partial class RoleAuthMap 
    {
        public static int Delete(RoleAuthMap_DeleteAction action)
        {
            if(action.ID==null) throw new Common.ViewException("'ID'的值不能为空");
            return DbHelper.Execute("Delete RoleAuthMap where ID=@ID",action);
        }
        public static List<Common.RoleAuthMap> RoleAuthMapList(Common.Easyui.EasyuiParam pager,Common.RoleAuthMap_RoleAuthMapListQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            if(query.RoleID.HasValue) sqlwhere+=" and RoleID = @RoleID";
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.RoleAuthMap>($"select ID,RoleID,Auth from [RoleAuthMap] {sqlwhere} {order}",query).ToList();
        }
        /// <summary>
        ///获取用户树
        /// </summary>
        public static List<Common.RoleAuthMap> RoleAuthList(List<string> names)
        {
            return DbHelper.Query<Common.RoleAuthMap>($"select Auth from [RoleAuthMap] where RoleID in(select ID from Roles where Name in @Names)", new { Names= names }).ToList();
        }
        /// <summary>
        ///获取用户树
        /// </summary>
        public static List<Common.Easyui.TreeList> GetAuthTree(Guid? roleID)
        {
            var autlist = RoleAuthMapList(new Common.Easyui.EasyuiParam(), new Common.RoleAuthMap_RoleAuthMapListQuery() { RoleID = roleID }).Select(f=>f.Auth).ToList();
            return Common.AuthListHelper.GetTreeLists(autlist);
        }

        public static int AddAuth(Common.RoleAuthMap roleauthmap)
        {
            DbHelper.Tran((c, t) => {
                c.Execute("delete RoleAuthMap where RoleID=@RoleID", new { RoleID = roleauthmap.RoleID }, t);
                c.Execute("insert into [RoleAuthMap](ID,RoleID,Auth) values(@ID,@RoleID,@Auth)", roleauthmap.AuthList.Select(f => new Common.RoleAuthMap() { ID = Guid.NewGuid(), RoleID = roleauthmap.RoleID, Auth = f }).ToList(), t);
            });
            return 1;
        }


    }
}



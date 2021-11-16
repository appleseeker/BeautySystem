using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Common;

namespace Business
{
    public partial class RoleAuthMap 
    {
        public static int Delete(RoleAuthMap_DeleteAction action)
        {
            return DataBase.RoleAuthMap.Delete(action);
        }
        public static List<Common.RoleAuthMap> RoleAuthMapList(Common.Easyui.EasyuiParam pager,Common.RoleAuthMap_RoleAuthMapListQuery query)
        {
            return DataBase.RoleAuthMap.RoleAuthMapList(pager,query);
        }
        public static int AddAuth(Common.RoleAuthMap roleauthmap)
        {
            return DataBase.RoleAuthMap.AddAuth(roleauthmap);
        }
        public static List<Common.Easyui.TreeList> GetAuthTree(Guid? roleID)
        {
            return DataBase.RoleAuthMap.GetAuthTree(roleID);
        }

    }
}



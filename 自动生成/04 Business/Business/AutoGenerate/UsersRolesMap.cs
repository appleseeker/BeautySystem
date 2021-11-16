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
    public partial class UsersRolesMap 
    {
        public static int Delete(UsersRolesMap_DeleteAction action)
        {
            return DataBase.UsersRolesMap.Delete(action);
        }
        public static List<Common.UsersRolesMap> UsersRolesMapList(Common.Easyui.EasyuiParam pager,Common.UsersRolesMap_UsersRolesMapListQuery query)
        {
            return DataBase.UsersRolesMap.UsersRolesMapList(pager,query);
        }
        public static int AddRole(Common.UsersRolesMap usersrolesmap)
        {
            return DataBase.UsersRolesMap.AddRole(usersrolesmap);
        }

    }
}



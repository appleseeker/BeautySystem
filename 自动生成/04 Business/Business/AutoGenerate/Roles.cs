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
    public partial class Roles 
    {
        public static int Delete(Roles_DeleteAction action)
        {
            return DataBase.Roles.Delete(action);
        }
        public static List<Common.Roles> RoleList(Common.Easyui.EasyuiParam pager,Common.Roles_RoleListQuery query)
        {
            return DataBase.Roles.RoleList(pager,query);
        }
        public static int RoleAdd(Common.Roles roles)
        {
            return DataBase.Roles.RoleAdd(roles);
        }

    }
}



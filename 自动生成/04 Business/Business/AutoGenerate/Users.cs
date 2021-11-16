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
    public partial class Users 
    {
        public static int Add(Common.Users users)
        {
            return DataBase.Users.Add(users);
        }
        public static int Edit(Common.Users users)
        {
            return DataBase.Users.Edit(users);
        }
        public static Common.Users GetOne(Guid ID)
        {
            return DataBase.Users.GetOne(ID);
        }
        public static int Delete(Users_DeleteAction action)
        {
            return DataBase.Users.Delete(action);
        }
        public static int Disable(Users_DisableAction action)
        {
            return DataBase.Users.Disable(action);
        }
        public static int Enable(Users_EnableAction action)
        {
            return DataBase.Users.Enable(action);
        }
        public static PagedList<Common.Users> Search(Common.Easyui.EasyuiParam pager,Common.Users_SearchQuery query)
        {
            return DataBase.Users.Search(pager,query);
        }
        /// <summary>
        ///获取用户权限
        /// </summary>
        public static Common.Users GetOneAuth(string loginName)
        {
            return DataBase.Users.GetOneAuth(loginName);
        }

    }
}



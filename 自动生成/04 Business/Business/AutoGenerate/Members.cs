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
    public partial class Members 
    {
        public static int Add(Common.Members members)
        {
            return DataBase.Members.Add(members);
        }
        public static int Edit(Common.Members members)
        {
            return DataBase.Members.Edit(members);
        }
        public static Common.Members GetOne(Guid ID)
        {
            return DataBase.Members.GetOne(ID);
        }
        public static int Delete(Members_DeleteAction action)
        {
            return DataBase.Members.Delete(action);
        }
        public static PagedList<Common.Members> Search(Common.Easyui.EasyuiParam pager,Common.Members_SearchQuery query)
        {
            return DataBase.Members.Search(pager,query);
        }
        public static Common.Members GetOneByOpenID(string openID)
        {
            return DataBase.Members.GetOneByOpenID(openID);
        }

    }
}



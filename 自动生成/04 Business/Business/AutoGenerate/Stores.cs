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
    public partial class Stores 
    {
        public static int Add(Common.Stores stores)
        {
            return DataBase.Stores.Add(stores);
        }
        public static int Edit(Common.Stores stores)
        {
            return DataBase.Stores.Edit(stores);
        }
        public static Common.Stores GetOne(Guid ID)
        {
            return DataBase.Stores.GetOne(ID);
        }
        public static int Delete(Stores_DeleteAction action)
        {
            return DataBase.Stores.Delete(action);
        }
        public static PagedList<Common.Stores> Search(Common.Easyui.EasyuiParam pager,Common.Stores_SearchQuery query)
        {
            return DataBase.Stores.Search(pager,query);
        }

        public static List<Common.Stores> GetAll(Guid companyid)
        {
            return DataBase.Stores.GetAll(companyid);
        }

    }
}



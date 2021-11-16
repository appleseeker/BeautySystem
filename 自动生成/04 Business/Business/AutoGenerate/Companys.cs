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
    public partial class Companys 
    {
        public static int Add(Common.Companys companys)
        {
            return DataBase.Companys.Add(companys);
        }
        public static int Edit(Common.Companys companys)
        {
            return DataBase.Companys.Edit(companys);
        }
        public static Common.Companys GetOne(Guid ID)
        {
            return DataBase.Companys.GetOne(ID);
        }
        public static int Delete(Companys_DeleteAction action)
        {
            return DataBase.Companys.Delete(action);
        }
        public static PagedList<Common.Companys> Search(Common.Easyui.EasyuiParam pager,Common.Companys_SearchQuery query)
        {
            return DataBase.Companys.Search(pager,query);
        }

        public static Common.Companys GetCompanysBySiteDomain(string siteDomain)
        {
            return DataBase.Companys.GetCompanysBySiteDomain(siteDomain);
        }

    }
}



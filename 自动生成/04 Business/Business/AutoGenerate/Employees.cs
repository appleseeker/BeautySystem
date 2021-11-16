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
    public partial class Employees 
    {
        public static int Add(Common.Employees employees)
        {
            return DataBase.Employees.Add(employees);
        }
        public static int Edit(Common.Employees employees)
        {
            return DataBase.Employees.Edit(employees);
        }
        public static Common.Employees GetOne(Guid ID)
        {
            return DataBase.Employees.GetOne(ID);
        }
        public static int Delete(Employees_DeleteAction action)
        {
            return DataBase.Employees.Delete(action);
        }
        public static PagedList<Common.Employees> Search(Common.Easyui.EasyuiParam pager,Common.Employees_SearchQuery query)
        {
            return DataBase.Employees.Search(pager,query);
        }
        public static Common.Employees GetOneAuth(string companyCode, string name, string pwd)
        {
            return DataBase.Employees.GetOneAuth(companyCode, name, pwd);
        }

        public static List<Common.Employees> GetAllbyStoreID(string StoreName)
        {
            return DataBase.Employees.GetAllbyStoreID(StoreName);
        }
    }
}



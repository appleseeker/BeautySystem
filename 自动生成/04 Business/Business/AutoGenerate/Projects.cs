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
    public partial class Projects 
    {
        public static int Add(Common.Projects projects)
        {
            return DataBase.Projects.Add(projects);
        }
        public static int Edit(Common.Projects projects)
        {
            return DataBase.Projects.Edit(projects);
        }
        public static Common.Projects GetOne(Guid ID)
        {
            return DataBase.Projects.GetOne(ID);
        }
        public static int Delete(Projects_DeleteAction action)
        {
            return DataBase.Projects.Delete(action);
        }
        public static int Up(Projects_UpAction action)
        {
            return DataBase.Projects.Up(action);
        }
        public static int Down(Projects_DownAction action)
        {
            return DataBase.Projects.Down(action);
        }
        public static PagedList<Common.Projects> Search(Common.Easyui.EasyuiParam pager,Common.Projects_SearchQuery query)
        {
            return DataBase.Projects.Search(pager,query);
        }

        public static List<Common.Projects> GetAll(string storeName)
        {
            return DataBase.Projects.GetAll(storeName);
        }
    }
}



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
    public partial class Images
    {
        public static List<Common.Images> GetImagesByStoresID(Guid StoresID)
        {
            return DataBase.Images.GetImagesByStoresID(StoresID);
        }
    }
}



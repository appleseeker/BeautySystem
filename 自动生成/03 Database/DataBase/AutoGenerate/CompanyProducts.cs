using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Common;

namespace DataBase
{
    public partial class CompanyProducts
    {
       
        public static Common.CompanyProducts GetOne(Guid ID)
        {
            return DbHelper.Query<Common.CompanyProducts>("select top 1 * from [CompanyProducts] where ID=@ID", new {ID}).First();
        }

        public static List<Common.CompanyProducts> GetAll(Guid companyID)
        {
            return DbHelper.Query<Common.CompanyProducts>($"select * from [CompanyProducts] where CompanyID=@CompanyID", new { CompanyID = companyID }).ToList();
        }

    }
}



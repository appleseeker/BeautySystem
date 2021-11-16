using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CompanyProducts
    {
        public static Common.CompanyProducts GetOne(Guid ID)
        {
            return DataBase.CompanyProducts.GetOne(ID);
        }

        public static List<Common.CompanyProducts> GetAll(Guid companyID)
        {
            return DataBase.CompanyProducts.GetAll(companyID);
        }
    }
}

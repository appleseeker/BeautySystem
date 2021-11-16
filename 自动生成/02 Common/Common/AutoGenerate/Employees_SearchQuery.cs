using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class Employees_SearchQuery 
    {
        public string EmpplyeeName { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? StoreID { get; set; }

    }
}



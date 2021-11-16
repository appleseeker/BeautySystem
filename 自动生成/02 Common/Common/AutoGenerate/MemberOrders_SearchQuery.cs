using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class MemberOrders_SearchQuery 
    {
        public Guid? StoreID { get; set; }
        public Guid? MemberID { get; set; }
        public Guid? EmployeeID { get; set; }
        public Guid? ProjectID { get; set; }
        public string Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? CreateTime2 { get; set; }

    }
}



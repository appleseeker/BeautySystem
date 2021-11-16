using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class MemberOrders 
    {
        public Guid? ID { get; set; }
        public Guid? StoreID { get; set; }
        public string StoreName { get; set; }
        public Guid? MemberID { get; set; }
        public string NickName { get; set; }
        public Guid? EmployeeID { get; set; }
        public string EmpplyeeName { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public DateTime? OrderTime { get; set; }
        public string Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Source { get; set; }
        public string Remark { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public int PeopleCount { get; set; }
        public int NeedTime { get; set; }
        public decimal Price { get; set; }

    }
}



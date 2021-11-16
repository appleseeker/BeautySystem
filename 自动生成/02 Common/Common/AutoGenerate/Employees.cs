using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class Employees
    {
        public Guid? ID { get; set; }
        public Guid? CompanyID { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public Guid? StoreID { get; set; }
        public string StoreName { get; set; }
        public string EmpplyeeName { get; set; }
        public string Sex { get; set; }
        public string Picture { get; set; }
        public DateTime? OnBoardDate { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Position { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }

        public string Photo { get; set; }
    }
}



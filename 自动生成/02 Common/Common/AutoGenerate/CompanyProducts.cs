using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class CompanyProducts
    {
        public Guid? ID { get; set; }
        public Guid? CompanyID { get; set; }
        public string ProductName { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public decimal CostPrice { get; set; }
        public int Count { get; set; }
    }
}

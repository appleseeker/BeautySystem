using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class Projects 
    {
        public Guid? ID { get; set; }
        public Guid? StoreID { get; set; }
        public string StoreName { get; set; }
        public string ProjectName { get; set; }
        public string Picture { get; set; }
        public string Status { get; set; }
        public int? Price { get; set; }
        public int? Count { get; set; }
        public int? DiscountPrice { get; set; }
        public DateTime? CreateTime { get; set; }

        public string Describe { get; set; }
        public int NeedTime { get; set; }

        public string Preferential { get; set; }
        public string Promote { get; set; }
    }
}



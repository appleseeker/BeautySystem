using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class Projects_SearchQuery 
    {
        public string ProjectName { get; set; }
        public Guid? StoreID { get; set; }
        public string Status { get; set; }
        public int? Price { get; set; }
        public int? Price2 { get; set; }

    }
}



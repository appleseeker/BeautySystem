using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class Stores 
    {
        public Guid? ID { get; set; }
        public Guid? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string StoreName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public int OpenTime { get; set; }
        public int CloseTime { get; set; }
        public string Banner { get; set; }

    }
}



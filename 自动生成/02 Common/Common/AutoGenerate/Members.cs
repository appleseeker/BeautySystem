using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class Members 
    {
        public Guid? ID { get; set; }
        public string Phone { get; set; }
        public string NickName { get; set; }
        public string Sex { get; set; }
        public string Picture { get; set; }
        public DateTime? CreateTime { get; set; }
        public string OpenID { get; set; }
        public Guid? CompanyID { get; set; }
        public string RealName { get; set; }
        public string Address { get; set; }
        public int? Points { get; set; }
        public string VIPLevel { get; set; }
    }
}



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class Companys 
    {
        public Guid? ID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string ContractName { get; set; }
        public string ContractPhone { get; set; }

        public string sAppID { get; set; }
        public string sToken { get; set; }
        public string sEncodingAESKey { get; set; }
        public string secret { get; set; }
        public string SiteDomain { get; set; }

        public string Describe { get; set; }
        public string Banner { get; set; }

    }
}



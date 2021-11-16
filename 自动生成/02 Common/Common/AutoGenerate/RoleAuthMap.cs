using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class RoleAuthMap 
    {
        public Guid? ID { get; set; }
        public Guid? RoleID { get; set; }
        public string Auth { get; set; }
        public List<string> AuthList { get; set; }

    }
}



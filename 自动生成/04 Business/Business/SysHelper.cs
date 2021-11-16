using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase;

namespace Business
{
    public class SysHelper
    {
        public static void AddLog(string title,string body)
        {
            SyslogAccess.Add(Guid.NewGuid(),title, body);
            return;
        }

        public static void AddLog(Guid id, string title, string body)
        {
            SyslogAccess.Add(id, title, body);
            return;
        }
    }
}

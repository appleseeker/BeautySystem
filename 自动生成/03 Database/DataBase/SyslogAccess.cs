using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataBase
{
    public class SyslogAccess
    {

        public static int Add(Guid id, string title, string body)
        {
            return DbHelper.Execute("insert into [Syslog](ID,Title,Content,CreateTime) values(@ID,@Title,@Content,@CreateTime)", new { ID = id, Title = title, Content = body, CreateTime = DateTime.Now });
        }
    }
}
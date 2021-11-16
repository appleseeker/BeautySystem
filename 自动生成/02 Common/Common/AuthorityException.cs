using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 权限异常
    /// </summary>
    public class AuthorityException:Exception
    {
        public AuthorityException(string mess) : base(mess) { }
    }
}

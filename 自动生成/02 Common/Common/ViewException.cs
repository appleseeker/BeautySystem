using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 用户向用户显示的错误信息
    /// </summary>
    public class ViewException:Exception
    {
        public ViewException(string mess)
            : base(mess)
        {

        }
    }
}

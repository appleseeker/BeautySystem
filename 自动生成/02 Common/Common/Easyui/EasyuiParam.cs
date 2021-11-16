using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Easyui
{
    public class EasyuiParam
    {
        public int page { get; set; }
        public int rows { get; set; }
        public string sort { get; set; }
        public string order { get; set; }
        public bool IsOrder
        {
            get
            {
                if(!string.IsNullOrEmpty(order))
                    return order.Equals("asc") ? true : false;
                else
                    return false;
            }
        }
    }
}

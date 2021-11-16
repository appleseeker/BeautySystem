using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Easyui
{
    public class TreeList
    {
        public TreeList()
        {
            this.children = new List<TreeList>();
        }

        public int id { get; set; }
        public string text { get; set; }
        public bool @checked { get; set; }
        public object attributes { get; set; }
        public List<TreeList> children { get; set; }
    }
}
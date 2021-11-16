using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class AuthListHelper
    {
        public static List<Common.Easyui.TreeList> GetTreeLists(List<string> checkedlist)
        {
            var output = new List<Common.Easyui.TreeList>();
            var list = AuthList.Auth();
            list.GroupBy(f => f.Split('/')[0]).ToList().ForEach(f => {
                var tree = new Common.Easyui.TreeList();
                output.Add(tree);
                tree.text = "【组】"+f.Key;
                tree.attributes = "";
                tree.@checked = false;
                tree.children = new List<Easyui.TreeList>();
                f.GroupBy(g => g.Split('/')[1]).ToList().ForEach(g => {
                    var one = new Common.Easyui.TreeList();
                    tree.children.Add(one);
                    one.text = "【页面】"+g.Key;
                    one.attributes = "";
                    one.@checked = false;
                    one.children = new List<Easyui.TreeList>();
                    g.ToList().ForEach(e => {
                        one.children.Add(new Easyui.TreeList() { text ="【操作】"+e.Split('/')[2], attributes = e, @checked = checkedlist.Any(t=>t==e) });
                    });
                });
            });
            return output;
        }

    }
}



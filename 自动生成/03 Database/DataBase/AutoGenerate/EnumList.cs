using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public partial class EnumList 
    {
        public static List<KeyValuePair<string,string>> EnableDisable()
        {
            var list=new List<KeyValuePair<string,string>>();
            list.Add(new KeyValuePair<string,string>("启用","启用"));
            list.Add(new KeyValuePair<string,string>("禁用","禁用"));
            return list;
        }
        public static List<KeyValuePair<string,string>> CompanyList()
        {
            var list=new List<KeyValuePair<string,string>>();
            var tmp = DbHelper.Query<dynamic>("select CompanyName,ID from Companys", null).ToList();
            foreach (var item in tmp)
            {
            var one = (IDictionary<string, object>)item;
            var keys = one.Keys.ToList();
            if (keys.Count >= 2) list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[1]].ToString()));
            else list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[0]].ToString()));
            }
            return list;
        }
        public static List<KeyValuePair<string,string>> StoresList()
        {
            var list=new List<KeyValuePair<string,string>>();
            var tmp = DbHelper.Query<dynamic>("select StoreName,ID from Stores", null).ToList();
            foreach (var item in tmp)
            {
            var one = (IDictionary<string, object>)item;
            var keys = one.Keys.ToList();
            if (keys.Count >= 2) list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[1]].ToString()));
            else list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[0]].ToString()));
            }
            return list;
        }
        public static List<KeyValuePair<string,string>> SexList()
        {
            var list=new List<KeyValuePair<string,string>>();
            list.Add(new KeyValuePair<string,string>("",""));
            list.Add(new KeyValuePair<string,string>("男","男"));
            list.Add(new KeyValuePair<string,string>("女","女"));
            return list;
        }
        public static List<KeyValuePair<string,string>> RoleList()
        {
            var list=new List<KeyValuePair<string,string>>();
            var tmp = DbHelper.Query<dynamic>("select Name from Roles", null).ToList();
            foreach (var item in tmp)
            {
            var one = (IDictionary<string, object>)item;
            var keys = one.Keys.ToList();
            if (keys.Count >= 2) list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[1]].ToString()));
            else list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[0]].ToString()));
            }
            return list;
        }
        public static List<KeyValuePair<string,string>> ProjectStatus()
        {
            var list=new List<KeyValuePair<string,string>>();
            list.Add(new KeyValuePair<string,string>("",""));
            list.Add(new KeyValuePair<string,string>("上架","上架"));
            list.Add(new KeyValuePair<string,string>("下架","下架"));
            return list;
        }
        public static List<KeyValuePair<string,string>> MemberList()
        {
            var list=new List<KeyValuePair<string,string>>();
            var tmp = DbHelper.Query<dynamic>("select NickName,ID from Members", null).ToList();
            foreach (var item in tmp)
            {
            var one = (IDictionary<string, object>)item;
            var keys = one.Keys.ToList();
            if (keys.Count >= 2) list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[1]].ToString()));
            else list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[0]].ToString()));
            }
            return list;
        }
        public static List<KeyValuePair<string,string>> EmployeeList()
        {
            var list=new List<KeyValuePair<string,string>>();
            var tmp = DbHelper.Query<dynamic>("select EmpplyeeName,ID from Employees", null).ToList();
            foreach (var item in tmp)
            {
            var one = (IDictionary<string, object>)item;
            var keys = one.Keys.ToList();
            if (keys.Count >= 2) list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[1]].ToString()));
            else list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[0]].ToString()));
            }
            return list;
        }
        public static List<KeyValuePair<string,string>> ProjectList()
        {
            var list=new List<KeyValuePair<string,string>>();
            var tmp = DbHelper.Query<dynamic>("select ProjectName,ID from Projects", null).ToList();
            foreach (var item in tmp)
            {
            var one = (IDictionary<string, object>)item;
            var keys = one.Keys.ToList();
            if (keys.Count >= 2) list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[1]].ToString()));
            else list.Add(new KeyValuePair<string, string>(one[keys[0]].ToString(), one[keys[0]].ToString()));
            }
            return list;
        }
        public static List<KeyValuePair<string,string>> MemberOrdersStatus()
        {
            var list=new List<KeyValuePair<string,string>>();
            list.Add(new KeyValuePair<string,string>("",""));
            list.Add(new KeyValuePair<string,string>("未开始", "未开始"));
            list.Add(new KeyValuePair<string,string>("完成","完成"));
            list.Add(new KeyValuePair<string,string>("取消","取消"));
            return list;
        }

    }
}



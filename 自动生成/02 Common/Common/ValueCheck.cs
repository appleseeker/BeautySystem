using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// 数据检查
    /// </summary>
    public class ValueCheck
    {
        /// <summary>
        /// 检查用户输入的数据是在a-z A-Z 0-9
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Checkstr(string str)
        {
            string x = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (var item in str)
            {
                if (!x.Contains(item)) return false;
            }
            return true;
        }
        public static bool Pwd(string str)
        {
            string x = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string y = "1234567890";
            bool a=false,b=false;
            foreach (var item in str)
            {
                if (!x.Contains(item) && !y.Contains(item)) { throw new ViewException("密码必须为字母和数字组合"); };
            }
            foreach (var item in str)
            {
                if (x.Contains(item) ) {a=true; };
                if (y.Contains(item) ) {b=true; };
            }
            return a &&b ;
        }
        public static bool CheckPhone(string str)
        {
            var m = true;
            string x = "1234567890";
            foreach (var item in str)
            {
                if (!x.Contains(item)) m= false;
            }
            if (m && str.Length==11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckCard(string str)
        {
            var m = true;
            string x = "1234567890";
            foreach (var item in str)
            {
                if (!x.Contains(item)) m = false;
            }
            return m;
        }
        public static bool CheckNumber(string str)
        {
            var m = true;
            string x = "1234567890";
            foreach (var item in str)
            {
                if (!x.Contains(item)) m = false;
            }
            return m;
        }

        public static bool CheckEmail(string str)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static  bool IsChinese(string checkedStr)
        {
            checkedStr = checkedStr.Replace("·", "").Replace(".","");
            if (Regex.IsMatch(checkedStr+"", @"^[\u4e00-\u9fa5]+$"))
                return true;
            else
                return false;
        }

    }
}

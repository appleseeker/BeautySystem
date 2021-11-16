using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public static class StringUtils
    {
        public static string HtmlRegexFilter(string html)
        {
            return html.Substring(html.IndexOf(">") + 1, html.LastIndexOf("<") - html.IndexOf(">") - 1);
        }

        ///<summary>
        /// 移除前缀字符串
        ///</summary>
        ///<param name="val">原字符串</param>
        ///<param name="str">前缀字符串</param>
        ///<returns></returns>
        public static string TrimStartStr(string val, string str)
        {
            string strRegex = @"^(" + str + ")";
            return Regex.Replace(val, strRegex, "");
        }

        ///<summary>
        /// 移除后缀字符串
        ///</summary>
        ///<param name="val">原字符串</param>
        ///<param name="str">后缀字符串</param>
        ///<returns></returns>
        public static string TrimEndStr(string val, string str)
        {
            string strRegex = @"(" + str + ")" + "$";
            return Regex.Replace(val, strRegex, "");
        }

        ///<summary>
        /// 截前后字符串
        ///</summary>
        ///<param name="val">原字符串</param>
        ///<param name="str">要截掉的字符串</param>
        ///<param name="bAllStr">是否贪婪,
        ///如果为true则对整个字符串中匹配的进行截取
        ///如果为false则只截取前缀或者后缀</param>
        ///<returns></returns>
        public static string TrimStartEndStr(string val, string str, bool bAllStr)
        {
            return Regex.Replace(val, @"(^(" + str + ")" + (bAllStr ? "*" : "") + "|(" + str + ")" + (bAllStr ? "*" : "") + "$)", "");
        }

        /// <summary>
        /// 截前后字符
        /// </summary>
        /// <param name="val">原字符串</param>
        /// <param name="c">要截取的字符</param>
        /// <returns></returns>
        public static string TrimStartEndChar(string val, char c)
        {
            return val.TrimStart(c).TrimEnd(c);
        }
}
}

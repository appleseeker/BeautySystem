using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 移除字符串中空格，并忽略大小写进行比对
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="target">目标比较字符串</param>
        /// <returns></returns>
        public static bool FullEquals(this string str, string target)
        {
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(target))
            {
                string nstr = str.Replace(" ", "");
                string ntarget = target.Replace(" ", "");
                return nstr.Equals(ntarget, StringComparison.OrdinalIgnoreCase);
            }
            else
                return false;
        }

        /// <summary>
        /// 强制转换成整形，失败返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int TryParseInt(this string str)
        {
            int val = 0;
            int.TryParse(str, out val);
            return val;
        }

        public static int? ToInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return default(int);
            else
            {
                int val = 0;
                int.TryParse(str, out val);
                return val;
            }
        }

        public static double TryParseDouble(this string str)
        {
            double val = 0.0;
            double.TryParse(str, out val);
            return val;
        }
        public static decimal TryParseDecimal(this string str)
        {
            decimal val = (decimal)0.0;
            decimal.TryParse(str, out val);
            return val;
        }
        public static bool TryParseBool(this string str)
        {
            bool val = true;
            bool.TryParse(str, out val);
            return val;
        }

        public static List<string> ToSpiltList(this String str, string spilt)
        {
            string[] list = str.Split(spilt.ToArray(), StringSplitOptions.RemoveEmptyEntries);
            return list.ToList();
        }

        public static string TranslatePLPStatus(this string status)
        {
            string result = string.Empty;
            switch (status.ToLower())
            {
                case "submit":
                    result = "Submit";
                    break;
                case "resubmit":
                    result = "ReSubmit";
                    break;
                case "adminaskformore":
                case "crosscheckaskformore":
                case "approveraskformore":
                    result = "AskForMore";
                    break;
                case "crosschecking":
                case "crosschecked":
                case "approving":
                case "officiallaunch":
                    result = "Approve";
                    break;
                case "crosscheckrejected":
                case "approverrejected":
                    result = "Reject";
                    break;
                case "prelaunch":
                    result = "Prelaunch";
                    break;
                case "publish":
                    result = "Publish";
                    break;
                case "terminate":
                    result = "Terminate";
                    break;
                default:
                    break;
            }
            return result;
        }

        public static string TranslatePWPStatus(this string status)
        {
            string result = string.Empty;
            switch (status.ToLower())
            {
                case "submit":
                    result = "Submit";
                    break;
                case "resubmit":
                    result = "ReSubmit";
                    break;
                case "adminaskformore":
                case "manageraskformore":
                case "revieweraskformore":
                    result = "AskForMore";
                    break;
                case "checking":
                case "checked":
                case "reviewing":
                case "reviewed":
                case "crosschecking":
                case "crosschecked":
                case "approving":
                case "approved":
                    result = "Approve";
                    break;
                case "managerrejected":
                case "crosscheckrejected":
                case "approverrejected":
                    result = "Reject";
                    break;
                case "withdraw":
                    result = "Publish";
                    break;
                case "terminate":
                    result = "Terminate";
                    break;
                default:
                    break;
            }

            return result;
        }

        public static string FilterSingleQuote(string value)
        {
            return value.Replace("'", "");
        }

        public static string HGRQRate(string strong, string ratio)
        {
            if (strong == "H")
                return "+" + ratio;
            else
                return "-" + ratio;
        }

        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <param   name="NoHTML">包括HTML的源码   </param>
        ///   <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = Htmlstring.Trim();

            return Htmlstring;
        }

        public static string FilterClubName(string value)
        {
            value = NoHTML(value);
            //value = (value.IndexOf("(") > -1) ? value.Substring(0, value.IndexOf("(")) : value;
            //value = (value.IndexOf("[") > -1) ? value.Substring(0, value.IndexOf("[")) : value;
            value = value.Replace('(', '[');
            value = value.Replace(')', ']');

            return value;
        }

        public static string FilterBankCard(string card)
        {
            if (!string.IsNullOrEmpty(card))
            {
                int st = card.Length / 4;
                int et = (card.Length / 4) * 3;

                return string.Format("{0}********{1}", card.Substring(0, st), card.Substring(et));
            }
            return card;
        }

        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        /// <summary>
        /// 截取字符串,根据长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubString(string str, int length)
        {
            return str.Length > length ? (str.Substring(0, length) + "...") : str;
        }

        /// <summary>
        /// 转换成可为空的日期
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeWithNull(this string str)
        {
            DateTime output;
            if (DateTime.TryParse(str, out output))
                return output;
            else
                return null;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Hash(this string str, bool half = false, bool gbk = false)
        {
            byte[] b;
            if (gbk == false)
                b = Encoding.UTF8.GetBytes(str);
            else
                b = Encoding.GetEncoding("GBK").GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            if (half)
            {
                ret = ret.Substring(8, 16);
            }
            return ret;
        }

        /// <summary>
        /// 获取大写的MD5签名结果
        /// </summary>
        /// <param name="encypStr"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string GetMD5(this string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用GB2312编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception ex)
            {
                inputBye = Encoding.GetEncoding("GB2312").GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();
            return retStr;
        }

        /// <summary>
        /// 把集合转换成SqlIn这种格式
        /// </summary>
        /// <typeparam name="T">集合类型，只能是字符串集合或整形集合</typeparam>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static string SqlIn<TSource>(this IEnumerable<TSource> list)
        {
            if (list == null || list.Count() == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                if (typeof(TSource) == typeof(int))
                    sb.AppendFormat("{0},", item);
                else
                    sb.AppendFormat("'{0}',", item);
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 过滤 Sql 语句字符串中的注入脚本
        /// </summary>
        /// <param name="source">传入的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string SqlFilter(this string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                //单引号替换成两个单引号
                source = source.Replace("'", "''");

                //半角封号替换为全角封号，防止多语句执行
                source = source.Replace(";", "；");

                //半角括号替换为全角括号
                source = source.Replace("(", "（");
                source = source.Replace(")", "）");

                return source;
            }
            else
                return source;
        }

        public static string LastName(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            else
                return str.Split("\\".ToArray(), StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
        }

        public static string SaveAsExtension(this HttpPostedFileBase fileUpload, string filename, bool renewFilename, out string msg, params string[] allowext)
        {
            string newpath = string.Empty;

            if (allowext.Count() > 0 && !allowext.Any(r => r.Equals(System.IO.Path.GetExtension(fileUpload.FileName), StringComparison.OrdinalIgnoreCase)))
                msg = "上传的文件无效！";
            else
            {
                if (renewFilename)
                {
                    string ext = System.IO.Path.GetExtension(fileUpload.FileName);
                    newpath = System.IO.Path.Combine(filename, Guid.NewGuid() + ext);
                }
                else
                    newpath = System.IO.Path.Combine(filename, Path.GetFileName(fileUpload.FileName));

                fileUpload.SaveAs(newpath);
                msg = "";
            }
            return newpath;
        }

        /// <summary>
        /// 添加ToDataTable扩展方法(包含属性）
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="list">参数</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(T);
            DataTable dt = new DataTable();
            dt.TableName = type.Name;
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                pList.Add(p);
                Type t = p.PropertyType;
                if (t.Name == "Nullable`1")
                {
                    PropertyInfo[] ps = t.GetProperties();
                    if (ps.Length > 0)
                    {
                        t = ps[ps.Length - 1].PropertyType;
                    }
                }
                dt.Columns.Add(p.Name, t);
            });
            foreach (var item in list)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null) ?? DBNull.Value);
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static string ToNumChinese(this string money)
        {
            string s = double.Parse(money).ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string
             d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(d, ".", 
                (Match m) =>
                {
                   return "负圆空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万億兆京垓秭穰"[m.Value[0] - '-'].ToString();
                });
        }
    }
}

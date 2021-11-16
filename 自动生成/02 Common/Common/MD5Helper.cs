using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace Common
{
    public class MD5Helper
    {
        public static string MD5(string pwd)
        {
            if (!string.IsNullOrEmpty(pwd))
            {
                //md5加密
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    return BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(pwd))).Replace("-", "").ToLower();
                }

            }
            else {
                return pwd;
            }

        }

        /// <summary>
        /// web加密的验证码
        /// </summary>
        /// <param name="checkCode"></param>
        /// <returns></returns>
        public static string WebCodeMd5(string checkCode)
        {
           return MD5(MD5("BoCai" + checkCode + "PangChen"));
        }
    }
}

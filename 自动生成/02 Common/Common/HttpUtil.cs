using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpUtil
    {
        private int _timeout = 100000;

        /// <summary>
        /// 请求与响应的超时时间
        /// </summary>
        public int Timeout
        {
            get { return this._timeout; }
            set { this._timeout = value; }
        }

        static public HttpUtil Create()
        {
            return new HttpUtil();
        }


        public string DoPost(string url, string postdata, Encoding encoding)
        {
            HttpWebRequest req = GetWebRequest(url, "POST");
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";   //原"application/x-www-form-urlencoded;charset=utf-8"; 不支持参数里有html代码改成text/plain
            //req.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/msword, */*";
            byte[] postData = Encoding.UTF8.GetBytes(postdata);
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return GetResponseAsString(rsp, encoding);
        }
        public string DoPost2(string url, string postdata, Encoding encoding)
        {
            HttpWebRequest req = GetWebRequest(url, "POST");
            req.ContentType = "application/json;charset=utf-8";   //原"application/x-www-form-urlencoded;charset=utf-8"; 不支持参数里有html代码改成text/plain
            //req.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/msword, */*";
            byte[] postData = Encoding.UTF8.GetBytes(postdata);
            System.IO.Stream reqStream = req.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();

            HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
            return GetResponseAsString(rsp, encoding);
        }

        public static string DoPost3(string url, string postData, Encoding encoding)
        {
            //发送请求的数据
            byte[] byte1 = encoding.GetBytes(postData);
            WebRequest myHttpWebRequest = WebRequest.Create(url);

            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, encoding);
            lcHtml = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();
            return lcHtml;
        }

        public static string DoPost4(string url, string postData, string contentType, Encoding encoding)
        {
            //发送请求的数据
            byte[] byte1 = encoding.GetBytes(postData);
            WebRequest myHttpWebRequest = WebRequest.Create(url);

            myHttpWebRequest.Method = "POST";
            myHttpWebRequest.ContentType = contentType;
            myHttpWebRequest.ContentLength = byte1.Length;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //声明一个HttpWebRequest请求  
            myHttpWebRequest.Timeout = 90000;
            //设置连接超时时间  
            myHttpWebRequest.Headers.Set("Pragma", "no-cache");
            //发送成功后接收返回的信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, encoding);
            lcHtml = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();
            return lcHtml;
        }

        public static string DoGet(string url, string requestParam, Encoding encoding)
        {
            if ("" != requestParam)
            { requestParam = requestParam.IndexOf('?') > -1 ? (requestParam) : ("?" + requestParam); }
            WebRequest myHttpWebRequest = WebRequest.Create(url + requestParam);
            myHttpWebRequest.Method = "GET";

            //发送成功后接收返回的信息
            WebResponse response = myHttpWebRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, encoding);
            string lcHtml = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();

            return lcHtml;
        }

        public static string DoGet1(string url)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(url).Result;
        }

        public static string UploadFile(string url, Stream postedStream, IDictionary<string, string> paramList, Encoding encoding)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Upload Web URL Is Empty.");

            //发送请求的数据
            WebRequest webRequest = WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postedStream.Length;

            //发送的参数
            foreach (var item in paramList)
            {
                webRequest.Headers.Add(item.Key, System.Web.HttpUtility.UrlEncode(item.Value));
            }

            using (Stream newStream = webRequest.GetRequestStream())
            {
                postedStream.CopyTo(newStream);
            }

            //发送成功后接收返回的信息
            string lcHtml = string.Empty;
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream, encoding);
                lcHtml = streamReader.ReadToEnd();
                streamReader.Close();
                webRequest.GetResponse().Close();
            }
            catch (WebException ex)
            {
                HttpWebResponse responsex = ex.Response as HttpWebResponse;
                if (responsex != null)
                {
                    throw new WebException(response.StatusDescription, ex);
                }
                throw ex;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return lcHtml;
        }

        private HttpWebRequest GetWebRequest(string url, string method)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ServicePoint.Expect100Continue = false;
            req.Accept = "*/*";
            req.Headers.Add("Accept-Language", "zh-cn");
            req.Headers.Add("Accept-Encoding", "gzip, deflate");
            req.Method = method;
            req.KeepAlive = true;
            //req.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.1.4322)";
            req.Timeout = this._timeout;

            return req;
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        private string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            System.IO.Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }
        }


        /// <summary> 
        /// 从文件读取 Stream 
        /// </summary> 
        public static Stream FileToStream(string fileName)
        {
            // 打开文件 
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[] 
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream 
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}

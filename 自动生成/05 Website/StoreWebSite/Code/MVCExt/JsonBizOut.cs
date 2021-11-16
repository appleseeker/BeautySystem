using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreWebSite.Code.MVCExt
{
    [Serializable]
    public class JsonBizOut<T> where T:class
    {
        public string Mess { get; set; }
        public bool IsSucceed { get; set;}
        public T Data { get; set; }
    }
}
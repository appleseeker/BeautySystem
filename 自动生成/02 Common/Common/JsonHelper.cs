using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class JsonHelper
    {
        public static string Sazeable<T>(T obj) where T : class 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static string Sazeable2<T>(T obj) where T : class
        {
            JsonSerializerSettings Settings= Settings = new JsonSerializerSettings();
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            Settings.Converters.Add(timeFormat);
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Settings);
        }
        public static string Sazeable3(Object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T Disable<T>(string str) where T : class
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }
    }
}

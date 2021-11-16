using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace WeiXinWeb.Code
{
    public class CacheHelper
    {
        /// <summary>
        /// 设置缓存(秒)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="timespan">缓存时间(秒)</param>
        public static void Set(string key, object value, int timespan)
        {
            if (value == null) return;
            if (timespan == 0) return;
            MemoryCache.Default.Set(key, value, new DateTimeOffset(DateTime.Now.AddSeconds(timespan)));
        }
        public static T Get<T>(string key,Func<T> func,int timespan)
        {
            if (!MemoryCache.Default.Contains(key))
            {
                var obj=func();
                Set(key, obj, timespan);
                return (T)obj;
            }
            return (T)MemoryCache.Default[key];
        }
        
    }
}
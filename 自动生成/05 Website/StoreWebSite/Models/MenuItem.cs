using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreWebSite.Models
{
    public class MenuItem
    {
        public MenuItem(string text,string url,string authkey)
        {
            Text = text;
            Url = url;
            AuthKey = authkey;
        }
        public string Text { get; set; }
        public string Url { get; set; }
        public string AuthKey { get; set; }
    }
}
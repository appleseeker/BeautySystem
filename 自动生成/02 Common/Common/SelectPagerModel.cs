using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common
{
    /// <summary>
    /// 分页查询模型
    /// </summary>
    public class SelectPagerModel
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 排序列名
        /// </summary>
        public string sidx { get; set; }
        /// <summary>
        /// 排序方式（asc,desc)
        /// </summary>
        public string sord { get; set; }

        /// <summary>
        /// 从http中读取jqgrid查询参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static SelectPagerModel GetModel(HttpRequestBase request)
        {
            SelectPagerModel m = new SelectPagerModel();
            m.page = string.IsNullOrEmpty(request["page"]) ? 1 : Convert.ToInt32(request["page"]);
            m.rows = string.IsNullOrEmpty(request["rows"]) ? 10 : Convert.ToInt32(request["rows"]);
            m.sidx = request["sidx"];
            m.sord = string.IsNullOrEmpty(request["sord"]) ? "asc" : request["sord"];
            return m;
        }

    }
}
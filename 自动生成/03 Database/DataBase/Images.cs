using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Common;

namespace DataBase
{
    public partial class Images
    {
        /// <summary>
        /// 根据域名返回信息公司配置信息
        /// </summary>
        /// <param name="siteDomain"></param>
        /// <returns></returns>
        public static List<Common.Images> GetImagesByStoresID(Guid StoresID)
        {
            return DbHelper.Query<Common.Images>("select * from [Images] where StoresID=@StoresID", new { StoresID }).ToList();
        }

    }
}



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
    public partial class Stores 
    {
        public static int Add(Common.Stores stores)
        {
            if (DataBase.EnumList.CompanyList().All(f => f.Value != Convert.ToString(stores.CompanyID))) { throw new Common.ViewException("‘公司ID’值无效，请通过下拉选择有效值。"); }
            if((stores.StoreName+"").Length<5 || (stores.StoreName+"").Length>200) throw new Common.ViewException("门店名长度必须在5-200之间");
            if(!AjaxVerify.Companys_StoreName_Ver(stores)) throw new Common.ViewException("门店名称已经存在");
            if((stores.Province+"").Length<0 || (stores.Province+"").Length>50) throw new Common.ViewException("省份名长度必须在0-50之间");
            if((stores.City+"").Length<0 || (stores.City+"").Length>50) throw new Common.ViewException("城市名长度必须在0-50之间");
            if((stores.District+"").Length<0 || (stores.District+"").Length>50) throw new Common.ViewException("地区名长度必须在0-50之间");
            if((stores.Address+"").Length<0 || (stores.Address+"").Length>200) throw new Common.ViewException("地址名长度必须在2-200之间");
            return DbHelper.Execute("insert into [Stores](ID,CompanyID,StoreName,Province,City,District,Address,OpenTime,CloseTime,Banner) values(@ID,@CompanyID,@StoreName,@Province,@City,@District,@Address,@OpenTime,@CloseTime,@Banner)", stores);
        }
        public static int Edit(Common.Stores stores)
        {
            if (DataBase.EnumList.CompanyList().All(f => f.Value != Convert.ToString(stores.CompanyID))) { throw new Common.ViewException("‘公司ID’值无效，请通过下拉选择有效值。"); }
            if((stores.StoreName+"").Length<5 || (stores.StoreName+"").Length>200) throw new Common.ViewException("门店名长度必须在5-200之间");
            if(!AjaxVerify.Companys_StoreName_Ver(stores)) throw new Common.ViewException("门店名称已经存在");
            if((stores.Province+"").Length<0 || (stores.Province+"").Length>50) throw new Common.ViewException("省份名长度必须在0-50之间");
            if((stores.City+"").Length<0 || (stores.City+"").Length>50) throw new Common.ViewException("城市名长度必须在0-50之间");
            if((stores.District+"").Length<0 || (stores.District+"").Length>50) throw new Common.ViewException("地区名长度必须在0-50之间");
            if((stores.Address+"").Length<0 || (stores.Address+"").Length>200) throw new Common.ViewException("地址名长度必须在2-200之间");
            return DbHelper.Execute("update [Stores] set ID=@ID,CompanyID=@CompanyID,StoreName=@StoreName,Province=@Province,City=@City,District=@District,Address=@Address,OpenTime=@OpenTime,CloseTime=@CloseTime,Banner=@Banner where ID=@ID", stores);
        }
        public static Common.Stores GetOne(Guid ID)
        {
            return DbHelper.Query<Common.Stores>("select top 1 * from [Stores] where ID=@ID",new {ID}).First();
        }
        public static int Delete(Stores_DeleteAction action)
        {
            return DbHelper.Execute("Delete Stores where ID=@ID",action);
        }
        public static PagedList<Common.Stores> Search(Common.Easyui.EasyuiParam pager,Common.Stores_SearchQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.Stores().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(!string.IsNullOrEmpty(query.StoreName)) sqlwhere+=" and [Stores].StoreName = @StoreName";
            if(query.CompanyID.HasValue) sqlwhere+=" and CompanyID = @CompanyID";
            if(!string.IsNullOrEmpty(query.Province)) sqlwhere+=" and [Stores].Province = @Province";
            if(!string.IsNullOrEmpty(query.City)) sqlwhere+=" and [Stores].City = @City";
            if(!string.IsNullOrEmpty(query.District)) sqlwhere+=" and [Stores].District = @District";
            if(!string.IsNullOrEmpty(query.Address)) sqlwhere+=" and [Stores].Address like @Address";
            query.Address=$"%{query.Address}%";
            var count=DbHelper.ExecuteScalar<int>($"select count(*) from [Stores] left join Companys on Companys.ID=Stores.CompanyID { sqlwhere} ",query);
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.Stores>($"select top {pager.rows.ToString()} [Stores].ID,[Companys].CompanyName,[Stores].StoreName,[Stores].Province,[Stores].City,[Stores].District,[Stores].Address from [Stores] left join Companys on Companys.ID=Stores.CompanyID {sqlwhere} and [Stores].ID not in (select top {((pager.page-1)*pager.rows).ToString()} ID from [Stores] {sqlwhere} {order}) {order}",query).ToPagedList(count);
        }

        public static List<Common.Stores> GetAll(Guid companyid)
        {
           return DbHelper.Query<Common.Stores>($"select * from Stores where CompanyID=@CompanyID", new { CompanyID = companyid }).ToList();
        }

    }
}



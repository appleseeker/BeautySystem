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
    public partial class Companys 
    {
        public static int Add(Common.Companys companys)
        {
            if((companys.CompanyName+"").Length<2 || (companys.CompanyName+"").Length>300) throw new Common.ViewException("公司名长度必须在2-300之间");
            if(!AjaxVerify.Companys_CompanyName_Ver(companys)) throw new Common.ViewException("公司名称已经存在");
            if ((companys.CompanyCode + "").Length < 2 || (companys.CompanyCode + "").Length > 50) throw new Common.ViewException("公司编号必须在2-50之间");
            if ((companys.Province+"").Length<0 || (companys.Province+"").Length>50) throw new Common.ViewException("省份名长度必须在0-50之间");
            if((companys.City+"").Length<0 || (companys.City+"").Length>50) throw new Common.ViewException("城市名长度必须在0-50之间");
            if((companys.District+"").Length<0 || (companys.District+"").Length>50) throw new Common.ViewException("地区名长度必须在0-50之间");
            if((companys.Address+"").Length<0 || (companys.Address+"").Length>200) throw new Common.ViewException("地址名长度必须在2-200之间");
            if((companys.ContractName+"").Length<0 || (companys.ContractName+"").Length>300) throw new Common.ViewException("地址名长度必须在2-300之间");
            if((companys.ContractPhone+"").Length<0 || (companys.ContractPhone+"").Length>20) throw new Common.ViewException("地址名长度必须在2-20之间");
            return DbHelper.Execute("insert into [Companys](ID,CompanyName,Province,City,District,Address,ContractName,ContractPhone,sAppID,sToken,sEncodingAESKey,secret,SiteDomain) values(@ID,@CompanyName,@Province,@City,@District,@Address,@ContractName,@ContractPhone,@sAppID,@sToken,@sEncodingAESKey,@secret,@SiteDomain)", companys);
        }
        public static int Edit(Common.Companys companys)
        {
            if((companys.CompanyName+"").Length<2 || (companys.CompanyName+"").Length>300) throw new Common.ViewException("公司名长度必须在2-300之间");
            if(!AjaxVerify.Companys_CompanyName_Ver(companys)) throw new Common.ViewException("公司名称已经存在");
            if ((companys.CompanyCode + "").Length < 2 || (companys.CompanyCode + "").Length > 50) throw new Common.ViewException("公司编号必须在2-50之间");
            if ((companys.Province+"").Length<0 || (companys.Province+"").Length>50) throw new Common.ViewException("省份名长度必须在0-50之间");
            if((companys.City+"").Length<0 || (companys.City+"").Length>50) throw new Common.ViewException("城市名长度必须在0-50之间");
            if((companys.District+"").Length<0 || (companys.District+"").Length>50) throw new Common.ViewException("地区名长度必须在0-50之间");
            if((companys.Address+"").Length<0 || (companys.Address+"").Length>200) throw new Common.ViewException("地址名长度必须在2-200之间");
            if((companys.ContractName+"").Length<0 || (companys.ContractName+"").Length>300) throw new Common.ViewException("地址名长度必须在2-300之间");
            if((companys.ContractPhone+"").Length<0 || (companys.ContractPhone+"").Length>20) throw new Common.ViewException("地址名长度必须在2-20之间");
            return DbHelper.Execute("update [Companys] set ID=@ID,CompanyName=@CompanyName,Province=@Province,City=@City,District=@District,Address=@Address,ContractName=@ContractName,ContractPhone=@ContractPhone,sAppID=@sAppID,sToken=@sToken,sEncodingAESKey=@sEncodingAESKey,secret=@secret,SiteDomain=@SiteDomain where ID=@ID", companys);
        }
        public static Common.Companys GetOne(Guid ID)
        {
            return DbHelper.Query<Common.Companys>("select top 1 * from [Companys] where ID=@ID",new {ID}).First();
        }
        public static int Delete(Companys_DeleteAction action)
        {
            return DbHelper.Execute("Delete Members where ID=@ID",action);
        }
        public static PagedList<Common.Companys> Search(Common.Easyui.EasyuiParam pager,Common.Companys_SearchQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.Companys().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(!string.IsNullOrEmpty(query.CompanyName))
                sqlwhere +=" and [Companys].CompanyName = @CompanyName";
            if(!string.IsNullOrEmpty(query.Province))
                sqlwhere +=" and [Companys].Province = @Province";
            if(!string.IsNullOrEmpty(query.City))
                sqlwhere +=" and [Companys].City = @City";
            if(!string.IsNullOrEmpty(query.District))
                sqlwhere +=" and [Companys].District = @District";
            if(!string.IsNullOrEmpty(query.Address))
                sqlwhere +=" and [Companys].Address like @Address";
            query.Address=$"%{query.Address}%";
            var count=DbHelper.ExecuteScalar<int>($"select count(*) from [Companys]  { sqlwhere} ",query);
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.Companys>($"select top {pager.rows.ToString()} * from [Companys]  {sqlwhere} and [Companys].ID not in (select top {((pager.page-1)*pager.rows).ToString()} ID from [Companys] {sqlwhere} {order}) {order}",query).ToPagedList(count);
        }
        /// <summary>
        /// 根据域名返回信息公司配置信息
        /// </summary>
        /// <param name="siteDomain"></param>
        /// <returns></returns>
        public static Common.Companys GetCompanysBySiteDomain(string siteDomain)
        {
            return DbHelper.Query<Common.Companys>("select top 1 * from [Companys] where SiteDomain=@SiteDomain", new { siteDomain }).First();
        }

    }
}



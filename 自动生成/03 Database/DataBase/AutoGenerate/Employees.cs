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
    public partial class Employees 
    {
        public static int Add(Common.Employees employees)
        {
            if (DataBase.EnumList.CompanyList().All(f => f.Value != Convert.ToString(employees.CompanyID))) { throw new Common.ViewException("‘公司’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.StoresList().All(f => f.Value != Convert.ToString(employees.StoreID))) { throw new Common.ViewException("‘门店’值无效，请通过下拉选择有效值。"); }
            if((employees.EmpplyeeName+"").Length<0 || (employees.EmpplyeeName+"").Length>100) throw new Common.ViewException("员工名长度必须在0-100之间");
            if (DataBase.EnumList.SexList().All(f => f.Value != Convert.ToString(employees.Sex))) { throw new Common.ViewException("‘性别’值无效，请通过下拉选择有效值。"); }
            return DbHelper.Execute("insert into [Employees](ID,CompanyID,StoreID,EmpplyeeName,Sex,Picture,OnBoardDate,Position) values(@ID,@CompanyID,@StoreID,@EmpplyeeName,@Sex,@Picture,@OnBoardDate,@Position)",employees);
        }
        public static int Edit(Common.Employees employees)
        {
            if (DataBase.EnumList.CompanyList().All(f => f.Value != Convert.ToString(employees.CompanyID))) { throw new Common.ViewException("‘公司’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.StoresList().All(f => f.Value != Convert.ToString(employees.StoreID))) { throw new Common.ViewException("‘门店’值无效，请通过下拉选择有效值。"); }
            if((employees.EmpplyeeName+"").Length<0 || (employees.EmpplyeeName+"").Length>100) throw new Common.ViewException("员工名长度必须在0-100之间");
            if (DataBase.EnumList.SexList().All(f => f.Value != Convert.ToString(employees.Sex))) { throw new Common.ViewException("‘性别’值无效，请通过下拉选择有效值。"); }
            return DbHelper.Execute("update [Employees] set ID=@ID,CompanyID=@CompanyID,StoreID=@StoreID,EmpplyeeName=@EmpplyeeName,Sex=@Sex,Picture=@Picture,OnBoardDate=@OnBoardDate,Position=@Position where ID=@ID",employees);
        }
        public static Common.Employees GetOne(Guid ID)
        {
            return DbHelper.Query<Common.Employees>("select top 1 * from [Employees] where ID=@ID",new {ID}).First();
        }
        public static int Delete(Employees_DeleteAction action)
        {
            return DbHelper.Execute("Delete Employees where ID=@ID",action);
        }
        public static PagedList<Common.Employees> Search(Common.Easyui.EasyuiParam pager,Common.Employees_SearchQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.Employees().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(!string.IsNullOrEmpty(query.EmpplyeeName)) sqlwhere+=" and [Employees].EmpplyeeName = @EmpplyeeName";
            if(query.CompanyID.HasValue) sqlwhere+=" and CompanyID = @CompanyID";
            if(query.StoreID.HasValue) sqlwhere+=" and StoreID = @StoreID";
            var count=DbHelper.ExecuteScalar<int>($"select count(*) from [Employees] left join Companys on Companys.ID=Employees.CompanyID left join Stores on Stores.ID=Employees.StoreID { sqlwhere} ",query);
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.Employees>($"select top {pager.rows.ToString()} [Employees].ID,[Companys].CompanyName,[Stores].StoreName,[Employees].EmpplyeeName,[Employees].Sex,[Employees].Picture,[Employees].OnBoardDate,[Employees].CreateTime,[Employees].Position from [Employees] left join Companys on Companys.ID=Employees.CompanyID left join Stores on Stores.ID=Employees.StoreID {sqlwhere} and [Employees].ID not in (select top {((pager.page-1)*pager.rows).ToString()} ID from [Employees] {sqlwhere} {order}) {order}",query).ToPagedList(count);
        }
        public static Common.Employees GetOneAuth(string companyCode, string name, string pwd)
        {
            var employee = DbHelper.Query<Common.Employees>("select top 1 * from [vEmployee] where Account=@Account and Pwd=@Pwd and CompanyCode=@CompanyCode", new { CompanyCode = companyCode, Account = name, Pwd = pwd }).FirstOrDefault();

            return employee;
        }

        public static List<Common.Employees> GetAllbyStoreID(string StoreName)
        {
            var employee = DbHelper.Query<Common.Employees>("select * from [vEmployee] where Position='美容师' and StoreID in (select top 1 ID from [Stores] where StoreName=@StoreName)", new { StoreName }).ToList();

            return employee;
        }
    }
}



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
    public partial class Projects 
    {
        public static int Add(Common.Projects projects)
        {
            if (DataBase.EnumList.StoresList().All(f => f.Value != Convert.ToString(projects.StoreID))) { throw new Common.ViewException("‘门店’值无效，请通过下拉选择有效值。"); }
            if((projects.ProjectName+"").Length<0 || (projects.ProjectName+"").Length>300) throw new Common.ViewException("'产品名'的长度必须在0到300之间");
            if (DataBase.EnumList.ProjectStatus().All(f => f.Value != Convert.ToString(projects.Status))) { throw new Common.ViewException("‘状态’值无效，请通过下拉选择有效值。"); }
            if(Convert.ToDecimal(projects.Price)>100000 || Convert.ToDecimal(projects.Price)<1) throw new Common.ViewException("'价格'的值必须在1到100000之间");
            if(Convert.ToDecimal(projects.Count)>100000 || Convert.ToDecimal(projects.Count)<0) throw new Common.ViewException("'数量'的值必须在0到100000之间");
            return DbHelper.Execute("insert into [Projects](ID,StoreID,ProjectName,Picture,Status,Price,Count,Describe,NeedTime) values(@ID,@StoreID,@ProjectName,@Picture,@Status,@Price,@Count,@Describe,@NeedTime)", projects);
        }
        public static int Edit(Common.Projects projects)
        {
            if (DataBase.EnumList.StoresList().All(f => f.Value != Convert.ToString(projects.StoreID))) { throw new Common.ViewException("‘门店’值无效，请通过下拉选择有效值。"); }
            if((projects.ProjectName+"").Length<0 || (projects.ProjectName+"").Length>300) throw new Common.ViewException("'产品名'的长度必须在0到300之间");
            if (DataBase.EnumList.ProjectStatus().All(f => f.Value != Convert.ToString(projects.Status))) { throw new Common.ViewException("‘状态’值无效，请通过下拉选择有效值。"); }
            if(Convert.ToDecimal(projects.Price)>100000 || Convert.ToDecimal(projects.Price)<1) throw new Common.ViewException("'价格'的值必须在1到100000之间");
            if(Convert.ToDecimal(projects.Count)>100000 || Convert.ToDecimal(projects.Count)<0) throw new Common.ViewException("'数量'的值必须在0到100000之间");
            return DbHelper.Execute("update [Projects] set ID=@ID,StoreID=@StoreID,ProjectName=@ProjectName,Picture=@Picture,Status=@Status,Price=@Price,Count=@Count,Describe=@Describe,NeedTime=@NeedTime where ID=@ID", projects);
        }
        public static Common.Projects GetOne(Guid ID)
        {
            return DbHelper.Query<Common.Projects>("select top 1 * from [Projects] where ID=@ID",new {ID}).First();
        }
        public static int Delete(Projects_DeleteAction action)
        {
            return DbHelper.Execute("Delete Projects where ID=@ID",action);
        }
        public static int Up(Projects_UpAction action)
        {
            return DbHelper.Execute("Update Projects set [Status]='上架' where ID=@ID",action);
        }
        public static int Down(Projects_DownAction action)
        {
            return DbHelper.Execute("Update Projects set [Status]='下架' where ID=@ID",action);
        }
        public static PagedList<Common.Projects> Search(Common.Easyui.EasyuiParam pager,Common.Projects_SearchQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.Projects().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(!string.IsNullOrEmpty(query.ProjectName)) sqlwhere+=" and [Projects].ProjectName = @ProjectName";
            if(query.StoreID.HasValue) sqlwhere+=" and StoreID = @StoreID";
            if(!string.IsNullOrEmpty(query.Status)) sqlwhere+=" and [Projects].Status = @Status";
            if(query.Price.HasValue) sqlwhere+=" and Price >= @Price";
            if(query.Price2.HasValue) sqlwhere+=" and Price <= @Price2";
            var count=DbHelper.ExecuteScalar<int>($"select count(*) from [Projects] left join Stores on Stores.ID=Projects.StoreID { sqlwhere} ",query);
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.Projects>($"select top {pager.rows.ToString()} [Projects].ID,[Stores].StoreName,[Projects].ProjectName,[Projects].Picture,[Projects].Price,[Projects].Status,[Projects].Count,[Projects].CreateTime,[Projects].Describe,[Projects].NeedTime from [Projects] left join Stores on Stores.ID=Projects.StoreID {sqlwhere} and [Projects].ID not in (select top {((pager.page-1)*pager.rows).ToString()} ID from [Projects] {sqlwhere} {order}) {order}",query).ToPagedList(count);
        }

        public static List<Common.Projects> GetAll(string storeName)
        {
            if (string.IsNullOrEmpty(storeName))
            {//优惠推广
                return DbHelper.Query<Common.Projects>($"select * from [Projects] where Promote='是'", null).ToList();
            }
            return DbHelper.Query<Common.Projects>($"select * from [Projects] where StoreID in(select top 1 ID from [Stores] where StoreName=@StoreName)", new { StoreName = storeName }).ToList();
        }

    }
}



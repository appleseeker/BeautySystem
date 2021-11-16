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
    public partial class MemberOrders 
    {
        public static int Add(Common.MemberOrders memberorders)
        {
            if (DataBase.EnumList.StoresList().All(f => f.Value != Convert.ToString(memberorders.StoreID))) { throw new Common.ViewException("‘门店’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.MemberList().All(f => f.Value != Convert.ToString(memberorders.MemberID))) { throw new Common.ViewException("‘会员’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.EmployeeList().All(f => f.Value != Convert.ToString(memberorders.EmployeeID))) { throw new Common.ViewException("‘员工’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.ProjectList().All(f => f.Value != Convert.ToString(memberorders.ProjectID))) { throw new Common.ViewException("‘项目’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.MemberOrdersStatus().All(f => f.Value != Convert.ToString(memberorders.Status))) { throw new Common.ViewException("‘状态’值无效，请通过下拉选择有效值。"); }
            return DbHelper.Execute("insert into [MemberOrders](ID,StoreID,MemberID,EmployeeID,ProjectID,OrderTime,Status,CreateTime,Source,Remark,Phone,UserName,PeopleCount,NeedTime,Price) values(@ID,@StoreID,@MemberID,@EmployeeID,@ProjectID,@OrderTime,@Status,@CreateTime,@Source,@Remark,@Phone,@UserName,@PeopleCount,@NeedTime,@Price)", memberorders);
        }
        public static int Edit(Common.MemberOrders memberorders)
        {
            if (DataBase.EnumList.StoresList().All(f => f.Value != Convert.ToString(memberorders.StoreID))) { throw new Common.ViewException("‘门店’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.MemberList().All(f => f.Value != Convert.ToString(memberorders.MemberID))) { throw new Common.ViewException("‘会员’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.EmployeeList().All(f => f.Value != Convert.ToString(memberorders.EmployeeID))) { throw new Common.ViewException("‘员工’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.ProjectList().All(f => f.Value != Convert.ToString(memberorders.ProjectID))) { throw new Common.ViewException("‘项目’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.MemberOrdersStatus().All(f => f.Value != Convert.ToString(memberorders.Status))) { throw new Common.ViewException("‘状态’值无效，请通过下拉选择有效值。"); }
            return DbHelper.Execute("update [MemberOrders] set ID=@ID,StoreID=@StoreID,MemberID=@MemberID,EmployeeID=@EmployeeID,ProjectID=@ProjectID,OrderTime=@OrderTime,Status=@Status where ID=@ID",memberorders);
        }
        public static Common.MemberOrders GetOne(Guid ID)
        {
            return DbHelper.Query<Common.MemberOrders>("select top 1 * from [vMemberOrders] where ID=@ID",new {ID}).First();
        }
        public static int Delete(MemberOrders_DeleteAction action)
        {
            return DbHelper.Execute("Delete MemberOrders where ID=@ID",action);
        }
        public static int wancheng(MemberOrders_wanchengAction action)
        {
            return DbHelper.Execute("Update MemberOrders set [Status]='完成' where ID=@ID",action);
        }
        public static int quxiao(MemberOrders_quxiaoAction action)
        {
            return DbHelper.Execute("Update MemberOrders set [Status]='取消' where ID=@ID",action);
        }
        public static PagedList<Common.MemberOrders> Search(Common.Easyui.EasyuiParam pager,Common.MemberOrders_SearchQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.MemberOrders().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(query.StoreID.HasValue) sqlwhere+=" and StoreID = @StoreID";
            if(query.MemberID.HasValue) sqlwhere+=" and MemberID = @MemberID";
            if(query.EmployeeID.HasValue) sqlwhere+=" and EmployeeID = @EmployeeID";
            if(query.ProjectID.HasValue) sqlwhere+=" and ProjectID = @ProjectID";
            if(!string.IsNullOrEmpty(query.Status)) sqlwhere+=" and [MemberOrders].Status = @Status";
            if(query.CreateTime.HasValue) sqlwhere+=" and CreateTime >= @CreateTime";
            if(query.CreateTime2.HasValue) sqlwhere+=" and CreateTime <= @CreateTime2";
            var count=DbHelper.ExecuteScalar<int>($"select count(*) from [MemberOrders] left join Stores on Stores.ID=MemberOrders.StoreID left join Members on Members.ID=MemberOrders.MemberID left join [Users] on Users.ID=MemberOrders.EmployeeID left join Projects on Projects.ID=MemberOrders.ProjectID { sqlwhere} ",query);
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.MemberOrders>($"select top {pager.rows.ToString()} [MemberOrders].ID,[Stores].StoreName,[Members].NickName,[Users].EmpplyeeName,[Projects].ProjectName,[MemberOrders].Status,[MemberOrders].OrderTime,[MemberOrders].CreateTime from [MemberOrders] left join Stores on Stores.ID=MemberOrders.StoreID left join Members on Members.ID=MemberOrders.MemberID left join [Users] on Users.ID=MemberOrders.EmployeeID left join Projects on Projects.ID=MemberOrders.ProjectID {sqlwhere} and [MemberOrders].ID not in (select top {((pager.page-1)*pager.rows).ToString()} ID from [MemberOrders] {sqlwhere} {order}) {order}",query).ToPagedList(count);
        }
        /// <summary>
        /// 获取预约列表
        /// </summary>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        public static List<Common.MemberOrders> GetListByMemberID(Guid MemberID)
        {
            return DbHelper.Query<Common.MemberOrders>("select * from [vMemberOrders] where MemberID=@MemberID", new { MemberID }).ToList();
        }

    }
}



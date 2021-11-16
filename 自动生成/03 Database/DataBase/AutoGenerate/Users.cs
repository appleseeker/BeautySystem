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
    public partial class Users 
    {
        public static int Add(Common.Users users)
        {
            if((users.LoginName+"").Length<3 || (users.LoginName+"").Length>20) throw new Common.ViewException("'登录名'的长度必须在3到20之间");
            if(!AjaxVerify.Users_LoginNameOnly(users)) throw new Common.ViewException("'登录名'的值验证不通过");
            if((users.Pwd+"").Length<3 || (users.Pwd+"").Length>20) throw new Common.ViewException("'登陆密码'的长度必须在3到20之间");
            if (DataBase.EnumList.EnableDisable().All(f => f.Value != Convert.ToString(users.Status))) { throw new Common.ViewException("‘状态’值无效，请通过下拉选择有效值。"); }
            if((users.Status+"").Length<2 || (users.Status+"").Length>2) throw new Common.ViewException("'状态'的长度必须在2到2之间");
            if (DataBase.EnumList.CompanyList().All(f => f.Value != Convert.ToString(users.CompanyID))) { throw new Common.ViewException("‘公司’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.StoresList().All(f => f.Value != Convert.ToString(users.StoreID))) { throw new Common.ViewException("‘门店’值无效，请通过下拉选择有效值。"); }
            if((users.EmpplyeeName+"").Length<0 || (users.EmpplyeeName+"").Length>100) throw new Common.ViewException("员工名长度必须在0-100之间");
            if (DataBase.EnumList.SexList().All(f => f.Value != Convert.ToString(users.Sex))) { throw new Common.ViewException("‘性别’值无效，请通过下拉选择有效值。"); }
            return DbHelper.Execute("insert into [Users](ID,LoginName,Pwd,Status,CompanyID,StoreID,EmpplyeeName,Sex,Picture,OnBoardDate,Position) values(@ID,@LoginName,@Pwd,@Status,@CompanyID,@StoreID,@EmpplyeeName,@Sex,@Picture,@OnBoardDate,@Position)",users);
        }
        public static int Edit(Common.Users users)
        {
            if((users.LoginName+"").Length<3 || (users.LoginName+"").Length>20) throw new Common.ViewException("'登录名'的长度必须在3到20之间");
            if(!AjaxVerify.Users_LoginNameOnly(users)) throw new Common.ViewException("'登录名'的值验证不通过");
            if((users.Pwd+"").Length<3 || (users.Pwd+"").Length>20) throw new Common.ViewException("'登陆密码'的长度必须在3到20之间");
            if (DataBase.EnumList.EnableDisable().All(f => f.Value != Convert.ToString(users.Status))) { throw new Common.ViewException("‘状态’值无效，请通过下拉选择有效值。"); }
            if((users.Status+"").Length<2 || (users.Status+"").Length>2) throw new Common.ViewException("'状态'的长度必须在2到2之间");
            if (DataBase.EnumList.CompanyList().All(f => f.Value != Convert.ToString(users.CompanyID))) { throw new Common.ViewException("‘公司’值无效，请通过下拉选择有效值。"); }
            if (DataBase.EnumList.StoresList().All(f => f.Value != Convert.ToString(users.StoreID))) { throw new Common.ViewException("‘门店’值无效，请通过下拉选择有效值。"); }
            if((users.EmpplyeeName+"").Length<0 || (users.EmpplyeeName+"").Length>100) throw new Common.ViewException("员工名长度必须在0-100之间");
            if (DataBase.EnumList.SexList().All(f => f.Value != Convert.ToString(users.Sex))) { throw new Common.ViewException("‘性别’值无效，请通过下拉选择有效值。"); }
            return DbHelper.Execute("update [Users] set ID=@ID,LoginName=@LoginName,Pwd=@Pwd,Status=@Status,CompanyID=@CompanyID,StoreID=@StoreID,EmpplyeeName=@EmpplyeeName,Sex=@Sex,Picture=@Picture,OnBoardDate=@OnBoardDate,Position=@Position where ID=@ID",users);
        }
        public static Common.Users GetOne(Guid ID)
        {
            return DbHelper.Query<Common.Users>("select top 1 * from [Users] where ID=@ID",new {ID}).First();
        }
        public static int Delete(Users_DeleteAction action)
        {
            return DbHelper.Execute("Delete Users where ID=@ID",action);
        }
        public static int Disable(Users_DisableAction action)
        {
            return DbHelper.Execute("Update Users set Status='禁用' where ID=@ID",action);
        }
        public static int Enable(Users_EnableAction action)
        {
            return DbHelper.Execute("Update Users set Status='启用' where ID=@ID",action);
        }
        public static PagedList<Common.Users> Search(Common.Easyui.EasyuiParam pager,Common.Users_SearchQuery query)
        {
            if(string.IsNullOrEmpty(pager.sort)) pager.sort="ID";
            var sqlwhere=" where 1=1 ";
            var propertiesList = new Common.Users().GetType().GetProperties().Select(f=>f.Name);
            if (propertiesList.All(f => f != pager.sort)) throw new ViewException("排序字段不是有效属性");
            if (pager.order.ToLower() != "asc" && pager.order != "desc") pager.order = "desc";
            if(!string.IsNullOrEmpty(query.LoginName)) sqlwhere+=" and [Users].LoginName = @LoginName";
            if(!string.IsNullOrEmpty(query.Status)) sqlwhere+=" and [Users].Status = @Status";
            if(!string.IsNullOrEmpty(query.EmpplyeeName)) sqlwhere+=" and [Users].EmpplyeeName = @EmpplyeeName";
            if(query.CompanyID.HasValue) sqlwhere+=" and CompanyID = @CompanyID";
            if(query.StoreID.HasValue) sqlwhere+=" and StoreID = @StoreID";
            var count=DbHelper.ExecuteScalar<int>($"select count(*) from [Users] left join Companys on Companys.ID=Users.CompanyID left join Stores on Stores.ID=Users.StoreID { sqlwhere} ",query);
            var order=$"order by {pager.sort} {pager.order}";
            return DbHelper.Query<Common.Users>($"select top {pager.rows.ToString()} [Users].ID,[Users].LoginName,[Users].Status,[Companys].CompanyName,[Stores].StoreName,[Users].EmpplyeeName,[Users].Sex,[Users].Picture,[Users].OnBoardDate,[Users].CreateTime,[Users].Position from [Users] left join Companys on Companys.ID=Users.CompanyID left join Stores on Stores.ID=Users.StoreID {sqlwhere} and [Users].ID not in (select top {((pager.page-1)*pager.rows).ToString()} ID from [Users] {sqlwhere} {order}) {order}",query).ToPagedList(count);
        }
        /// <summary>
        ///获取用户权限
        /// </summary>
        public static Common.Users GetOneAuth(string loginName)
        {
            var user= DbHelper.Query<Common.Users>("select top 1 * from [Users] where LoginName=@LoginName", new { LoginName=loginName }).FirstOrDefault();
            if (user != null)
            {
              //查询角色
              var roles=UsersRolesMap.UsersRolesMapList(new Common.Easyui.EasyuiParam() {  order="asc", page=1, rows=1000}, new Common.UsersRolesMap_UsersRolesMapListQuery() { UserID = user.ID });
              //查询角色权限
              var authlist = RoleAuthMap.RoleAuthList(roles.Select(f => f.RoleName).ToList());
              user.AuthList = authlist;
            }
            return user;
        }

    }
}



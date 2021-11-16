using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public partial class AjaxVerify 
    {
        public static bool Users_LoginNameOnly(Common.Users model)
        {
            var tmpvalue=DbHelper.ExecuteScalar("select count(*) from [Users] where id!=@id and LoginName=@LoginName",model);
            return ((int)tmpvalue==0);
        }
        public static bool RoleOnly(Common.Roles model)
        {
            var tmpvalue=DbHelper.ExecuteScalar("select count(*) from Roles where Name=@Name and ID!=@ID",model);
            return ((int)tmpvalue==0);
        }
        public static bool UserRolesMapOnly(Common.UsersRolesMap model)
        {
            var tmpvalue=DbHelper.ExecuteScalar("select count(*) from UsersRolesMap where ID!=@ID and RoleName=@RoleName and UserID=@UserID",model);
            return ((int)tmpvalue==0);
        }
        public static bool Members_NickName_Ver(Common.Members model)
        {
            var tmpvalue=DbHelper.ExecuteScalar("select count(*) from Members where NickName=@NickName and ID!=@ID",model);
            return ((int)tmpvalue==0);
        }
        public static bool Companys_CompanyName_Ver(Common.Companys model)
        {
            var tmpvalue=DbHelper.ExecuteScalar("select count(*) from Companys where CompanyName=@CompanyName and ID!=@ID",model);
            return ((int)tmpvalue==0);
        }
        public static bool Companys_StoreName_Ver(Common.Stores model)
        {
            var tmpvalue=DbHelper.ExecuteScalar("select count(*) from Stores where StoreName=@StoreName and ID!=@ID",model);
            return ((int)tmpvalue==0);
        }

    }
}



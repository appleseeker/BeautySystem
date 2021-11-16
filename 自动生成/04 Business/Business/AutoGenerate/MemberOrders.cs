using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Common;

namespace Business
{
    public partial class MemberOrders 
    {
        public static int Add(Common.MemberOrders memberorders)
        {
            return DataBase.MemberOrders.Add(memberorders);
        }
        public static int Edit(Common.MemberOrders memberorders)
        {
            return DataBase.MemberOrders.Edit(memberorders);
        }
        public static Common.MemberOrders GetOne(Guid ID)
        {
            return DataBase.MemberOrders.GetOne(ID);
        }
        public static int Delete(MemberOrders_DeleteAction action)
        {
            return DataBase.MemberOrders.Delete(action);
        }
        public static int wancheng(MemberOrders_wanchengAction action)
        {
            return DataBase.MemberOrders.wancheng(action);
        }
        public static int quxiao(MemberOrders_quxiaoAction action)
        {
            return DataBase.MemberOrders.quxiao(action);
        }
        public static PagedList<Common.MemberOrders> Search(Common.Easyui.EasyuiParam pager,Common.MemberOrders_SearchQuery query)
        {
            return DataBase.MemberOrders.Search(pager,query);
        }
        public static List<Common.MemberOrders> GetListByMemberID(Guid MemberID)
        {
            return DataBase.MemberOrders.GetListByMemberID(MemberID);
        }

    }
}



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public partial class AuthList 
    {
        public static List<string> Auth()
        {
            var list=new List<string>();
            list.Add("员工账户/添加员工/访问");
            list.Add("员工账户/编辑员工信息/访问");
            list.Add("员工账户/员工管理/访问");
            list.Add("员工账户/员工管理/删除");
            list.Add("员工账户/员工管理/禁用");
            list.Add("员工账户/员工管理/启用");
            list.Add("角色管理/角色管理/访问");
            list.Add("角色管理/角色管理/删除");
            list.Add("角色管理/新建角色/访问");
            list.Add("角色权限/权限列表/访问");
            list.Add("角色权限/权限列表/删除");
            list.Add("角色权限/编辑权限/访问");
            list.Add("账户角色/账户角色列表/访问");
            list.Add("账户角色/账户角色列表/删除");
            list.Add("账户角色/添加角色/访问");
            list.Add("会员/添加会员/访问");
            list.Add("会员/编辑会员信息/访问");
            list.Add("会员/会员管理/访问");
            list.Add("会员/会员管理/删除");
            list.Add("公司/添加公司/访问");
            list.Add("公司/编辑公司信息/访问");
            list.Add("公司/公司管理/访问");
            list.Add("公司/公司管理/删除");
            list.Add("门店/添加门店/访问");
            list.Add("门店/编辑门店信息/访问");
            list.Add("门店/门店管理/访问");
            list.Add("门店/门店管理/删除");
            list.Add("产品/添加产品/访问");
            list.Add("产品/编辑产品信息/访问");
            list.Add("产品/产品管理/访问");
            list.Add("产品/产品管理/删除");
            list.Add("产品/产品管理/上架");
            list.Add("产品/产品管理/下架");
            list.Add("预约/添加预约/访问");
            list.Add("预约/编辑预约信息/访问");
            list.Add("预约/预约管理/访问");
            list.Add("预约/预约管理/删除");
            list.Add("预约/预约管理/完成");
            list.Add("预约/预约管理/取消");
            return list;
        }

    }
}



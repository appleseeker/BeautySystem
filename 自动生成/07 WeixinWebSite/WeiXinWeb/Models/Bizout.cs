using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXinWeb.Models
{
    public class Bizout<T>
    {
        public int code { get; set; }
        public string errorMsg { get; set; }
        public T data { get; set; }
        public static Bizout<T> Action(Func<T> action)
        {
            try
            {
                var x = action();
                return new Bizout<T>() { code = 1, data = x };
            }
            catch (Common.ViewException vex)
            {
                return new Bizout<T>() { code = -1, errorMsg = vex.Message };
            }catch(Exception ex)
            {
                var id = Guid.NewGuid();
                Business.SysHelper.AddLog(id, "系统异常", ex.ToString());
                return new Bizout<T>() { code = -2, errorMsg = $"业务失败，请联系管理员;(业务ID:{id.ToString()})"};
            }
        }
    }
}
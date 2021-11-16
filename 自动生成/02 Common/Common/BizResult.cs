using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BizResult<T>
    {
        public BizResult() { }

        public BizResult(bool IsSuccess, string ReturnString, T ReturnObject)
        {
            this.IsSuccess = IsSuccess;
            this.ReturnString = ReturnString;
            this.ReturnObject = ReturnObject;
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get;
            set;
        }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnString
        {
            get;
            set;
        }

        public string ErrorCode
        {
            get;
            set;
        }

        /// <summary>
        /// 返回对象
        /// </summary>
        public T ReturnObject
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.BaseClass
{
    public class BaseErr
    {
        /// <summary>
        /// 错误码(如果有错误的时候),0表示没有错误
        /// </summary>
        public int errcode { set; get; }
        /// <summary>
        /// 错误说明(如果有错误的时候)
        /// </summary>
        public string errmsg { set; get; }
    }
}

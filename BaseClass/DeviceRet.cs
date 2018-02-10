using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.BaseClass
{
    /// <summary>
    /// 提示信息（硬件）
    /// </summary>
    public class DeviceRet:BaseErr
    {
        /// <summary>
        /// 成功的提示代码
        /// </summary>
        public int ret { set; get; }
        /// <summary>
        /// 提示的信息
        /// </summary>
        public string ret_info { set; get; }
    }
}

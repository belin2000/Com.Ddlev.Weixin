using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.Pay.Unifiedorder
{
    /// <summary>
    /// 用于确定H5支付接口的参数
    /// </summary>
    public interface IH5_Info
    {
        string type { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.Pay.Sandbox
{
    public class couponRequest : Com.Ddlev.Weixin.High.Pay.couponRequest
    {
        public couponRequest(Config _c):base(_c)
        {
        }
        public new couponResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/sandboxnew/mmpaymkttransfers/send_coupon";
            return send(url);
        }
    }
}

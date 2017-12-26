using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.Pay.Sandbox
{
    public class unifiedorderRequest: Com.Ddlev.Weixin.High.Pay.Unifiedorder.unifiedorderRequest
    {
        public unifiedorderRequest(Config c) : base(c)
        {

        }
        public new Unifiedorder.unifiedorderResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/sandboxnew/pay/unifiedorder";
            return base.send(url);
        }
    }
}

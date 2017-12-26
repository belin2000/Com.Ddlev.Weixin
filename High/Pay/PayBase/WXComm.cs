using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Pay.PayBase
{
    /// <summary>
    /// 微信支付的公用基础接口类
    /// </summary>
    public class WXComm: WXCommBase
    {
        /// <summary>
        /// 微信分配的公众账号ID(必填)
        /// </summary>
        public string appid { set; get; }

    }
    public class WXCommBase: WXCommBa
    {
        /// <summary>
        /// 微信支付分配的商户号(必填)
        /// </summary>
        public string mch_id { set; get; }

    }
    public class WXCommBa
    {
        protected Config c;
        /// <summary>
        /// 随机字符串，不长于32位(必填)
        /// </summary>
        public string nonce_str { set; get; }
        /// <summary>
        /// 签名(必填)
        /// </summary>
        public string sign { set; get; }
    }
}

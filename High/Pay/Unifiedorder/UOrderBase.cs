using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.Pay.Unifiedorder
{
    public class UOrderBase: PayBase.WXComm
    {
        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string device_info { set; get; }
        /// <summary>
        /// 商品描述(必填)
        /// </summary>
        public string body { set; get; }
        /// <summary>
        /// 附加数据，原样返回
        /// </summary>
        public string attach { set; get; }
        /// <summary>
        /// 商户系统内部的订单号,32个字符内、可包含字母,确保在商户系统唯一(必填)
        /// </summary>
        public string out_trade_no { set; get; }
        /// <summary>
        /// 订单总金额，单位为分，不能带小数点(必填)
        /// </summary>
        public int total_fee { set; get; }
        /// <summary>
        /// 订单生成的机器IP(必填)
        /// </summary>
        public string spbill_create_ip { set; get; }
        /// <summary>
        /// 商品标记，该字段不能随便填，不使用请填空
        /// </summary>
        public string goods_tag { set; get; }
    }
}

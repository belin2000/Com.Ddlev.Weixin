using Com.Ddlev.Weixin.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 退款
    /// </summary>
    public class RefundRequest:PayBase.WXComm,IFace.IRequest<RefundResponse>
    {

        /// <summary>
        /// 微信支付分配的子商户号 ， 受理模式下必填(选填)
        /// </summary>
        public string sub_mch_id { set; get; }
        /// <summary>
        /// 微信支付分配的终端设备号 ，与下单一致(选填)
        /// </summary>
        public string device_info { set; get; }
        /// <summary>
        /// 微信订单号
        /// </summary>
        public string transaction_id { set; get; }
        /// <summary>
        /// 商户系统内部的订单号 (transaction_id 和 out_trade_no 必填一个或者都填写)
        /// </summary>
        public string out_trade_no { set; get; }
        /// <summary>
        /// 商户系统内部的退款单号 ， 商户系统内部唯一 ， 同一退款单号多次请求只退一笔
        /// </summary>
        public string out_refund_no { set; get; }
        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int total_fee { set; get; }
        /// <summary>
        /// 退款总金额 ， 单位为分 , 可以做部分退款
        /// </summary>
        public int refund_fee { set; get; }
        /// 操作员帐号 , 默认为商户号
        /// </summary>
        public string op_user_id { set; get; }

        public RefundRequest(Config _c)
        {
            this.c = _c;
        }
        protected RefundResponse send(string url= "https://api.mch.weixin.qq.com/secapi/pay/refund")
        {
            if (string.IsNullOrEmpty(this.sign) || this.sign == "")
            {
                this.sign = BaseClass.BaseMethod.MakeSign(this, c);
            }
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8, "", c.certpath, c.Mchid);
            var rs = new RefundResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rs);
            return rs;
        }
        public RefundResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            return send(url);
        }
    }
    /// <summary>
    /// 退款结果
    /// </summary>
    public class RefundResponse : BaseClass.BusinessLogic,IFace.IResponse
    {
        /// <summary>
        /// 微信订单号
        /// </summary>
        public string transaction_id { set; get; }
        /// <summary>
        /// 商户系统内部的订单号
        /// </summary>
        public string out_trade_no { set; get; }
        /// <summary>
        /// 商户退款单号
        /// </summary>
        public string out_refund_no { set; get; }
        /// <summary>
        /// 微信退款单号
        /// </summary>
        public string refund_id { set; get; }
        /// <summary>
        /// ORIGI NAL — 原路退款，默认 ;BALANCE — 退回到余额
        /// </summary>
        public string refund_channel { set; get; }
        /// <summary>
        /// 退款总金额 , 单位为分 , 可以做部分退款
        /// </summary>
        public int refund_fee { set; get; }
        /// <summary>
        /// 现金券退款金额 大于等于 退款金额 ， 退款金额 - 现金券退款金额为现金
        /// </summary>
        public int coupon_refund_fee { set; get; }
    }

}

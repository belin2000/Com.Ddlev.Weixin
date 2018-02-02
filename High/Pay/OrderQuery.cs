using Com.Ddlev.Weixin.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Pay
{
    public class OrderQueryRequest: PayBase.WXComm, IFace.IRequest<OrderQueryResponse>
    {

        /// <summary>
        /// 微信支付分配的子商户号，受理模式下必填
        /// </summary>
        public string sub_mch_id { set; get; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { set; get; }
        /// <summary>
        /// 商户系统的订单号，与请求一致。
        /// </summary>
        public string out_trade_no { set; get; }

        public OrderQueryRequest(Config _c)
        {
            this.c = _c;
            this.appid = _c.AppID;
            this.mch_id = _c.Mchid;
            this.nonce_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
        protected OrderQueryResponse send(string url)
        {
            if (string.IsNullOrEmpty(this.sign) || this.sign == "")
            {
                this.sign = BaseClass.BaseMethod.MakeSign(this,c);
            }
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8);
            var rs = new OrderQueryResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rs);
            return rs;
        }
        public OrderQueryResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/pay/orderquery";
            return send(url);
        }

        public async Task<OrderQueryResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class OrderQueryResponse : BaseClass.BusinessLogic,IFace.IResponse
    {
        string _trade_state;
        /// <summary>
        /// 交易状态SUCCESS—支付成功 REFUND—转入退款 NOTPAY—未支付 CLOSED—已关闭 REVOKED—已撤销 USERPAYING--用户支付中 NOPAY--未支付(输入密码或确认支付超时) PAYERROR--支付失败(其他原因，如银行返回失败)
        /// </summary>
        public string trade_state
        {
            get { return _trade_state; }
            set { _trade_state = value; }
        }
        string _openid;
        /// <summary>
        /// 用户在商户appid下的唯一标识
        /// </summary>
        public string openid
        {
            get { return _openid; }
            set { _openid = value; }
        }
        string _is_subscribe;
        /// <summary>
        /// 用户是否关注公众账号，Y关注，N-未关注，仅在公众账号类型支付有效
        /// </summary>
        public string is_subscribe
        {
            get { return _is_subscribe; }
            set { _is_subscribe = value; }
        }

        string _trade_type;
        /// <summary>
        /// JSAPI、NATIVE、MICROPAY、APP
        /// </summary>
        public string trade_type
        {
            get { return _trade_type; }
            set { _trade_type = value; }
        }
        string _bank_type;
        /// <summary>
        /// 银行类型，采用字符串类型的银行标识
        /// </summary>
        public string bank_type
        {
            get { return _bank_type; }
            set { _bank_type = value; }
        }

        int _total_fee;
        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public int total_fee
        {
            get { return _total_fee; }
            set { _total_fee = value; }
        }
        int _coupon_fee;
        /// <summary>
        /// 现金券支付金额<=订单总金额，订单总金额-现金券金额为现金支付金额
        /// </summary>
        public int coupon_fee
        {
            get { return _coupon_fee; }
            set { _coupon_fee = value; }
        }
        string _fee_type;
        /// <summary>
        /// 货币类型，符合ISO4217标准的三位字母代码，默认人民币：CNY
        /// </summary>
        public string fee_type
        {
            get { return _fee_type; }
            set { _fee_type = value; }
        }

        string _transaction_id;
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id
        {
            get { return _transaction_id; }
            set { _transaction_id = value; }
        }

        string _out_trade_no;
        /// <summary>
        /// 商户系统的订单号，与请求一致。
        /// </summary>
        public string out_trade_no
        {
            get { return _out_trade_no; }
            set { _out_trade_no = value; }
        }
        string _attach;
        /// <summary>
        /// 商家数据包，原样返回
        /// </summary>
        public string attach
        {
            get { return _attach; }
            set { _attach = value; }
        }
        string _time_end;
        /// <summary>
        /// 支付完成时间，格式为 yyyyMMddhhmmss
        /// </summary>
        public string time_end
        {
            get { return _time_end; }
            set { _time_end = value; }
        }
    }

}

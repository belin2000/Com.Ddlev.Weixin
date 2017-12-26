using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 统一通知接口(支付成功后，微信通知我们的服务器的数据)
    /// </summary>
    public class PayBack : BaseClass.BusinessLogic
    {
        /*以下字段在 return_code 为 SUCCESS 的时候有返回*/
        string _openid;
        /// <summary>
        /// 用户在商户appid下的唯一标识
        /// </summary>
        public string openid
        {
            get { return _openid; }
            set { _openid = value; }
        }
        string _trade_type;
        /// <summary>
        /// 交易类型 JSAPI、NATIVE、MICROPAY、APP
        /// </summary>
        public string trade_type
        {
            get { return _trade_type; }
            set { _trade_type = value; }
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
        /// 现金券支付金额小于等于订单总金额，订单总金额-现金券金额为现金支付金额
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
        /// 支付完成时间，格式为yyyyMMddhhmmss
        /// </summary>
        public string time_end
        {
            get { return _time_end; }
            set { _time_end = value; }
        }

    }
    /// <summary>
    /// 微信支付，微信对我方服务器发送数据，我们回复微信的的处理结果
    /// </summary>
    public class WeiXinPayBack
    {
        string _return_code;
        /// <summary>
        /// SUCCESS/FAIL SUCCESS表示商户接收通知成功并校验成功
        /// </summary>
        public string return_code
        {
            get { return _return_code; }
            set { _return_code = value; }
        }
        string _return_msg;
        /// <summary>
        /// 返回信息，如非空，为错误原因
        /// </summary>
        public string return_msg
        {
            get { return _return_msg; }
            set { _return_msg = value; }
        }
    }
}

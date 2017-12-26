using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 刷卡支付
    /// </summary>
    public class MicroPayRequest : Unifiedorder.UOrderBase, IFace.IRequest<MicroPayResponse>
    {
        /// <summary>
        /// 扫码支付授权码，设备读取用户微信中的条码或者二维码信息（注：用户刷卡条形码规则：18位纯数字，以10、11、12、13、14、15开头）
        /// </summary>
        public string auth_code { set; get; }
        /// <summary>
        /// [商户号使用]微信分配的子商户公众账号ID，如需在支付完成后获取sub_openid则此参数必传。
        /// </summary>
        public string sub_appid { set; get; }
        /// <summary>
        /// [商户号使用]微信支付分配的子商户号，受理模式下必填
        /// </summary>
        public string sub_mch_id { set; get; }

        /// <summary>
        /// 初始化后并设置相关属性
        /// </summary>
        /// <param name="C"></param>
        public MicroPayRequest(Config C)
        {
            this.c = C;
            this.appid = c.AppID;
            this.mch_id = c.Mchid;
            this.sub_appid = c.Sub_Appid;
            this.sub_mch_id = c.SubMchId;
        }
        public MicroPayResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/pay/micropay";
            return send(url);
        }
        protected MicroPayResponse send(string url)
        {
            if (string.IsNullOrEmpty(this.sign) || this.sign == "")
            {
                this.sign = BaseClass.BaseMethod.MakeSign(this, c);
            }
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8);
            MicroPayResponse ucb = new MicroPayResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, ucb);
            return ucb;
        }
    }
    public class MicroPayResponse : BaseClass.BusinessLogic, IFace.IResponse
    {
        /// <summary>
        /// 用户在商户appid 下的唯一标识
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// 用户是否关注公众账号，仅在公众账号类型支付有效，取值范围：Y或N;Y-关注;N-未关注
        /// </summary>
        public string is_subscribe { set; get; }
        /// <summary>
        /// 子商户appid下用户唯一标识，如需返回则请求时需要传sub_appid
        /// </summary>
        public string sub_openid { set; get; }
        /// <summary>
        /// 用户是否关注子公众账号，仅在公众账号类型支付有效，取值范围：Y或N;Y-关注;N-未关注
        /// </summary>
        public string sub_is_subscribe { set; get; }
        /// <summary>
        /// 支付类型为MICROPAY(即扫码支付)
        /// </summary>
        public string trade_type { set; get; }
        /// <summary>
        /// 银行类型，采用字符串类型的银行标识
        /// </summary>
        public string bank_type { set; get; }
        /// <summary>
        /// 订单总金额，单位为分，只能为整数
        /// </summary>
        public int total_fee { set; get; }
        /// <summary>
        /// 现金支付金额
        /// </summary>
        public string cash_fee { set; get; }
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { set; get; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { set; get; }
        /// <summary>
        /// 商家数据包，原样返回
        /// </summary>
        public string attach { set; get; }
        /// <summary>
        /// 支付完成时间(格式为yyyyMMddHHmmss)
        /// </summary>
        public string time_end { set; get; }
    }
}

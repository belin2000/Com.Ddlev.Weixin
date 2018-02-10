using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 代金券
    /// </summary>
    public class couponRequest:PayBase.WXComm, IFace.IRequest<couponResponse>
    {
        /// <summary>
        /// 代金券批次id(微信商户后台活动)
        /// </summary>
        public string coupon_stock_id { set; get; }
        /// <summary>
        /// openid记录数（目前支持1）(不可配置)
        /// </summary>
        public int openid_count { set; get; }
        /// <summary>
        /// 商户单据号	(商户此次发放凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性)(不可配置)
        /// </summary>
        public string partner_trade_no { set; get; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// 操作员帐号, 默认为商户号 可在商户平台配置操作员对应的api权限(非必填)
        /// </summary>
        public string op_user_id { set; get; }
        /// <summary>
        /// 设备号非必填
        /// </summary>
        public string device_info { set; get; }
        /// <summary>
        /// 协议版本(默认1.0)	(不可配置)
        /// </summary>
        public string version { set; get; }
        /// <summary>
        /// 协议类型(XML【目前仅支持默认XML】)(不可配置)
        /// </summary>
        public string type { set; get; }
        /// <summary>
        /// 代金券 初始化（需要设置属性）
        /// </summary>
        /// <param name="_c"></param>
        public couponRequest( Config _c) {
            this.c = _c;
            this.appid = c.AppID;
            this.device_info = "";
            this.mch_id = c.Mchid;
            this.nonce_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            this.openid_count = 1;
            this.op_user_id = "";
            this.partner_trade_no = mch_id + DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(10, 99);
            this.type = "XML";
            this.version = "1.0";
        }
        public couponResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/send_coupon";
            return send(url);
        }
        protected couponResponse send(string url)
        {
            var dic= BaseClass.BaseMethod.MakeToDictionary(this);
            dic.Remove("sign");
            var paySign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8", 0) + "&key=" + c.Key, "MD5", "utf-8").ToUpper();
            this.sign = paySign;
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8, "", c.certpath, c.Mchid);
            couponResponse ucb = new couponResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, ucb);
            return ucb;
        }

        public async Task<couponResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }

    public class couponResponse: BaseClass.BusinessLogic,IFace.IResponse
    {
        /// <summary>
        /// 代金券批次id	
        /// </summary>
        public string coupon_stock_id { set; get; }
        /// <summary>
        /// 返回记录数	
        /// </summary>
        public int resp_count { set; get; }
        /// <summary>
        /// 成功记录数	(0或者1)
        /// </summary>
        public int success_count { set; get; }
        /// <summary>
        /// 失败记录数	(0或者1)
        /// </summary>
        public int failed_count { set; get; }
        /// <summary>
        /// 用户标识openid	
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// 代金券id	
        /// </summary>
        public string coupon_id { set; get; }
    }
}

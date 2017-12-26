using Com.Ddlev.Weixin.BaseClass;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;


namespace Com.Ddlev.Weixin.High.Pay.Unifiedorder
{
    /// <summary>
    /// 统一下单接口
    /// </summary>
    public class unifiedorderRequest: UOrderBase, IFace.IRequest<unifiedorderResponse>
    {

        /// <summary>
        /// 微信支付分配的子商户号，受理模式下必填
        /// </summary>
        public string sub_mch_id { set; get; }

        /// <summary>
        /// 订单生成时间，格式为 yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。时区为GMT+8beijing。该时间取自商户服务器
        /// </summary>
        public string time_start { set; get; }
        /// <summary>
        /// 订单失效时间，格式为 yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。时区为GMT+8beijing。该时间取自商户服务器
        /// </summary>
        public string time_expire { set; get; }
        
        /// <summary>
        /// 接收微信支付成功通知(必填)
        /// </summary>
        public string notify_url { set; get; }
        /// <summary>
        /// JSAPI、NATIVE、APP、MWEB(h5页面下支付) (必填)[商户号模式下不能使用native]
        /// </summary>
        public string trade_type { set; get; }
        /// <summary>
        /// 用户在商户appid下的唯一标识，trade_type为JSAPI 时，此参数必传，获取方式见表头说明。 [商户号]openid和sub_openid可以选传其中之一;建议使用sub_openid
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// 只在 trade_type 为 NATIVE 时需要填写。此id为二维码中包含的商品ID，商户自行维护。
        /// </summary>
        public string product_id { set; get; }
        /// <summary>
        /// [商户号使用]微信分配的子商户公众账号ID，如需在支付完成后获取sub_openid则此参数必传。
        /// </summary>
        public string sub_appid { set; get; }
        /// <summary>
        /// [商户号使用]trade_type=JSAPI，此参数必传，用户在子商户appid下的唯一标识。openid和sub_openid可以选传其中之一，如果选择传sub_openid,则必须传sub_appid。
        /// </summary>
        public string sub_openid { set; get; }

        /// <summary>
        /// Scene_Info类转换的json,在h5支付下有效
        /// </summary>
        public string scene_info { set; get; }

        public unifiedorderRequest(Config C)
        {
            this.c = C;
        }

        /// <summary>
        /// 获取预支付请求后返回数据
        /// </summary>
        /// <param name="u"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        protected unifiedorderResponse send(string url= "https://api.mch.weixin.qq.com/pay/unifiedorder")
        {
            if (string.IsNullOrEmpty(this.sign) || this.sign == "")
            {
                this.sign= BaseClass.BaseMethod.MakeSign(this,c);
            }
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this,true);
            //获取请求回来的xml数据
            string bxml= BaseClass.BaseMethod.WebRequestPost(xml, url,Encoding.UTF8);
            unifiedorderResponse ucb = new unifiedorderResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, ucb);
            return ucb;
        }
        public unifiedorderResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            return send(url);
        }

    }

    public class unifiedorderResponse: BaseClass.BusinessLogic, IFace.IResponse
    {
        ///以下字段在 return_code 和 result_code   都为 SUCCESS 的时候有 返回

        /// <summary>
        /// 交易类型 JSAPI、NATIVE、APP、MWEB(H5支付)
        /// </summary>
        public string trade_type { set; get; }
        /// <summary>
        /// 微信生成的预支付ID，用于后续接口调用中使用
        /// </summary>
        public string prepay_id { set; get; }
        /// <summary>
        /// trade_type 为 NATIVE 是有返回，此参数可直接生成二维码展示出来进行扫码支付
        /// </summary>
        public string code_url { set; get; }
        /// <summary>
        /// mweb_url为拉起微信支付收银台的中间页面，可通过访问该url来拉起微信客户端，完成支付,mweb_url的有效期为5分钟。在MWEB(H5支付)，跳转到改页面进行支付
        /// </summary>
        public string mweb_url { set; get; }

        /// <summary>
        /// 验证返回的数据是否合法
        /// </summary>
        /// <param name="c"></param>
        /// <param name="uc"></param>
        /// <returns></returns>
        public bool Check(Config c)
        {
            SortedDictionary<string, string> dic = BaseClass.BaseMethod.MakeToDictionary(this,1);
            dic.Remove("sign");
            return BaseClass.BaseMethod.Check(dic, c);
        }
    }


    public class Scene_Info
    {
        /// <summary>
        /// 使用H5_Info 的json
        /// </summary>
        public string h5_info { set; get; }

        public Scene_Info() { }
        public Scene_Info(IH5_Info _h5_info)
        {
            this.h5_info= Newtonsoft.Json.JsonConvert.SerializeObject( _h5_info);
        }
    }

}

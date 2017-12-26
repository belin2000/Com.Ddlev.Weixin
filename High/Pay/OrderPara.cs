using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 用于公众号支付的类（jsapi支付和小程序支付）
    /// </summary>
    public class OrderParaJS: PayBase.OrderParaComm
    {
        
        /// <summary>
        /// 签名方式（3.X为MD5）
        /// </summary>
        public string signType { set; get; }
        /// <summary>
        /// 签名(3.X 大写)
        /// </summary>
        public string paySign { set; get; }

        public OrderParaJS(Config _c)
        {
            this.appId = c.AppID;
            this.timeStamp = BaseClass.BaseMethod.ConvertDateTimeInt(DateTime.Now).ToString();
            this.nonceStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.signType = "MD5";
        }
        public OrderParaJS(Config c, Unifiedorder.unifiedorderRequest u)
        {
            var s = u.send();
            string prepay_id = "";
            if (s.return_code == "SUCCESS" && s.result_code == "SUCCESS")
            {
                prepay_id = s.prepay_id;
            }
            this.appId = c.AppID;
            this.timeStamp = BaseClass.BaseMethod.ConvertDateTimeInt(DateTime.Now).ToString();
            this.nonceStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.package = "prepay_id=" + prepay_id;
            this.signType = "MD5";
            SortedDictionary<string, string> dic = BaseClass.BaseMethod.MakeToDictionary(this,0);
            dic.Remove("paySign");
            //dic.Remove("signType");
            this.paySign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8",0) + "&key=" + c.Key, "MD5", "utf-8").ToUpper();
        }
    }
    /// <summary>
    /// 用于公众号支付的类（APP支付）
    /// </summary>
    public class OrderParaAPP
    {
        protected Config c;
        string _appid;
        /// <summary>
        /// 微信分配的公众账号ID
        /// </summary>
        public string appid
        {
            get { return _appid; }
            set { _appid = value; }
        }
        string _partnerid;
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string partnerid
        {
            get { return _partnerid; }
            set { _partnerid = value; }
        }

        string _prepayid;
        /// <summary>
        /// 微信返回的支付交易会话ID
        /// </summary>
        public string prepayid
        {
            get { return _prepayid; }
            set { _prepayid = value; }
        }
        string _package;
        /// <summary>
        /// 暂填写固定值Sign=WXPay
        /// </summary>
        public string package
        {
            get { return _package; }
            set { _package = value; }
        }
        string _noncestr;
        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string noncestr
        {
            get { return _noncestr; }
            set { _noncestr = value; }
        }
        string _timestamp;
        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }
        string _sign;
        /// <summary>
        /// 签名
        /// </summary>
        public string sign
        {
            get { return _sign; }
            set { _sign = value; }
        }

        public OrderParaAPP(Config _c)
        {
            this.c = _c;
            this.appid = c.AppID;
            this.timestamp = BaseClass.BaseMethod.ConvertDateTimeInt(DateTime.Now).ToString();
            this.noncestr = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.package = "Sign=WXPay";
        }
        public OrderParaAPP(Config c, Unifiedorder.unifiedorderRequest u)
        {
            var s = u.send();
            string prepay_id = "";
            if (s.return_code == "SUCCESS" && s.result_code == "SUCCESS")
            {
                prepay_id = s.prepay_id;
            }
            this.appid = c.AppID;
            this.timestamp = BaseClass.BaseMethod.ConvertDateTimeInt(DateTime.Now).ToString();
            this.noncestr = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.prepayid = prepay_id;
            this.partnerid = c.Mchid;
            SortedDictionary<string, string> dic = BaseClass.BaseMethod.MakeToDictionary(this, 0);
            dic.Remove("sign");
            this.sign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8", 0) + "&key=" + c.Key, "MD5", "utf-8").ToUpper();
        }
    }


}

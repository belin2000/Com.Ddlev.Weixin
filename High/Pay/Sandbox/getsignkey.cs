using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Com.Ddlev.Weixin.High.Pay.Sandbox
{
    /// <summary>
    /// 获取沙箱的支付密钥
    /// </summary>
    public class getsignkeyRequest
    {
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { set; get; }
        /// <summary>
        /// 支付密钥
        /// </summary>
        string Key;
        string Appid;
        /// <summary>
        /// 随机码
        /// </summary>
        public string nonce_str {set;get;}
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { set;get;}

        public getsignkeyRequest(string _mch_id, string _key,string _appid)
        {
            this.Key = _key;
            this.Appid = _appid;
            this.mch_id = _mch_id;
            this.nonce_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
        public Config getconfig()
        {
            var c = new Config();
            string url = "https://api.mch.weixin.qq.com/sandboxnew/pay/getsignkey";
            SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
            dic.Add("mch_id", this.mch_id);
            dic.Add("nonce_str", this.nonce_str);
            var paySign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8", 0) + "&key=" + Key, "MD5", "utf-8").ToUpper();
            this.sign = paySign;
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8);
            getsignkeyResponse ucb = new getsignkeyResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, ucb);
            c.Mchid = this.mch_id;
            c.AppID = Appid;
            c.Key = ucb.sandbox_signkey;
            return c;
        }
        
    }

    public class getsignkeyResponse: BaseClass.ReturnCode
    {
        public string sandbox_signkey { set; get; }
    }
}

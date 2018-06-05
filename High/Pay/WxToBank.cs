using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 微信转账到银行卡
    /// </summary>
    public class WxToBankRequst:IFace.IRequest<WxToBankResponse>
    {
        Com.Ddlev.Weixin.High.Pay.Config c;
        /// <summary>
        /// 商户号	(默认赋值)
        /// </summary>
        public string mch_id { set; get; }
        /// <summary>
        /// 商户企业付款单号	
        /// </summary>
        public string partner_trade_no { set; get; }
        /// <summary>
        /// 随机数(默认赋值)	
        /// </summary>
        public string nonce_str { set; get; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { set; get; }
        /// <summary>
        /// 收款方银行卡号	(rsa加密后的值)
        /// </summary>
        public string enc_bank_no { set; get; }
        /// <summary>
        /// 收款方用户名	(rsa加密后的值)
        /// </summary>
        public string enc_true_name { set; get; }
        /// <summary>
        /// 收款方开户行	
        /// </summary>
        public string bank_code { set; get; }
        /// <summary>
        /// 付款金额	RMB分
        /// </summary>
        public int amount { set; get; }
        /// <summary>
        /// 付款说明	
        /// </summary>
        public string desc { set; get; }
        /// <summary>
        /// 使用openssl生成的证书（格式）
        /// </summary>
        string cartpath;
        /// <summary>
        /// 微信转账到银行卡
        /// </summary>
        /// <param name="_c">配置</param>
        /// <param name="_cartpath">rsa证书的物理路径</param>
        /// <param name="_partner_trade_no">商户企业付款单号(唯一性)</param>
        /// <param name="bank_no">收款方银行卡号(明文)</param>
        /// <param name="true_name">收款方用户名(明文)</param>
        /// <param name="_amount">付款金额，单位分</param>
        /// <param name="_bank_code">银行编码</param>
        /// <param name="_desc">付款说明</param>
        public WxToBankRequst(Config _c, string _cartpath,string _partner_trade_no="",string bank_no="",string true_name="",string _bank_code="",int _amount=0,string _desc="")
        {
            this.c = _c;
            this.mch_id = c.Mchid;
            this.nonce_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            this.cartpath = _cartpath;
            this.partner_trade_no = _partner_trade_no;
            if (!string.IsNullOrWhiteSpace(bank_no))
            {
                this.enc_bank_no = Com.Ddlev.Cryptography.Encrypt.RSAEncrypt(bank_no, cartpath, true);
            }
            if (!string.IsNullOrWhiteSpace(true_name))
            {
                this.enc_true_name = Com.Ddlev.Cryptography.Encrypt.RSAEncrypt(true_name, cartpath, true);
            }
            this.bank_code = _bank_code;
            this.amount = _amount;
            this.desc = _desc;
        }

        public WxToBankResponse send()
        {
            var url = "https://api.mch.weixin.qq.com/mmpaysptrans/pay_bank";
            return send(url);
        }
        protected WxToBankResponse send(string url)
        {
            SortedDictionary<string, string> dic = BaseClass.BaseMethod.MakeToDictionary(this, 1);
            dic.Remove("sign");
            string sign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8") + "&key=" + c.Key, "MD5", "utf-8").ToUpper();
            this.sign = sign;
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8, "", c.certpath, c.Mchid);
            WxToBankResponse rf = new WxToBankResponse();
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rf);
            return rf;
        }

        public async Task<WxToBankResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class WxToBankResponse : BaseClass.BusinessLogic, IFace.IResponse
    {
        public string partner_trade_no { set; get; }
        public int amount { set; get; }
        public string payment_no { set; get; }
        public int cmms_amt { set; get; }
    }
}

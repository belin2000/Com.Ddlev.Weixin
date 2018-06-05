using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 查询微信转账到银行卡是否成功
    /// </summary>
    public class QueryBankRequest: IFace.IRequest<QueryBankResponse>
    {
        Config c;
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
        /// 查询微信转账到银行卡的结果
        /// </summary>
        /// <param name="_c">带证书路径的配置</param>
        /// <param name="_partner_trade_no">商户单号</param>
        public QueryBankRequest(Config _c,string _partner_trade_no)
        {
            this.c = _c;
            this.mch_id = c.Mchid;
            this.partner_trade_no = _partner_trade_no;
            this.nonce_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        public QueryBankResponse send()
        {
            var url = "	https://api.mch.weixin.qq.com/mmpaysptrans/query_bank";
            return send(url);
        }
        QueryBankResponse send(string url)
        {
            SortedDictionary<string, string> dic = BaseClass.BaseMethod.MakeToDictionary(this, 1);
            dic.Remove("sign");
            string sign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8") + "&key=" + c.Key, "MD5", "utf-8").ToUpper();
            this.sign = sign;
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8, "", c.certpath, c.Mchid);
            QueryBankResponse rf = new QueryBankResponse();
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rf);
            return rf;
        }

        public async Task<QueryBankResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class QueryBankResponse : BaseClass.BusinessLogic, IFace.IResponse
    {
        /// <summary>
        /// 商户企业付款单号(商户单号)
        /// </summary>
        public string partner_trade_no { set; get; }
        /// <summary>
        /// 微信企业付款单号	
        /// </summary>
        public string payment_no { set; get; }


        /// <summary>
        /// 收款用户银行卡号(MD5加密)
        /// </summary>
        public string bank_no_md5 { set; get; }
        /// <summary>
        /// 收款人真实姓名（MD5加密）
        /// </summary>
        public string true_name_md5 { set; get; }
        /// <summary>
        /// 代付金额	
        /// </summary>
        public int amount { set; get; }
        /// <summary>
        /// 代付单状态	[代付订单状态： PROCESSING（处理中，如有明确失败，则返回额外失败原因；否则没有错误原因）SUCCESS（付款成功）FAILED（付款失败,需要替换付款单号重新发起付款）BANK_FAIL（银行退票，订单状态由付款成功流转至退票,退票时付款金额和手续费会自动退还）]
        /// </summary>
        public string status { set; get; }
        /// <summary>
        /// 手续费金额	
        /// </summary>
        public int cmms_amt { set; get; }
        /// <summary>
        /// 商户下单时间	
        /// </summary>
        public string create_time { set; get; }
        /// <summary>
        /// 成功付款时间	
        /// </summary>
        public string pay_succ_time { set; get; }
        /// <summary>
        /// 失败原因	
        /// </summary>
        public string reason { set; get; }
    }
}

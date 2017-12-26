using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Xml;
using Com.Ddlev.Weixin.BaseClass;

namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 企业转账
    /// </summary>
    public class TransfersRequest:PayBase.WXCommBa,IFace.IRequest<TransfersResponse>
    {
        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string mch_appid { set; get; }
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mchid { set; get; }
        /// <summary>
        /// 微信支付分配的终端设备号(非必填)
        /// </summary>
        public string device_info { set; get; }
        /// <summary>
        /// 商户订单号，需保持唯一性
        /// </summary>
        public string partner_trade_no { set; get; }
        /// <summary>
        /// 商户appid下，某用户的openid
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// NO_CHECK：不校验真实姓名 ;FORCE_CHECK：强校验真实姓名（未实名认证的用户会校验失败，无法转账） ;OPTION_CHECK：针对已实名认证的用户才校验真实姓名（未实名认证用户不校验，可以转账成功）
        /// </summary>
        public string check_name { set; get; }
        /// <summary>
        /// 收款用户真实姓名。如果check_name设置为FORCE_CHECK或OPTION_CHECK，则必填用户真实姓名(非必填)
        /// </summary>
        public string re_user_name { set; get; }
        /// <summary>
        /// 企业付款金额，单位为分
        /// </summary>
        public int amount { set; get; }
        /// <summary>
        /// 企业付款操作说明信息。必填。
        /// </summary>
        public string desc { set; get; }
        /// <summary>
        /// 调用接口的机器Ip地址(必填)
        /// </summary>
        public string spbill_create_ip { set; get; }
        public TransfersRequest(Config _c)
        {
            this.c = _c;
        }
        protected TransfersResponse send(string url= "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers")
        {
            if (string.IsNullOrEmpty(this.sign) || this.sign == "")
            {
                this.sign = BaseClass.BaseMethod.MakeSign(this, c);
            }
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8, "", c.certpath, c.Mchid);
            var rs = new TransfersResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rs);
            return rs;
        }
        public TransfersResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";
            return send(url);
        }
    }

    public class TransfersResponse : BaseClass.BusinessLogic, IFace.IResponse
    {
        string _mch_appid;
        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string mch_appid
        {
            get
            {
                return _mch_appid;
            }

            set
            {
                _mch_appid = value;
            }
        }
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mchid
        {
            get
            {
                return _mchid;
            }

            set
            {
                _mchid = value;
            }
        }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string partner_trade_no
        {
            get
            {
                return _partner_trade_no;
            }

            set
            {
                _partner_trade_no = value;
            }
        }
        /// <summary>
        /// 微信订单号
        /// </summary>
        public string payment_no
        {
            get
            {
                return _payment_no;
            }

            set
            {
                _payment_no = value;
            }
        }
        /// <summary>
        /// 微信支付成功时间
        /// </summary>
        public string payment_time
        {
            get
            {
                return _payment_time;
            }

            set
            {
                _payment_time = value;
            }
        }

        string _payment_time;

        string _payment_no;
        string _partner_trade_no;
        string _mchid;
    }

    /// <summary>
    /// 查询转账状态
    /// </summary>
    public class TransferInfo:PayBase.WXComm,IFace.IRequest<TransferInfoResponse>
    {
        /// <summary>
        /// 商户调用企业付款API时使用的商户订单号 
        /// </summary>
        public string partner_trade_no { set; get; }
        public TransferInfo(Config _c)
        {
            this.c = _c;
        }
        protected TransferInfoResponse send(string url)
        {
            if (string.IsNullOrEmpty(this.sign) || this.sign == "")
            {
                this.sign = BaseClass.BaseMethod.MakeSign(this, c);
            }
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8,"",c.certpath,c.Mchid);
            var rs = new TransferInfoResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rs);
            return rs;
        }
        public TransferInfoResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/secapi/pay/refund";
            return send(url);
        }

    }
    public class TransferInfoResponse : BaseClass.BusinessLogic, IFace.IResponse
    {
        /// <summary>
        /// 商户使用查询API填写的单号的原路返回. 
        /// </summary>
        public string partner_trade_no { set; get; }
        /// <summary>
        /// 调用企业付款API时，微信系统内部产生的单号
        /// </summary>
        public string detail_id { set; get; }
        /// <summary>
        /// SUCCESS:转账成功         FAILED:转账失败        PROCESSING:处理中
        /// </summary>
        public string status { set; get; }
        /// <summary>
        /// 如果失败则有失败原因
        /// </summary>
        public string reason { set; get; }
        /// <summary>
        /// 转账的openid
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// 收款用户姓名
        /// </summary>
        public string transfer_name { set; get; }
        /// <summary>
        /// 付款金额  
        /// </summary>
        public int payment_amount { set; get; }
        /// <summary>
        /// 转账时间 
        /// </summary>
        public DateTime transfer_time { set; get; }
        /// <summary>
        /// 付款描述 
        /// </summary>
        public string desc { set; get; }
    }


}

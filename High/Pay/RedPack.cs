using Com.Ddlev.Weixin.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 发送红包
    /// </summary>
    public class SendRedPack: PayBase.WXCommBase,IFace.IRequest<SendRedPackResponse>
    {
        /// <summary>
        /// 商户订单号（每个订单号必须唯一）组成：mch_id+yyyymmdd+10位一天内不能重复的数字。 接口根据商户订单号支持重入，如出现超时可再调用。
        /// </summary>
        public string mch_billno { set; get; }
        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId）。接口传入的所有appid应该为公众号的appid（在mp.weixin.qq.com申请的），不能为APP的appid（在open.weixin.qq.com申请的）。 
        /// </summary>
        public string wxappid { set; get; }
        /// <summary>
        /// 红包发送者名称(商户名称)
        /// </summary>
        public string send_name { set; get; }
        /// <summary>
        /// 用户openid(接受红包的用户)
        /// </summary>
        public string re_openid { set; get; }
        /// <summary>
        /// 付款金额，单位分
        /// </summary>
        public int total_amount { set; get; }

        public int total_num { set; get; }
        /// <summary>
        /// 红包祝福语
        /// </summary>
        public string wishing { set; get; }
        /// <summary>
        /// 机器ip地址
        /// </summary>
        public string client_ip { set; get; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string act_name { set; get; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string remark { set; get; }

        public SendRedPack(Config _c)
        {
            this.c = _c;
            this.mch_id = c.Mchid;
            this.nonce_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            this.wxappid = c.AppID;
        }
        protected SendRedPackResponse send(string url="https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack")
        {
            if (string.IsNullOrEmpty(this.sign) || this.sign == "")
            {
                this.sign = BaseClass.BaseMethod.MakeSign(this, c);
            }
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8, "", c.certpath, c.Mchid);
            var rs = new SendRedPackResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rs);
            return rs;
        }
        public SendRedPackResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";
            return this.send(url);
        }

    }
    /// <summary>
    /// 发送红包的结果
    /// </summary>
    public class SendRedPackResponse : BaseClass.BusinessLogic, IFace.IResponse
    {
        /// <summary>
        /// mch_billno
        /// </summary>
        public string mch_billno { set; get; }
        
        /// <summary>
        /// 商户appid，接口传入的所有appid应该为公众号的appid (在mp.weixin.qq.com申请的)
        /// </summary>
        public string wxappid { set; get; }
        /// <summary>
        /// 接受收红包的用户 用户在wxappid下的openid
        /// </summary>
        public string re_openid { set; get; }
        /// <summary>
        /// 付款金额，单位分
        /// </summary>
        public int total_amount { set; get; }
        /// <summary>
        /// 红包发送时间
        /// </summary>
        public DateTime send_time { set; get; }
        /// <summary>
        /// 微信单号
        /// </summary>
        public string send_listid { set; get; }
    }

    /// <summary>
    /// 查询红包
    /// </summary>
    public class Gethbinfo:PayBase.WXCommBase, IFace.IRequest<GethbinfoResponse>
    {
        /// <summary>
        /// 商户发放红包的商户订单号
        /// </summary>
        public string mch_billno { set; get; }
        /// <summary>
        /// 微信分配的公众账号ID（企业号corpid即为此appId），接口传入的所有appid应该为公众号的appid（在mp.weixin.qq.com申请的）
        /// </summary>
        public string appid { set; get; }
        /// <summary>
        /// MCHT(固定的值):通过商户订单号获取红包信息。
        /// </summary>
        public string bill_type { set; get; }
        public Gethbinfo(Config _c)
        {
            this.c = _c;
        }
        protected GethbinfoResponse send(string url)
        {
            if (string.IsNullOrEmpty(this.sign) || this.sign == "")
            {
                this.sign = BaseClass.BaseMethod.MakeSign(this, c);
            }
            //组成xml
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            //获取请求回来的xml数据
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8, "", c.certpath, c.Mchid);
            var rs = new GethbinfoResponse();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rs);
            return rs;
        }
        public GethbinfoResponse send()
        {
            string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/gethbinfo";
            return send(url);
        }
    }

    public class GethbinfoResponse : BaseClass.BusinessLogic,IFace.IResponse
    {
        /// <summary>
        /// 商户使用查询API填写的商户单号的原路返回
        /// </summary>
        public string mch_billno { set; get; }
        /// <summary>
        /// 使用API发放现金红包时返回的红包单号 
        /// </summary>
        public string detail_id { set; get; }
        /// <summary>
        /// SENDING:发放中         SENT:已发放待领取        FAILED：发放失败        RECEIVED:已领取        REFUND:已退款
        /// </summary>
        public string status { set; get; }
        /// <summary>
        /// 发放类型(API:通过API接口发放         UPLOAD:通过上传文件方式发放        ACTIVITY:通过活动方式发放 )
        /// </summary>
        public string send_type { set; get; }
        /// <summary>
        /// GROUP:裂变红包         NORMAL:普通红包
        /// </summary>
        public string hb_type { set; get; }
        /// <summary>
        /// 红包个数 
        /// </summary>
        public int total_num { set; get; }
        /// <summary>
        /// 红包金额 
        /// </summary>
        public int total_amount { set; get; }
        /// <summary>
        /// 发送失败原因
        /// </summary>
        public string reason { set; get; }
        /// <summary>
        /// 红包发送时间
        /// </summary>
        public DateTime send_time { set; get; }
        /// <summary>
        /// 红包的退款时间（如果其未领取的退款） 
        /// </summary>
        public DateTime refund_time { set; get; }
        /// <summary>
        /// 红包退款金额
        /// </summary>
        public int refund_amount { set; get; }
        /// <summary>
        /// 祝福语 
        /// </summary>
        public string wishing { set; get; }
        /// <summary>
        /// 活动描述，低版本微信可见 
        /// </summary>
        public string remark { set; get; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string act_name { set; get; }
        /// <summary>
        /// 裂变红包领取列表 
        /// </summary>
        List<hbinfo> hblist { set; get; }
    }


    /// <summary>
    /// 返回的抢红包的信息
    /// </summary>
    public class hbinfo
    {
        /// <summary>
        /// 领取红包的Openid
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// RECEIVED:已领取
        /// </summary>
        public string status { set; get; }
        /// <summary>
        /// 金额
        /// </summary>
        public int amount { set; get; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public DateTime rcv_time { set; get; }
    }

}

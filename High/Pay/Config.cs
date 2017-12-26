using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Pay
{
    public class Config: Com.Ddlev.Weixin.High.Config
    {
        /// <summary>
        /// 商户 ID，身份标识
        /// </summary>
        public string Mchid { set; get; }
        /// <summary>
        /// 子商户ID，受理模式下子商户与受理商有绑定关系，没有不提交该参数
        /// </summary>
        public string SubMchId { set; get; }
        /// <summary>
        /// 商户支付密钥 Key [子商户要用服务商的key]
        /// </summary>
        public string Key { set; get; }
        /// <summary>
        /// 子商户公众号
        /// </summary>
        public string Sub_Appid { set; get; }        

        /// <summary>
        /// 证书的路径(真实路径 c:\ab.cert)（退款红包等必填）
        /// </summary>
        public string certpath { set; get; }
        public Config() { }
        /// <summary>
        /// 商户使用
        /// </summary>
        /// <param name="_Appid"></param>
        /// <param name="_Mchid"></param>
        /// <param name="_Key"></param>
        public Config(string _Appid, string _Mchid, string _Key)
        {
            this.AppID = _Appid;
            this.Mchid = _Mchid;
            this.Key = _Key;
        }
        /// <summary>
        /// 商户使用
        /// </summary>
        /// <param name="_Appid"></param>
        /// <param name="_Mchid"></param>
        /// <param name="_Key"></param>
        /// <param name="_certpath">证书的路径(真实路径 c:\ab.cert)（退款红包等必填）</param>
        public Config(string _Appid, string _Mchid, string _Key,string _certpath)
        {
            this.AppID = _Appid;
            this.Mchid = _Mchid;
            this.Key = _Key;
            this.certpath = _certpath;
        }
        /// <summary>
        /// 子商户使用
        /// </summary>
        /// <param name="_Appid"></param>
        /// <param name="Sub_Appid"></param>
        /// <param name="_Mchid"></param>
        /// <param name="_SubMchId"></param>
        /// <param name="_Key"></param>
        public Config(string _Appid, string Sub_Appid, string _Mchid, string _SubMchId, string _Key)
        {
            this.AppID = _Appid;
            this.Sub_Appid = Sub_Appid;
            this.Mchid = _Mchid;
            this.SubMchId = _SubMchId;
            this.Key = _Key;
        }
        /// <summary>
        /// 子商户使用
        /// </summary>
        /// <param name="_Appid"></param>
        /// <param name="Sub_Appid"></param>
        /// <param name="_Mchid"></param>
        /// <param name="_SubMchId"></param>
        /// <param name="_Key"></param>
        /// <param name="_certpath">证书的路径(真实路径 c:\ab.cert)（退款红包等必填）</param>
        public Config(string _Appid, string Sub_Appid, string _Mchid, string _SubMchId, string _Key, string _certpath)
        {
            this.AppID = _Appid;
            this.Sub_Appid = Sub_Appid;
            this.Mchid = _Mchid;
            this.SubMchId = _SubMchId;
            this.Key = _Key;
            this.certpath = _certpath;
        }
    }
}

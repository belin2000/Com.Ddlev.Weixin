using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Pay
{
    /// <summary>
    /// 获取企业付款的rsa证书
    /// </summary>
    public class GetPublicKeyRequest:IFace.IRequest<GetPublicKeyResponse>
    {
        Config c;
        public string sign { set; get; }
        public string mch_id { set; get; }
        public string nonce_str { set; get; }
        public string sign_type { set; get; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_c">配置,带微信证书的路径</param>
        public GetPublicKeyRequest(Config _c)
        {
            this.c = _c;
            sign = "";
            mch_id = c.Mchid;
            nonce_str = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            sign_type = "MD5";
        }
        /// <summary>
        /// 获取返回来的数据（建议把返回的数据中的pub_key生成.pem文件存放在服务器上，不需要每次都获取微信服务器的rsakey）
        /// </summary>
        /// <returns></returns>
        public GetPublicKeyResponse send()
        {
            var url = "https://fraud.mch.weixin.qq.com/risk/getpublickey";
            return send(url);
        }
        /// <summary>
        /// 获取返回来的数据（建议把返回的数据中的pub_key生成.pem文件存放在服务器上，不需要每次都获取微信服务器的rsakey）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        GetPublicKeyResponse send( string url)
        {
            SortedDictionary<string, string> dic = BaseClass.BaseMethod.MakeToDictionary(this, 1);
            dic.Remove("sign");
            string sign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8") + "&key=" + c.Key, "MD5", "utf-8").ToUpper();
            this.sign = sign;
            string xml = BaseClass.BaseMethod.ObjToXml(this, true);
            string bxml = BaseClass.BaseMethod.WebRequestPost(xml, url, Encoding.UTF8, "", c.certpath, c.Mchid);
            GetPublicKeyResponse rf = new GetPublicKeyResponse();
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(bxml);
            BaseClass.BaseMethod.XmlToObj(xmlDoc, rf);
            return rf;
        }

        public async Task<GetPublicKeyResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    /// <summary>
    /// 获取返回的数据
    /// </summary>
    public class GetPublicKeyResponse : BaseClass.BusinessLogic, IFace.IResponse
    {
        public string pub_key { set; get; }
    }
}

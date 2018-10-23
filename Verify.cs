using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ddlev.Weixin.IFace;

namespace Com.Ddlev.Weixin
{
    /// <summary>
    /// 验证服务器 和消息体
    /// </summary>
    public class Verify
    {
        /// <summary>
        /// 判断是验证还是消息
        /// </summary>
        /// <returns>如果是验证，true(然后使用CheckConfig进行验证),否则为false.</returns>
        public static bool ISVerify()
        {
            return System.Web.HttpContext.Current.Request.InputStream.Length > 0 ? false : true;
        }

        /// <summary>
        /// 检测验证(ISVerify()为true才能调用)
        /// </summary>
        /// <param name="timestamp">时间戳 </param>
        /// <param name="nonce">随机数 </param>
        /// <param name="echostr">随机字符串</param>
        /// <param name="signature">微信加密签名</param>
        /// <returns></returns>
        public static string Check(string timestamp, string nonce, string echostr, string signature, Config _c)
        {
            string[] sd = { _c.Token, timestamp, nonce };
            Array.Sort(sd);
            string gdata = string.Join("", sd);
            return signature.ToLower().Equals(Com.Ddlev.Cryptography.Encrypt.SHA1(gdata), StringComparison.OrdinalIgnoreCase) ? echostr : "";
        }
    }

    /// <summary>
    /// 微信消息体处理
    /// </summary>
    public class WechatMessage
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public IWechatMessage GetMessage(string xml,Config c)
        {
            //判断这个xml是否是加密体
            var xd = new  System.Xml.XmlDocument();
            xd.LoadXml(xml);
            return GetMessage(xd,c);
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="xd"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public IWechatMessage GetMessage(System.Xml.XmlDocument xd,Config c)
        {
            if (xd.SelectNodes("xml/Encrypt").Count > 0)
            {
                //表示有加密消息体
                return GetMessage(Tencent.Cryptography.AES_decrypt(xd.SelectSingleNode("xml/Encrypt").InnerXml, c.EncodingAESKey), c);
            }
            else
            {
                //表示明文的消息体
                return GetMessage_(xd);
            }
        }
        /// <summary>
        /// 获得明文的消息体
        /// </summary>
        /// <param name="xd"></param>
        /// <returns></returns>
        IWechatMessage GetMessage_(System.Xml.XmlDocument xd)
        {
            string objname = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(xd.SelectSingleNode("xml/MsgType").InnerText);
            System.Reflection.Assembly ab = System.Reflection.Assembly.GetExecutingAssembly();
            object t = Activator.CreateInstance(ab.GetType("Com.Ddlev.Weixin.BaseClass.MsgPropertiesFor" + objname));
            BaseClass.BaseMethod.XmlToObj(xd, t);
            return (IWechatMessage)t;
        }

        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public string SendMessage(IWechatMessage t, Config c)
        {
            if (c.EncodingType == 1)
            {
                return SendMessage(t);
            }
            else
            {
                string xmld = SendMessage(t);
                BaseClass.EncryptBase eb = new BaseClass.EncryptBase();
                eb.Encrypt = Tencent.Cryptography.AES_encrypt(xmld, c.EncodingAESKey, c.AppID);
                eb.MsgSignature = BaseClass.BaseMethod.MsgSignature(eb.Encrypt, eb.TimeStamp.ToString(), eb.Nonce, c);
                return BaseClass.BaseMethod.ObjToXml(eb, true);
            }
        }
        string SendMessage(IWechatMessage t)
        {
            return BaseClass.BaseMethod.ObjToXml(t);
        }

    }
}

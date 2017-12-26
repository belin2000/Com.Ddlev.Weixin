using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Reflection;
using System.Web.Security;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;


namespace Com.Ddlev.Weixin.BaseClass
{
    public partial class BaseMethod:Com.Ddlev.Base.BaseMethod
    {


        /// <summary>
        /// 消息体的签名
        /// </summary>
        /// <param name="Encrypt"></param>
        /// <param name="TimeStamp"></param>
        /// <param name="Nonce"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string MsgSignature(string Encrypt, string TimeStamp, string Nonce, Config c)
        {
            string[] sd = { c.Token, TimeStamp, Nonce, Encrypt };
            Array.Sort(sd);
            string gdata = string.Join("", sd);
            return SHA1(gdata).ToLower();
        }
        /// <summary>
        /// 类转换为xml
        /// </summary>
        /// <param name="t">实体类</param>
        /// <param name="SpaceOut">null或者空值是否去掉,true 表示去掉</param>
        /// <returns></returns>
        public static string ObjToXml(object t,bool SpaceOut)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] pis = t.GetType().GetProperties();
            
            foreach (PropertyInfo pi in pis)
            {
                if (pi.PropertyType.IsEnum)//枚举
                {
                    string nvalue = Enum.GetName(pi.PropertyType, pi.GetValue(t, null));
                    sb.Append("<" + pi.Name + "><![CDATA[" + nvalue.Substring(1) + "]]></" + pi.Name + ">");
                    continue;
                }
                if (pi.GetValue(t, null)!=null && Regex.IsMatch(pi.GetValue(t, null).ToString(), @"^[0-9.]+$"))
                {
                    sb.Append("<" + pi.Name + ">" + pi.GetValue(t, null) + "</" + pi.Name + ">");
                    continue;
                }
                if (pi.PropertyType == typeof(String))
                {
                    string p=pi.GetValue(t, null)==null?"":pi.GetValue(t, null).ToString();
                    if (SpaceOut && (string.IsNullOrEmpty(p) || (p=="")))
                    {
                        continue;
                    }
                    else
                    {
                        sb.Append("<" + pi.Name + "><![CDATA[" + pi.GetValue(t, null) + "]]></" + pi.Name + ">");
                        continue;
                    }
                }
                
                
            }
            string xmldata = "<xml>" + sb.ToString() + "</xml>";
            return xmldata;
        }
        /// <summary>
        /// 对象转换为xml
        /// </summary>
        /// <param name="t"></param>
        /// <param name="SpaceOut">null或者空值是否去掉该节点</param>
        /// <returns></returns>
        public static string ObjToXml(Dictionary<string,string> t, bool SpaceOut)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in t)
            {
                if (SpaceOut && (string.IsNullOrEmpty(kvp.Value) || (kvp.Value == "")))
                {
                    continue;
                }
                else
                {
                    if (Regex.IsMatch(kvp.Value, @"^[0-9.]+$"))
                    {
                        sb.Append("<" + kvp.Key + ">" + kvp.Value + "</" + kvp.Key + ">");
                        continue;
                    }
                    else
                    {
                        sb.Append("<" + kvp.Key + "><![CDATA[" + kvp.Value + "]]></" + kvp.Key + ">");
                        continue;
                    }
                }
            }
            string xmldata = "<xml>" + sb.ToString() + "</xml>";
            return xmldata;
        }
        public static void XmlToObj(XmlDocument xd,object t)
        {
            PropertyInfo[] pis = t.GetType().GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                try
                {
                    XmlNode xn = xd.SelectSingleNode("xml/" + pi.Name);
                    if (pi.PropertyType.IsEnum)
                    {
                        //如果是枚举的话
                        pi.SetValue(t, Enum.Parse(pi.PropertyType, "_" + xn.InnerText), null);
                    }
                    else
                    {
                        pi.SetValue(t, Convert.ChangeType(xn.InnerText, pi.PropertyType), null);
                    }


                }
                catch
                {
                    continue;
                }
            }
        }
        /// <summary>
        /// 表示总是接受证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受  
            return true;
        }
        /// <summary>
        /// 只是针对只有一层的xml
        /// </summary>
        /// <param name="xd"></param>
        /// <returns></returns>
        public static SortedDictionary<string, string> XMLToSortedDictionary(System.Xml.XmlDocument xd)
        {
            SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
            XmlElement xe = xd.DocumentElement;
            foreach (XmlNode xn in xe.ChildNodes)
            {
                dic.Add(xn.Name, xn.InnerText);
            }
            return dic;
        }


        #region
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool Check(SortedDictionary<string, string> dic, Com.Ddlev.Weixin.High.Pay.Config c)
        {
            string mysing = dic["sign"];
            //SortedDictionary<string, string> dic = ITA.WeiXin.BaseClass.BaseMethod.MakeToDictionary(cb, 1);
            dic.Remove("sign");
            dic.Remove("paySign");
            string sign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8") + "&key=" + c.Key, "MD5", "utf-8").ToUpper();
            if (mysing != sign)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 生成签名
        /// </summary>
        public static string MakeSign(object t, Com.Ddlev.Weixin.High.Pay.Config c)
        {
            SortedDictionary<string, string> dic = BaseClass.BaseMethod.MakeToDictionary(t, 1);
            dic.Remove("sign");
            string sign = BaseClass.BaseMethod.Sign(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8", 1) + "&key=" + c.Key, "MD5", "utf-8");
            return sign.ToUpper();
        }
        #endregion
    }
}

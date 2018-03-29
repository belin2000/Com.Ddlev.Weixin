using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High
{
    /// <summary>
    /// 针对微信js（前端分享等初始化）
    /// </summary>
    public class WxJs
    {
        /// <summary>
        /// appid
        /// </summary>
        public string appId { set; get; }
        /// <summary>
        /// 生成签名的时间戳
        /// </summary>
        public string timestamp { set; get; }
        /// <summary>
        /// 生成签名的随机串
        /// </summary>
        public string nonceStr { set; get; }
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { set; get; }
        public bool isok { set; get; }

        public WxJs(Config c)
        {
            appId = c.AppID;
            timestamp = BaseClass.BaseMethod.ConvertDateTimeInt(DateTime.Now).ToString();
            nonceStr = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            signature = BaseClass.BaseMethod.Sign("", "MD5", "utf-8");
            var url = System.Web.HttpContext.Current.Request.Url.ToString().Split('#')[0];
            var token = new HightToken(c).Token;
            var jt = JsapiTicket(token);
            if (!string.IsNullOrWhiteSpace(jt))
            {
                isok = true;
                SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
                dic.Add("noncestr", nonceStr);
                dic.Add("timestamp", timestamp);
                dic.Add("url", url);
                dic.Add("jsapi_ticket", jt);
                signature = BaseClass.BaseMethod.SHA1(BaseClass.BaseMethod.MakeUrl(dic, false, "utf-8", 0), System.Text.Encoding.UTF8).ToLower();
            }
            else
            {
                isok = false;
            }
        }
        /// <summary>
        /// 获取用户的ticket
        /// </summary>
        /// <param name="actoken"></param>
        /// <returns></returns>
        public string JsapiTicket(string actoken)
        {
            
            if (!DataCacheConfig.GetHelper().HasKey(appId + "_JsapiTicket"))
            {
                string cb = BaseClass.BaseMethod.WebRequestGet("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + actoken + "&type=jsapi", System.Text.Encoding.UTF8);
                var jo = Newtonsoft.Json.Linq.JObject.Parse(cb);
                if (Convert.ToInt32(jo["errcode"]) == 0)
                {
                    var ticket = jo["ticket"].ToString();
                    DataCacheConfig.GetHelper().Set(appId + "_JsapiTicket", ticket,7000);
                    return ticket;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return DataCacheConfig.GetHelper().Get<string>(appId + "_JsapiTicket").ToString();
            }
        }
    }
}

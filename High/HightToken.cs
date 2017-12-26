using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Com.Ddlev.Weixin.IFace;
namespace Com.Ddlev.Weixin.High
{
    /// <summary>
    /// 高级接口的AccessToken
    /// </summary>
    public class HightToken:IFace.IRequest<HightTokenResponse>
    {
        static readonly object iso = new object();
        Config c;
        /// <summary>
        /// 高级接口的Token(长度大于512)
        /// </summary>
        public string Token { set; get; }
        /// <summary>
        ///  初始化,配置token和缓存时长
        /// </summary>
        /// <param name="C">配置</param>
        /// <param name="_Token">token的字符</param>
        /// <param name="senum">缓存时长（秒）</param>
        public HightToken(Config C,string _Token,int senum=3600)
        {
            this.c = C;
            if (System.Web.HttpContext.Current.Cache.Get(c.AppID + "_HightToken") == null)
            {
                System.Web.HttpContext.Current.Cache.Add(c.AppID + "_HightToken", _Token, null, DateTime.Now.AddSeconds(Convert.ToInt32(senum)), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
                this.Token = _Token;
            }
            else
            {
                this.Token = System.Web.HttpContext.Current.Cache.Get(c.AppID + "_HightToken").ToString();
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="C"></param>
        public HightToken(Config C)
        {
            this.c = C;
            if (System.Web.HttpContext.Current.Cache.Get(c.AppID + "_HightToken") == null)
            {
                lock(iso)
                {
                    if (System.Web.HttpContext.Current.Cache.Get(c.AppID + "_HightToken") == null)
                    {
                        var rs = send();
                        this.Token = rs.access_token;
                    }
                }
            }
            this.Token = System.Web.HttpContext.Current.Cache.Get(C.AppID + "_HightToken").ToString();

        }

        /// <summary>
        /// 该方法在初始化接口时候已经实现(再次调用该方法的时候，强制从微信服务器获取)
        /// </summary>
        /// <returns></returns>
        public HightTokenResponse send()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + c.AppID + "&secret=" + c.SecretKey;
            var rs= send(url);
            if (!string.IsNullOrEmpty(rs.access_token) && rs.access_token != "")
            {
                System.Web.HttpContext.Current.Cache.Add(c.AppID + "_HightToken", rs.access_token, null, DateTime.Now.AddSeconds(Convert.ToInt32(rs.expires_in)-200), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
            }
            return rs;
        }
        protected HightTokenResponse send(string url)
        {
            string json = BaseClass.BaseMethod.WebRequestGet(url, Encoding.Default);
            HightTokenResponse ht = Newtonsoft.Json.JsonConvert.DeserializeObject< HightTokenResponse>(json);
            return ht;
        }
    }

    public class HightTokenResponse : BaseClass.BaseErr, IFace.IResponse
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        public string access_token{set;get;}
        /// <summary>
        /// 凭证有效时间，单位：秒
        /// </summary>
        public int expires_in { set; get; }

    }
}

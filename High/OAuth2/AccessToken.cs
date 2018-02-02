using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.OAuth2
{
    /// <summary>
    /// 获取oauth 的 AccessToken
    /// </summary>
    public class AccessTokenRequest:IFace.IRequest<AccessTokenResponse>
    {
        Config c;
        public string code { set; get; }
        public string appid { set; get; }
        public string secret { set; get; }
        public string grant_type { get { return "authorization_code"; } }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_c"></param>
        /// <param name="_code"></param>
        public AccessTokenRequest(Config _c, string _code)
        {
            this.c = _c;
            this.appid = c.AppID;
            this.secret = c.SecretKey;
            this.code = _code;
        }
        public AccessTokenResponse send()
        {
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?";
            url += BaseClass.BaseMethod.MakeUrl(BaseClass.BaseMethod.MakeToDictionary(this), true, "utf-8");
            return send(url);
        }
        public AccessTokenResponse send(string url)
        {
            string json = BaseClass.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AccessTokenResponse>(json);
        }

        public async Task<AccessTokenResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class AccessTokenResponse:BaseClass.BaseErr, IFace.IResponse
    {
        /// <summary>
        /// 网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
        /// </summary>
        public string access_token { set; get; }
        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        public int expires_in { set; get; }
        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        public string refresh_token { set; get; }
        /// <summary>
        /// 用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔
        /// </summary>
        public string scope { set; get; }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.High
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public partial class User
    {
        string subscribe_;
        /// <summary>
        /// 用户是否订阅该公众号标识(0/1)，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public string subscribe
        {
            set { subscribe_ = value; }
            get { return subscribe_; }
        }
        string openid_;
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid
        {
            set { openid_ = value; }
            get { return openid_; }
        }
        string nickname_;
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname
        {
            set { nickname_ = value; }
            get { return nickname_; }
        }
        int sex_;
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public int sex
        {
            set { sex_ = value; }
            get { return sex_; }
        }
        string city_;
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string city
        {
            set { city_ = value; }
            get { return city_; }
        }
        string country_;
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string country
        {
            set { country_ = value; }
            get { return country_; }
        }
        string province_;
        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string province
        {
            set { province_ = value; }
            get { return province_; }
        }
        string language_;
        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string language
        {
            set { language_ = value; }
            get { return language_; }
        }
        string headimgurl_;
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string headimgurl
        {
            set { headimgurl_ = value; }
            get { return headimgurl_; }
        }
        string avatarUrl_;
        /// <summary>
        /// 微信小程序的用户头像
        /// </summary>
        public string avatarUrl
        {
            set { avatarUrl_ = value; }
            get { return avatarUrl_; }
        }

        long subscribe_time_;
        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间(可以使用ITA.WeiXin.BaseClass.UnixTimeToTime转换为时间)
        /// </summary>
        public long subscribe_time
        {
            set { subscribe_time_ = value; }
            get { return subscribe_time_; }
        }
        string unionid_;
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public string unionid
        {
            set { unionid_ = value; }
            get { return unionid_; }
        }
        /// <summary>
        /// 拉取用户信息（需用户关注）
        /// </summary>
        /// <param name="c"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static User GetUserInfo(Config c, string openid)
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + token.Token + "&openid=" + openid + "&lang=zh_CN";
            string sb = BaseClass.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            JObject json = JObject.Parse(sb);
            if (json["errcode"] != null)
            {
                return new User();
            }
            else
            {
                User u = new User();
                u = (User)Newtonsoft.Json.JsonConvert.DeserializeObject(sb, u.GetType());
                return u;
            }
        }
        /// <summary>
        /// 拉取用户信息(需scope为 snsapi_userinfo 或者 snsapi_login)
        /// </summary>
        /// <param name="Access_token">OAuth2.GetAccessToken 返回的 access_token</param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static User GetUserInfoForUserInfo(string Access_token, string openid)
        {
            string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + Access_token + "&openid=" + openid + "&lang=zh_CN";
            string sb = BaseClass.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            JObject json = JObject.Parse(sb);
            if (json["errcode"] != null)
            {
                return new User();
            }
            else
            {
                User u = new User();
                u = (User)Newtonsoft.Json.JsonConvert.DeserializeObject(sb, u.GetType());
                return u;
            }
        }

    }

    public class UserRequest : IFace.IRequest<UserResponse>
    {
        Config c;
        string openid;
        string Access_token;
        /// <summary>
        /// 拉取用户信息（需用户关注）
        /// </summary>
        /// <param name="_c"></param>
        /// <param name="_openid"></param>
        /// <returns></returns>
        public UserRequest(Config _c, string _openid)
        {
            this.c = _c;
            this.openid = _openid;
        }
        /// <summary>
        /// 拉取用户信息(需scope为 snsapi_userinfo 或者 snsapi_login)
        /// </summary>
        /// <param name="_Access_token">OAuth2.GetAccessToken 返回的 access_token</param>
        /// <param name="_openid"></param>
        /// <returns></returns>
        public UserRequest(string _Access_token, string _openid)
        {
            this.Access_token = _Access_token;
            this.openid = _openid;
        }
        public UserResponse send()
        {
            string url = "";
            if (string.IsNullOrWhiteSpace(this.Access_token))
            {
                HightToken token = new HightToken(c);
                url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + token.Token + "&openid=" + openid + "&lang=zh_CN"; //关注
            }
            else
            {
                url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + Access_token + "&openid=" + openid + "&lang=zh_CN"; //拉伸
            }
            return send(url);
        }
        protected UserResponse send(string url)
        {
            string sb = BaseClass.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserResponse>(sb);
        }
    }
    public class UserResponse : User, IFace.IResponse
    {
       public string errcode { set; get; }
    }
}

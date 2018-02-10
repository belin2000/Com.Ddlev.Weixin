using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.OAuth2
{
    /// <summary>
    /// auth2授权
    /// </summary>
    public class Authorize
    {
        string _appid;

        public string appid
        {
            get { return _appid; }
            set { _appid = value; }
        }
        string _redirect_uri;
        /// <summary>
        /// 重定向地址
        /// </summary>
        public string redirect_uri
        {
            get { return _redirect_uri; }
            set { _redirect_uri = value; }
        }
        string _response_type;
        /// <summary>
        /// 填 code 
        /// </summary>
        public string response_type
        {
            get { return _response_type; }
            set { _response_type = value; }
        }
        string _scope;
        /// <summary>
        /// 应用授权作用域，请使用BaseClass.ScopeType的枚举的字符串来转换(例如:BaseClass.ScopeType.snsapi_base.ToString())
        /// </summary>
        public string scope
        {
            get { return _scope; }
            set { _scope = value; }
        }
        string _state;
        /// <summary>
        /// 重定向后会带上 state 参数，开发者可以填写任意参数值
        /// </summary>
        public string state
        {
            get { return _state; }
            set { _state = value; }
        }
        string _wechat_redirect;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="C"></param>
        /// <param name="Redirect_uri"></param>
        /// <param name="Scope">应用授权作用域,请使用BaseClass.ScopeType的枚举的字符串来转换(例如:BaseClass.ScopeType.snsapi_base.ToString())</param>
        public Authorize(Config C, string Redirect_uri, string Scope)
        {
            this._appid = C.AppID;
            this._redirect_uri = Redirect_uri;
            this._response_type = "code";
            this._scope = Scope;
            this._state = DateTime.Now.ToString("yyyyMMddHHmmss");
            this._wechat_redirect = "wechat_redirect";
        }
        /// <summary>
        /// 生成跳转到授权的url
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public string Get()
        {
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?";
            return Get(url);
        }
        /// <summary>
        /// 生成跳转到授权的url
        /// </summary>
        /// <param name="o"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private string Get(string url)
        {
            SortedDictionary<string, string> dic = BaseClass.BaseMethod.MakeToDictionary(this);
            dic.Remove("wechat_redirect");
            string urlstr = BaseClass.BaseMethod.MakeUrl(dic, true, "UTF-8", 1);
            urlstr += "#" + this._wechat_redirect;
            return url + urlstr;
        }
        /// <summary>
        /// 使用网页版登录微信的
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetWebOauth2()
        {
            string url = "https://open.weixin.qq.com/connect/qrconnect?";
            this.scope = "snsapi_login";
            return Get(url);
        }
    }
}

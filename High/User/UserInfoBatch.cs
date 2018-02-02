using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.User
{
    public class UserInfoBatchRequest : IFace.IRequest<UserInfoBatchResponse>
    {
        Config c;
        /// <summary>
        /// 拉伸列表的用户的openid,每次最多100个
        /// </summary>
        public List<UserInfoItem> user_list { set; get; }
        /// <summary>
        /// 该方法在初始化接口时候已经实现(再次调用该方法的时候，强制从微信服务器获取)
        /// </summary>
        /// <returns></returns>
        protected UserInfoBatchResponse send(string url)
        {
            JsonSerializerSettings st = new JsonSerializerSettings();
            st.NullValueHandling = NullValueHandling.Ignore;
            st.DefaultValueHandling = DefaultValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(this, st);
            string s = BaseClass.BaseMethod.WebRequestPost(json, url, Encoding.UTF8);
            var rs = (UserInfoBatchResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(s, typeof(UserInfoBatchResponse));
            return rs;
        }
        public UserInfoBatchResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=" + token.Token;
            return send(url);
        }

        public async Task<UserInfoBatchResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }

        public UserInfoBatchRequest(Config _c)
        {
            this.c = _c;
        }
    }
    public class UserInfoBatchResponse : BaseClass.BaseResponse, IFace.IResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<User> user_info_list { set; get; }
    }

    public class UserInfoItem
    {
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// 国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语，默认为zh-CN(非必填)
        /// </summary>
        public string lang { set; get; }
    }
}

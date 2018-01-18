using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Tags
{
    /// <summary>
    /// 获取用户身上的标签列表
    /// </summary>
    public class UserTagIDRequest : IFace.IRequest<UserTagIDResponse>
    {
        Config c;
        /// <summary>
        /// 用户的OPENID
        /// </summary>
        public string openid { set; get; }
        protected UserTagIDResponse send(string url)
        {
            JsonSerializerSettings st = new JsonSerializerSettings();
            st.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(this, st);
            string s = BaseClass.BaseMethod.WebRequestPost(json, url, Encoding.UTF8);
            var rs = (UserTagIDResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(s, typeof(UserTagIDResponse));
            return rs;
        }
        public UserTagIDResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/tags/getidlist?access_token=" + token.Token;
            return send(url);
        }
        public UserTagIDRequest(Config _c)
        {
            this.c = _c;
        }
    }
    public class UserTagIDResponse : BaseClass.BaseResponse, IFace.IResponse
    {
        public List<long> tagid_list { set; get; }
    }
}

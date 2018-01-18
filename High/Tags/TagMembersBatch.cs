using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Tags
{
    /// <summary>
    /// 批量为用户取消标签
    /// </summary>
    public class TagMembersBatchUnRequest : IFace.IRequest<TagMembersBatchUnResponse>
    {
        Config c;
        /// <summary>
        /// 粉丝列表
        /// </summary>
        public List<string> openid_list { set; get; }
        /// <summary>
        /// 对应标签的ID
        /// </summary>
        public long tagid { set; get; }
        protected TagMembersBatchUnResponse send(string url)
        {
            JsonSerializerSettings st = new JsonSerializerSettings();
            st.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(this, st);
            string s = BaseClass.BaseMethod.WebRequestPost(json, url, Encoding.UTF8);
            var rs = (TagMembersBatchUnResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(s, typeof(TagMembersBatchUnResponse));
            return rs;
        }
        public TagMembersBatchUnResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchuntagging?access_token=" + token.Token;
            return send(url);
        }
        public TagMembersBatchUnRequest(Config _c)
        {
            this.c = _c;
        }
    }
    public class TagMembersBatchUnResponse : BaseClass.BaseResponse, IFace.IResponse
    { }
}

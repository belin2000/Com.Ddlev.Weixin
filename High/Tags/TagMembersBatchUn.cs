using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Tags
{
    /// <summary>
    /// 批量为用户打标签
    /// </summary>
    public class TagMembersBatchRequest : IFace.IRequest<TagMembersBatchResponse>
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
        protected TagMembersBatchResponse send(string url)
        {
            JsonSerializerSettings st = new JsonSerializerSettings();
            st.NullValueHandling = NullValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(this, st);
            string s = BaseClass.BaseMethod.WebRequestPost(json, url, Encoding.UTF8);
            var rs = (TagMembersBatchResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(s, typeof(TagMembersBatchResponse));
            return rs;
        }
        public TagMembersBatchResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchtagging?access_token=" + token.Token;
            return send(url);
        }
        public TagMembersBatchRequest(Config _c)
        {
            this.c = _c;
        }
    }
    public class TagMembersBatchResponse : BaseClass.BaseResponse, IFace.IResponse
    { }
}

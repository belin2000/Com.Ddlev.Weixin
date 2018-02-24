using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Tags
{
    /// <summary>
    /// 获取标签下粉丝列表
    /// </summary>
    public class TagUserRequest : IFace.IRequest<TagUserResponse>
    {
        Config c;
        /// <summary>
        /// 对应标签的ID
        /// </summary>
        public long tagid { set; get; }
        /// <summary>
        /// 第一个拉取的OPENID，不填默认从头开始拉取
        /// </summary>
        public string next_openid { set; get; }
        protected TagUserResponse send(string url)
        {
            return BaseClass.BaseMethod.send<TagUserResponse>(url, this);
        }
        public TagUserResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/user/tag/get?access_token=" + token.Token;
            return send(url);
        }

        public async Task<TagUserResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }

        public TagUserRequest(Config _c)
        {
            this.c = _c;
        }
    }
    public class TagUserResponse : BaseClass.BaseResponse, IFace.IResponse
    { 
        /// <summary>
        /// 数量
        /// </summary>
        public long count { set; get; }
        /// <summary>
        /// 取列表最后一个用户的openid
        /// </summary>
        public string next_openid { set; get; }
        public TagUser data { set; get; }

    }
    public class TagUser {
        /// <summary>
        /// openid的列表
        /// </summary>
        List<string> openid { set; get; }
    }


}

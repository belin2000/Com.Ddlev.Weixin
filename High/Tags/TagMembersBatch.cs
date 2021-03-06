﻿using Newtonsoft.Json;
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
            return BaseClass.BaseMethod.send<TagMembersBatchUnResponse>(url, this);
        }
        public TagMembersBatchUnResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchuntagging?access_token=" + token.Token;
            return send(url);
        }

        public async Task<TagMembersBatchUnResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }

        public TagMembersBatchUnRequest(Config _c)
        {
            this.c = _c;
        }
    }
    public class TagMembersBatchUnResponse : BaseClass.BaseResponse, IFace.IResponse
    { }
}

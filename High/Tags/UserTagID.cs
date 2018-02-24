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
            return BaseClass.BaseMethod.send<UserTagIDResponse>(url, this);
        }
        public UserTagIDResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/tags/getidlist?access_token=" + token.Token;
            return send(url);
        }

        public async Task<UserTagIDResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
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

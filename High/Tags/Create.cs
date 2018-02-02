using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Tags
{
    public class DeleteRequest : IFace.IRequest<DeleteResponse>
    {
        Config c;
        public TagItem tag { set; get; }
        /// <summary>
        /// 该方法在初始化接口时候已经实现(再次调用该方法的时候，强制从微信服务器获取)
        /// </summary>
        /// <returns></returns>
        protected DeleteResponse send(string url)
        {
            JsonSerializerSettings st = new JsonSerializerSettings();
            st.NullValueHandling = NullValueHandling.Ignore;
            st.DefaultValueHandling = DefaultValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(this,st);
            string s = BaseClass.BaseMethod.WebRequestPost(json, url, Encoding.UTF8);
            var rs = (DeleteResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(s, typeof(DeleteResponse));
            return rs;
        }
        public DeleteResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/tags/delete?access_token=" + token.Token;
            return send(url);
        }

        public async Task<DeleteResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }

        public DeleteRequest(Config _c)
        {
            this.c = _c;
        }
    }


    public class DeleteResponse : BaseClass.BaseResponse, IFace.IResponse
    {
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Wxa
{
    /// <summary>
    /// 小程序获取session_key和用户的openid (code 换取 session_key)
    /// </summary>
    public class SnsscodesessionRequest : IFace.IRequest<SnsscodesessionResponse>
    {
        Config c;
        string code;
        public SnsscodesessionRequest(Config _c,string _code)
        {
            this.c = _c;
            this.code = _code;
        }
        public SnsscodesessionRequest(Config _c)
        {
            this.c = _c;
        }
        public SnsscodesessionResponse send()
        {
            string url = "https://api.weixin.qq.com/sns/jscode2session?appid=" + c.AppID + "&secret=" + c.SecretKey + "&js_code=" + code + "&grant_type=authorization_code";
            var back = Com.Ddlev.Base.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            var ts= Newtonsoft.Json.JsonConvert.DeserializeObject<SnsscodesessionResponse>(back);
            if(string.IsNullOrWhiteSpace( ts.errcode)){
                DataCacheConfig.GetHelper().Set("sessionkey_" + ts.openid, ts.session_key, 8*60*60); //缓存8小时
            }
            return ts;
        }
        




        public async Task<SnsscodesessionResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class SnsscodesessionResponse : IFace.IResponse
    {
        /// <summary>
        /// 错误代码（错误时候出现）
        /// </summary>
        public string errcode { set; get; }
        /// <summary>
        /// 错误信息（错误时候出现）
        /// </summary>
        public string errmsg { set; get; }
        /// <summary>
        /// 用户的openid(成功时候出现)
        /// </summary>
        public string openid { set; get; }
        /// <summary>
        /// 用户的会话密钥(成功时候出现)
        /// </summary>
        public string session_key { set; get; }
        /// <summary>
        /// 用户在开放平台的唯一标识符
        /// </summary>
        public string unionid { set; get; }
    }
}

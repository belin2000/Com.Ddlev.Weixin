using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ddlev.Weixin.BaseClass;

namespace Com.Ddlev.Weixin.High.Wxa
{
    /// <summary>
    /// 小程序内容检测（检查一段文本是否含有违法违规内容）
    /// </summary>
    public class msgSecCheckRequest:IFace.IRequest<msgSecCheckResponse>
    {
        Config c;

        /// <summary>
        /// 要检测的内容
        /// </summary>
        public string content { set; get; }

        /// <summary>
        /// 小程序内容检测(errcode:87014表示有违规内存，0表示正常)
        /// </summary>
        /// <param name="_c">配置</param>
        /// <param name="_content">要检测的内容（不可超过4W字）,建议去掉html或者进行编码</param>
        public msgSecCheckRequest(Config _c,string _content)
        {
            this.c = _c;
            this.content = _content;
        }
        public msgSecCheckResponse send()
        {
            var token = new High.HightToken(c).Token;
            string url = "https://api.weixin.qq.com/wxa/msg_sec_check?access_token=" + token;
            return send(url);
        }
        public msgSecCheckResponse send(string url)
        {
            Dictionary<string, dynamic> dic = new Dictionary<string, dynamic>();
            dic.Add("content", this.content);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<msgSecCheckResponse>(BaseClass.BaseMethod.WebRequestPost(Newtonsoft.Json.JsonConvert.SerializeObject(dic), url, Encoding.UTF8));

        }

        public async Task<msgSecCheckResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class msgSecCheckResponse : Com.Ddlev.Weixin.BaseClass.BaseErr, IFace.IResponse
    { }
}

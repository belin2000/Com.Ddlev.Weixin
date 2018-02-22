using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    public class GetOpenidIDRequest : DeviceItem, IFace.IRequest<GetOpenidIDResponse>
    {
        Config c;


        public GetOpenidIDRequest(Config _c)
        {
            this.c = _c;
        }

        public GetOpenidIDResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/get_openid?access_token=" + token.Token + "&device_type=" + device_type + "&device_id=" + device_id;
            return send(url);
        }
        GetOpenidIDResponse send(string url)
        {
            string s = BaseClass.BaseMethod.WebRequestGet( url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GetOpenidIDResponse>(s);
        }
        public async Task<GetOpenidIDResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class GetOpenidIDResponse : BaseClass.BaseErr, IFace.IResponse
    {
        /// <summary>
        /// openid 的组合
        /// </summary>
        public List<string> open_id { set; get; }
        /// <summary>
        /// 信息
        /// </summary>
        public string resp_msg { set; get; }
    }
}

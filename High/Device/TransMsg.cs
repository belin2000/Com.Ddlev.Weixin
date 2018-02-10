using Com.Ddlev.Weixin.BaseClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    /// <summary>
    /// 主动发送消息给设备(微信硬件)
    /// </summary>
    public class TransMsgRequest:IFace.IRequest<TransMsgResponse>
    {
        Config c;

        /// <summary>
        /// 设备类型，目前为“公众账号原始ID”
        /// </summary>
        public string device_type { set; get; }
        /// <summary>
        /// 设备ID
        /// </summary>
        public string device_id { set; get; }
        /// <summary>
        /// 微信用户账号的openid
        /// </summary>
        public string open_id { set; get; }
        /// <summary>
        /// 消息内容，BASE64编码
        /// </summary>
        public string content { set; get; }
        public TransMsgRequest(Config _c)
        {
            this.c = _c;
        }
        public TransMsgResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/transmsg?access_token=" + token.Token;
            return send(url);
        }
        TransMsgResponse send(string url)
        {
            JsonSerializerSettings jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(this, jSetting);
            string s = BaseClass.BaseMethod.WebRequestPost(json, url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TransMsgResponse>(s);
        }

        public async Task<TransMsgResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class TransMsgResponse : DeviceRet, IFace.IResponse
    { }
}

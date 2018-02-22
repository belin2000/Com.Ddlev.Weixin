using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    public class GetBindDeviceRequest : IFace.IRequest<GetBindDeviceResponse>
    {
        Config c;
        /// <summary>
        /// 设备的deviceid
        /// </summary>
        public string openid { set; get; }
        public GetBindDeviceResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/get_bind_device?access_token=" + token.Token + "&openid=" + openid;
            return send(url);
        }
        GetBindDeviceResponse send(string url)
        {
            string s = BaseClass.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GetBindDeviceResponse>(s);
        }
        public async Task<GetBindDeviceResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class GetBindDeviceResponse : IFace.IResponse
    {
        public BaseClass.BaseErr resp_msg { set; get; }
        public string openid { set; get; }

        public List<DeviceItem> device_list { set; get; }
    }
}

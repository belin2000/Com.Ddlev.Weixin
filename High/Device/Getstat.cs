using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    /// <summary>
    /// 第三方公众账号通过设备id查询该id的状态（三种状态：未授权、已授权、已绑定）。
    /// </summary>
    public class GetStatRequest : IFace.IRequest<GetStatResponse>
    {
        Config c;
        /// <summary>
        /// 设备id
        /// </summary>
        public string device_id { set; get; }
        public GetStatResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/get_stat?access_token=" + token.Token+ "&device_id="+ device_id;
            return send(url);
        }
        GetStatResponse send(string url)
        {
            JsonSerializerSettings jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(this, jSetting);
            string s = BaseClass.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<GetStatResponse>(s);
        }

        public async Task<GetStatResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class GetStatResponse : BaseClass.BaseErr, IFace.IResponse
    {
        /// <summary>
        /// 设备状态，目前取值如下： 0：未授权  1：已经授权（尚未被用户绑定） 2：已经被用户绑定 3：属性未设置
        /// </summary>
        public int status { set; get; }
        /// <summary>
        /// status对应的描述
        /// </summary>
        public string status_info { set; get; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    /// <summary>
    /// 第三方公众账号通过设备id从公众平台批量获取设备二维码。
    /// </summary>

    public class QRCodeRequest : IFace.IRequest<QRCodeResponse>
    {
        Config c;
        /// <summary>
        /// 设备id的个数
        /// </summary>
        public string device_num { set; get; }
        /// <summary>
        /// 设备id的列表，json的array格式，其size必须等于device_num
        /// </summary>
        public List<string> device_id_list { set; get; }

        public QRCodeRequest(Config _c)
        {
            this.c = _c;
        }

        public QRCodeResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/transmsg?access_token=" + token.Token;
            return send(url);
        }
        QRCodeResponse send(string url)
        {
            JsonSerializerSettings jSetting = new JsonSerializerSettings();
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.DefaultValueHandling = DefaultValueHandling.Ignore;
            string json = JsonConvert.SerializeObject(this, jSetting);
            string s = BaseClass.BaseMethod.WebRequestPost(json, url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<QRCodeResponse>(s);
        }
        public async Task<QRCodeResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class QRCodeResponse : BaseClass.BaseResponse, IFace.IResponse
    {
        public int device_num { set; get; }
        public List<QRCodeResponseItem> code_list { set; get; }
    }

    public class QRCodeResponseItem
    {
        public string device_id { set; get; }
        public string ticket { set; get; }
    }
}

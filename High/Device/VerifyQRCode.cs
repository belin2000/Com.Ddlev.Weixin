using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    /// <summary>
    /// 第三方公众账号通过获取设备二维码的api得到ticket后，可以通过该api拿到对应的设备属性。
    /// </summary>
    public class VerifyQRCodeRequest : IFace.IRequest<VerifyQRCodeResponse>
    {
        Config c;
        /// <summary>
        /// 设备二维码的ticket
        /// </summary>
        public string ticket { set; get; }
        public VerifyQRCodeResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/verify_qrcode?access_token=" + token.Token ;
            return send(url);
        }
        VerifyQRCodeResponse send(string url)
        {
            return BaseClass.BaseMethod.send<VerifyQRCodeResponse>(url, this);
        }

        public async Task<VerifyQRCodeResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class VerifyQRCodeResponse : IFace.IResponse
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public string device_id { set; get; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string device_type { set; get; }
        /// <summary>
        /// 设备的mac地址
        /// </summary>
        public string mac { set; get; }
    }
}

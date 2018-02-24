using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    /// <summary>
    /// 第三方控制设备绑定/解绑
    /// </summary>
    public class CompelUnBindRequest : IFace.IRequest<CompelUnBindResponse>
    {
        Config c;
        /// <summary>
        /// 设备id
        /// </summary>
        public string device_id { set; get; }
        /// <summary>
        /// 用户对应的openid
        /// </summary>
        public string openid { set; get; }
        public CompelUnBindRequest(Config _c)
        {
            this.c = _c;
        }
        public CompelUnBindResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/compel_unbind?access_token=" + token.Token;
            return send(url);
        }
        CompelUnBindResponse send(string url)
        {
            return BaseClass.BaseMethod.send<CompelUnBindResponse>(url, this);
        }

        public async Task<CompelUnBindResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class CompelUnBindResponse : IFace.IResponse
    {
        public BaseClass.BaseErr base_resp { set; get; }
    }
}

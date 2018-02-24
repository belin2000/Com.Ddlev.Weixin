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
    public class CompelBindRequest : IFace.IRequest<CompelBindResponse>
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
        public CompelBindRequest(Config _c)
        {
            this.c = _c;
        }
        public CompelBindResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/compel_bind?access_token=" + token.Token;
            return send(url);
        }
        CompelBindResponse send(string url)
        {
            return BaseClass.BaseMethod.send<CompelBindResponse>(url, this);
        }

        public async Task<CompelBindResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class CompelBindResponse : IFace.IResponse
    {
        public BaseClass.BaseErr base_resp { set; get; }
    }
}

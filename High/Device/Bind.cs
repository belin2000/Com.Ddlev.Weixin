using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    /// <summary>
    /// 用户通过第三方H5直连第三方后台绑定：在第三方后台处理完成后,若绑定成功，调用API把绑定的结果推送给公众平台。
    /// </summary>
    public class BindRequest : IFace.IRequest<BindResponse>
    {
        Config c;
        /// <summary>
        /// 绑定操作合法性的凭证（由微信后台生成，第三方H5通过客户端jsapi获得）
        /// </summary>
        public string ticket { set; get; }
        /// <summary>
        /// 设备id
        /// </summary>
        public string device_id { set; get; }
        /// <summary>
        /// 用户对应的openid
        /// </summary>
        public string openid { set; get; }
        public BindRequest(Config _c)
        {
            this.c = _c;
        }
        public BindResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/bind?access_token=" + token.Token;
            return send(url);
        }
        BindResponse send(string url)
        {
            return BaseClass.BaseMethod.send<BindResponse>(url, this);
        }

        public async Task<BindResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class BindResponse : IFace.IResponse
    {
        public BaseClass.BaseErr base_resp { set; get; }
    }
}

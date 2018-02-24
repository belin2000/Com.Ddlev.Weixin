using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    /// <summary>
    /// 用户通过第三方H5直连第三方后台解绑设备：在第三方后台处理完成后,若解绑成功，调用API把解绑的结果推送给公众平台。
    /// </summary>
    public class UnBindRequest : BindRequest, IFace.IRequest<UnBindResponse>
    {
        Config c;
        public UnBindRequest(Config _c):base(_c)
        {
            this.c = _c;
        }
        public new UnBindResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/device/unbind?access_token=" + token.Token;
            return send(url);
        }
        UnBindResponse send(string url)
        {
            return BaseClass.BaseMethod.send<UnBindResponse>(url, this);
        }

        public new  async Task<UnBindResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class UnBindResponse : IFace.IResponse
    {
        public BaseClass.BaseErr base_resp { set; get; }
    }
}

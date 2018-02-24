using Newtonsoft.Json;
using System.Text;
using System;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Menu
{
    /// <summary>
    /// 新建自定义菜单
    /// </summary>
    public class CreateRequest : IFace.IRequest<CreateResponse>
    {
        Config c;
        Menu m;
        /// <summary>
        /// 新建自定义菜单 初始化
        /// </summary>
        /// <param name="_c"></param>
        /// <param name="_m"></param>
        public CreateRequest(Config _c, Menu _m)
        {
            this.m = _m;
            this.c = _c;
        }
        public CreateResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + token.Token;
            return send(url);
        }
        CreateResponse send( string url)
        {
            return BaseClass.BaseMethod.send<CreateResponse>(url, m);
        }

        public async Task<CreateResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }

    public class CreateResponse : BaseClass.BaseResponse, IFace.IResponse
    {
    }
}

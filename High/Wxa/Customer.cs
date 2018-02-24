using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Wxa
{
    public class CustomerRequest :  IFace.IRequest<CustomerResponse>
    {
        Config c;
        dynamic t;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="C"></param>
        /// <param name="T">只能是Com.Ddlev.Weixin.High.CustomerClass的前面是CustomerFor的类(小程序只支出image和text)；</param>
        /// <returns></returns>
        public CustomerRequest(Config C, dynamic T)
        {
            c = C;
            t = T;
        }

        public CustomerResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + token.Token;
            return send(url);
        }
        public CustomerResponse send(string url)
        {
            return BaseClass.BaseMethod.send<CustomerResponse>(url, t);
        }

        public async Task<CustomerResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
}

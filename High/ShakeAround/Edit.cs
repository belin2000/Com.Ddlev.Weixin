using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.ShakeAround
{
    public class EditRequest : ShakeAroundPage, IFace.IRequest<EditResponse>
    {
        Config c;
        /// <summary>
        /// 初始化（初始化后配置属性）
        /// </summary>
        /// <param name="_c"></param>
        public EditRequest(Config _c)
        {
            this.c = _c;
        }
        public EditResponse send()
        {
            var p = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            string url = "https://api.weixin.qq.com/shakearound/page/update?access_token=" + new HightToken(c).Token;
            EditResponse sr = new EditResponse();
            try
            {
                sr = send(url);
            }
            catch
            { }
            return sr;
        }
        EditResponse send(string url)
        {
            return BaseClass.BaseMethod.send<EditResponse>(url, this);
        }

        public async Task<EditResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class EditResponse : ShakeAroundBaseResponse,IFace.IResponse
    { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.ShakeAround
{
    public class AddRequest : ShakeAroundPage, IFace.IRequest<AddResponse>
    {
        Config c;
        /// <summary>
        /// 初始化（初始化后配置属性）
        /// </summary>
        /// <param name="_c"></param>
        public AddRequest(Config _c)
        {
            this.c = _c;
        }
        public AddResponse send()
        {
            this.page_id = 0; //null;
            var p = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            p = p.Replace(",\"page_id\":0", "");

            var sr = new AddResponse();
            try
            {
                sr = (AddResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(Com.Ddlev.Weixin.BaseClass.BaseMethod.WebRequestPost(p, "https://api.weixin.qq.com/shakearound/page/add?access_token=" + new HightToken(c).Token, Encoding.UTF8), typeof(AddResponse));
            }
            catch
            { }
            return sr;
        }
    }
    public class AddResponse : ShakeAroundBaseResponse,IFace.IResponse
    { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            EditResponse sr = new EditResponse();
            try
            {
                sr = (EditResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(Com.Ddlev.Weixin.BaseClass.BaseMethod.WebRequestPost(p, "https://api.weixin.qq.com/shakearound/page/update?access_token=" + new HightToken(c).Token, Encoding.UTF8), typeof(EditResponse));
            }
            catch
            { }
            return sr;
        }
    }
    public class EditResponse : ShakeAroundBaseResponse,IFace.IResponse
    { }
}

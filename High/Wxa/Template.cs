using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Wxa
{
    public class TemplateRequest : High.TemplateRequest, IFace.IRequest<TemplateResponse>
    {

        /// <summary>
        /// 点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转（代替模版中的url）
        /// </summary>
        public string page { set; get; }
        /// <summary>
        /// 表单提交场景下，为 submit 事件带上的 formId；支付场景下，为本次支付的 prepay_id
        /// </summary>
        public string form_id { set; get; }
        /// <summary>
        /// 模板需要放大的关键词，不填则默认无放大(如:keyword1.DATA)
        /// </summary>
        public string emphasis_keyword { set; get; }
        /// <summary>
        /// 初始化。(初始化后需要对属性进行赋值)
        /// </summary>
        /// <param name="_c"></param>
        public TemplateRequest(Config _c):base(_c)
        {
            this.c = _c;
        }
        public new TemplateResponse send()
        {
            var st = new Newtonsoft.Json.JsonSerializerSettings();
            st.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //拼成json
            string paramData = Newtonsoft.Json.JsonConvert.SerializeObject(this, st);
            var str = BaseClass.BaseMethod.WebRequestPost(paramData, "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + (new HightToken(c).Token), System.Text.Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TemplateResponse>(str);
        }
        public new  async Task<TemplateResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
}

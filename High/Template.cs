using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High
{
    /// <summary>
    /// 发送模版信息
    /// </summary>
    public class TemplateRequest:IFace.IRequest<TemplateResponse>
    {
        protected Config c;
        /// <summary>
        /// 对应模版的那些first,keyword1等
        /// </summary>
        public Dictionary<string, TemplateItem> data { set; get; }
        /// <summary>
        /// 发给那个用户的openid
        /// </summary>
        public string touser { set; get; }
        /// <summary>
        /// 点击后跳转到那个页面
        /// </summary>
        public string url { set; get; }
        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据（公众号跳小程序使用）
        /// </summary>
        public BaseClass.MiniProgram miniprogram { set; get; }

        /// <summary>
        /// 微信后台的模版ID
        /// </summary>
        public string template_id { set; get; }
        public string topcolor { set; get; }
        /// <summary>
        /// 初始化（需要设置属性）
        /// </summary>
        /// <param name="_c">微信配置</param>
        public TemplateRequest(Config _c)
        {
            this.c = _c;
        }

        /// <summary>
        /// (发送模版消息)模版消息
        /// </summary>
        public TemplateResponse send()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + (new HightToken(c).Token);
            return send(url);
        }
        TemplateResponse send(string url)
        {
            return BaseClass.BaseMethod.send<TemplateResponse>(url, this);
        }
        public async Task<TemplateResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }

    public class TemplateResponse : IFace.IResponse
    {
        public int errcode{set;get;}
        public string errmsg { set; get; }
        public string msgid { set; get; }
    }

    /// <summary>
    /// 对应的字典的值和颜色
    /// </summary>
    public class TemplateItem
    {
            /// <summary>
            /// 值
            /// </summary>
           public string value { set; get; }
            /// <summary>
            /// 颜色（例如 #ffffff ）
            /// </summary>
           public string color { set; get; }

           public TemplateItem() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_value">对于的keyword1等的值</param>
        /// <param name="_color">显示的颜色</param>
        public TemplateItem(string _value, string _color)
        {
            this.color = _color;
            this.value = _value;
        }
    }
}

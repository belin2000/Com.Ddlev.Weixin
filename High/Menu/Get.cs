using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.Menu
{
    /// <summary>
    /// 获取服务器的菜单
    /// </summary>
    public class GetRequest:IFace.IRequest<GetResponse>
    {
        Config c;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_c"></param>
        public GetRequest(Config _c)
        {
            this.c = _c;
        }

        public GetResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token=" + token.Token;
            string sb = BaseClass.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<GetResponse>(sb);
            return res;
        }
    }
    public class GetResponse : IFace.IResponse
    {
        public int is_menu_open { set; get; }
        public string errcode { set; get; }
        public SelfMenu selfmenu_info { set; get; }
    }

    public class SelfMenu
    {

    }
}

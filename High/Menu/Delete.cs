﻿using System.Text;

namespace Com.Ddlev.Weixin.High.Menu
{
    /// <summary>
    /// 删除自定义菜单
    /// </summary>
    public class DeleteRequest : IFace.IRequest<DeleteResponse>
    {
        Config c;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_c"></param>
        public DeleteRequest(Config _c)
        {
            this.c = _c;
        }
        public DeleteResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=" + token.Token;
            string sb = BaseClass.BaseMethod.WebRequestGet(url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DeleteResponse>(sb);
        }
    }

    public class DeleteResponse : BaseClass.BaseResponse, IFace.IResponse
    {
    }
}
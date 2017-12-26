using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.ShakeAround
{
    public class UploadRequest : IFace.IRequest<UploadResponse>
    {
        Config c;
        string filename;
        /// <summary>
        /// 初始化接口
        /// </summary>
        /// <param name="_c"></param>
        /// <param name="_filename"></param>
        public UploadRequest(Config _c, string _filename)
        {
            this.c = _c;
            this.filename = _filename;
        }
        public UploadResponse send()
        {
            HightToken token = new HightToken(c);
            string posturl = "https://api.weixin.qq.com/shakearound/material/add?access_token=" + token.Token;
            string sb = BaseClass.BaseMethod.UploadFileByWebClient(posturl, filename);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UploadResponse>(sb);
        }
    }
    public class UploadResponse : BaseClass.BaseErr, IFace.IResponse
    {
        public Picdata data { set; get; }
    }
    public class Picdata
    {
        public string pic_url { set; get; }
    }
}

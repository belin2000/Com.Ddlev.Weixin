using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Wxa
{
    public class ImgSecCheckRequest : IFace.IRequest<ImgSecCheckResponse>
    {
        Config c;

        /// <summary>
        /// 图片的真实路径
        /// </summary>
        public string filepath { set; get; }

        public ImgSecCheckRequest(Config _c, string _filepath)
        {
            this.c = _c;
            this.filepath = _filepath;
        }
        public ImgSecCheckResponse send()
        {
            var token = new High.HightToken(c).Token;
            string url = "https://api.weixin.qq.com/wxa/img_sec_check?access_token=" + token;
            return send(url);
        }
        public ImgSecCheckResponse send(string url)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ImgSecCheckResponse>(BaseClass.BaseMethod.UploadFileByWebClient(url, this.filepath));
        }

        public async Task<ImgSecCheckResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class ImgSecCheckResponse : Com.Ddlev.Weixin.BaseClass.BaseErr, IFace.IResponse
    { }
}

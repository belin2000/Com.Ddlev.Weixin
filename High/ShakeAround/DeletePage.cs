using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.ShakeAround
{
    public class DeletePageRequest : IFace.IRequest<DeletePageResponse>
    {
        Config c;
        /// <summary>
        /// 要删除的页面的url
        /// </summary>
        public int PageID { set; get; }

        public DeletePageRequest(Config _c, int _pageid)
        {
            this.c = _c;
            this.PageID = _pageid;
        }

        public DeletePageResponse send()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("page_id", PageID);
            var p = Newtonsoft.Json.JsonConvert.SerializeObject(dic);

            DeletePageResponse sr = new DeletePageResponse();
            try
            {
                sr = (DeletePageResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(BaseClass.BaseMethod.WebRequestPost(p, "https://api.weixin.qq.com/shakearound/page/delete?access_token=" + new HightToken(c).Token, Encoding.UTF8), typeof(DeletePageResponse));
            }
            catch
            { }
            return sr;
        }

        public async Task<DeletePageResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class DeletePageResponse : ShakeAroundBaseResponse, IFace.IResponse
    { }
}

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
            string url = "https://api.weixin.qq.com/shakearound/page/delete?access_token=" + new HightToken(c).Token;
            DeletePageResponse sr = new DeletePageResponse();
            try
            {
                return BaseClass.BaseMethod.send<DeletePageResponse>(url, dic);
            }
            catch
            { }
            return sr;
        }
        DeletePageResponse send(string url)
        {
            return BaseClass.BaseMethod.send<DeletePageResponse>(url, this);
        }

        public async Task<DeletePageResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class DeletePageResponse : ShakeAroundBaseResponse, IFace.IResponse
    { }
}

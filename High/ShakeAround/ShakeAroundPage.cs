using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.ShakeAround
{
    /// <summary>
    /// 基础属性
    /// </summary>
    public class ShakeAroundPage
    {
        /// <summary>
        /// 在摇一摇页面展示的主标题，不超过6个汉字或12个英文字母
        /// </summary>
        public string title { set; get; }
        /// <summary>
        /// 在摇一摇页面展示的副标题，不超过7个汉字或14个英文字母
        /// </summary>
        public string description { set; get; }
        /// <summary>
        /// 页面连接的h5 网址
        /// </summary>
        public string page_url { set; get; }
        /// <summary>
        /// 说明，不超过15个字
        /// </summary>
        public string comment { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string icon_url { set; get; }

        public int page_id { set; get; }
    }
    public class ShakeAroundBaseResponse
    {
        public Page_id data { set; get; }
        /// <summary>
        /// 0就是
        /// </summary>
        public int errcode { set; get; }
        /// <summary>
        /// success就是
        /// </summary>
        public string errmsg { set; get; }
    }
    public class Page_id
    {
        public int page_id;
    }
}

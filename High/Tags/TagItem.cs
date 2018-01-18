using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Tags
{
    public class TagItem
    {
        /// <summary>
        /// 标签名称(删除请设置为null)
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 标签id，由微信分配(新增请设置为null)
        /// </summary>
        public long id { set; get; }
        public TagItem(long _id = 0, string _name = null)
        {
            id = _id;
            name = _name;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Pay.PayBase
{
    public class OrderParaComm
    {
        protected Config c;
        /// <summary>
        /// 公众号 id,可在配置获取
        /// </summary>
        public string appId { set; get; }
        /// <summary>
        /// 时间戳，使用BaseMethod.ConvertDateTimeInt获取
        /// </summary>
        public string timeStamp { set; get; }
        /// <summary>
        /// 随机字符串（32 个字节以下）
        /// </summary>
        public string nonceStr { set; get; }
        /// <summary>
        /// 订单详情扩展字符串
        /// </summary>
        public string package { set; get; }
    }
}

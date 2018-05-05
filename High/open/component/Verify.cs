using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.open.component
{
    /// <summary>
    /// 第三方平台授权
    /// </summary>
    public class Verify
    {
        /// <summary>
        /// 第三方平台appid (授权给我方处理的appid)
        /// </summary>
        public string AppId { set; get; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string CreateTime { set; get; }
        /// <summary>
        /// 默认值：component_verify_ticket
        /// </summary>
        public string InfoType { set; get; }
        /// <summary>
        /// Ticket内容
        /// </summary>
        public string ComponentVerifyTicket { set; get; }

        /// <summary>
        /// 解密数据并赋值
        /// </summary>
        /// <param name="Encrypt"></param>
        public Verify(string Encrypt,Config c )
        {

        }
    }
}

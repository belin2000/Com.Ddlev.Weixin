using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.IFace
{
    public interface IWechatMessage
    {
        /// <summary>
        /// 接收方微信号 
        /// </summary>
        string ToUserName { set; get; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        string FromUserName { set; get; }
        /// <summary>
        /// 消息创建时间 （整型）,使用 （unix时间，该时间是从1970/1/1起计数的秒数）
        /// </summary>
        long CreateTime { set; get; }
        /// <summary>
        /// 信息类型
        /// </summary>
        BaseClass.WeiXinMsgType MsgType { set; get; }
        /// <summary>
        /// 企业号的应用ID
        /// </summary>
        string AgentID { set; get; }
    }
}

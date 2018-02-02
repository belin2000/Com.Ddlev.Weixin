using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.BaseClass
{
    /// <summary>
    /// 微信公共平台公用类
    /// </summary>
    public class WeixinBase
    {
        string _ToUserName;
        /// <summary>
        /// 接收方微信号 
        /// </summary>
        public string ToUserName
        {
            get { return _ToUserName; }
            set { _ToUserName = value; }
        }
        string _FromUserName;
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName
        {
            get { return _FromUserName; }
            set { _FromUserName = value; }
        }
        long _CreateTime;
        /// <summary>
        /// 消息创建时间 （整型）,使用 （unix时间，该时间是从1970/1/1起计数的秒数）
        /// </summary>
        public long CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        WeiXinMsgType _MsgType;
        /// <summary>
        /// 信息类型
        /// </summary>
        public WeiXinMsgType MsgType
        {
            get { return _MsgType; }
            set { _MsgType = value; }
        }
        string _AgentID;
        /// <summary>
        /// 企业号的应用ID
        /// </summary>
        public string AgentID
        {
            get { return _AgentID; }
            set { _AgentID = value; }
        }
        public WeixinBase()
        {
            this.CreateTime = BaseClass.BaseMethod.ConvertDateTimeInt(DateTime.Now);
        }
    }
    /// <summary>
    /// 信息类型
    /// </summary>
    public enum WeiXinMsgType
    {
        /// <summary>
        /// 文本信息
        /// </summary>
        _text=0,
        /// <summary>
        /// 图文信息
        /// </summary>
        _image,
        /// <summary>
        /// 地理位置信息
        /// </summary>
        _location,
        /// <summary>
        /// 音乐信息
        /// </summary>
        _music,
        /// <summary>
        /// 图文信息
        /// </summary>
        _news,
        /// <summary>
        /// 连接信息
        /// </summary>
        _link,
        /// <summary>
        /// 语音
        /// </summary>
        _voice,
        /// <summary>
        /// 事件信息
        /// </summary>
        _event,
        /// <summary>
        /// 文件信息（企业号可用）
        /// </summary>
        _file,
        /// <summary>
        /// 保存在微信后台的图文信息(企业号)
        /// </summary>
        _mpnews
    }

    public class EncryptBase
    {
        string _Encrypt;
        /// <summary>
        /// 密码体
        /// </summary>
        public string Encrypt
        {
            get { return _Encrypt; }
            set { _Encrypt = value; }
        }
        string _ToUserName;
        /// <summary>
        /// 回复的用户（仅仅接收信息的时候有）
        /// </summary>
        public string ToUserName
        {
            get { return _ToUserName; }
            set { _ToUserName = value; }
        }
        string _MsgSignature;
        /// <summary>
        /// 签名
        /// </summary>
        public string MsgSignature
        {
            get { return _MsgSignature; }
            set { _MsgSignature = value; }
        }

        long _TimeStamp;
        /// <summary>
        /// 当前时间
        /// </summary>
        public long TimeStamp
        {
            get { return _TimeStamp; }
            set { _TimeStamp = value; }
        }
        string _Nonce;
        /// <summary>
        /// 随机数
        /// </summary>
        public string Nonce
        {
            get { return _Nonce; }
            set { _Nonce = value; }
        }
        public EncryptBase()
        {
            this.ToUserName = "";
            this.TimeStamp = BaseMethod.ConvertDateTimeInt(DateTime.Now);
            this.Nonce = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }

    public enum ScopeType
    {
        /// <summary>
        /// (微信)不弹出授权页面，直接跳转，这个只能拿到用户openid
        /// </summary>
        snsapi_base = 1,
        /// <summary>
        /// (微信)弹出授权页面，这个可以通过 openid 拿到昵称、性别、所在地
        /// </summary>
        snsapi_userinfo = 2,
        /// <summary>
        /// (用于web网站登录)弹出授权页面，这个可以通过 openid 拿到昵称、性别、所在地
        /// </summary>
        snsapi_login = 3
    }

    /// <summary>
    /// 配置小程序的appid和页面（用于信息推送，菜单和模版）
    /// </summary>
    public class MiniProgram
    {
        /// <summary>
        /// 小程序的appid （该小程序appid必须与发模板消息的公众号是绑定关联关系）
        /// </summary>
        public string appid { set; get; }
        /// <summary>
        /// 所需跳转到小程序的具体页面路径，支持带参数,（示例index?foo=bar）
        /// </summary>
        public string pagepath { set; get; }

        public MiniProgram(string _appid="",string _pagepath="")
        {
            this.appid = _appid;
            this.pagepath = _pagepath;
        }
    }
}

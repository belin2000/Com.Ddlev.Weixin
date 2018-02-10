using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.BaseClass
{
    /// <summary>
    /// 文本信息(接收和发送)
    /// </summary>
    public class MsgPropertiesForText : WeixinBase
    {
        string _Content;
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }
        int _FuncFlag;
        /// <summary>
        /// 星标刚收到的消息。(默认为0)[发送时有效]
        /// </summary>
        public int FuncFlag
        {
            get { return _FuncFlag; }
            set { _FuncFlag = value; }
        }
        public MsgPropertiesForText()
        {
            this.FuncFlag = 0;
            this.MsgType = WeiXinMsgType._text;
        }
    }
    /// <summary>
    /// 图片信息(接收)
    /// </summary>
    public class MsgPropertiesForImage : WeixinBase
    {
        string _PicUrl;
        /// <summary>
        /// 图片链接([接收时有效])
        /// </summary>
        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }
        string _MediaId;
        /// <summary>
        /// 图片媒体文件id，可以调用获取媒体文件接口拉取数据
        /// </summary>
        public string MediaId
        {
            get { return _MediaId; }
            set { _MediaId = value; }
        }
    }
    /// <summary>
    /// 图文信息(发送)
    /// </summary>
    public class MsgPropertiesForNews : WeixinBase
    {
        //int _FuncFlag;
        ///// <summary>
        ///// 星标刚收到的消息。(默认为0)[发送时有效]
        ///// </summary>
        //public int FuncFlag
        //{
        //    get { return _FuncFlag; }
        //    set { _FuncFlag = value; }
        //}

        int _ArticleCount;
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount
        {
            get { return _ArticleCount; }
            set { _ArticleCount = value; }
        }
        List<Article> _Articles;
        /// <summary>
        /// 图文信息(集合)
        /// </summary>
        public List<Article> Articles
        {
            get { return _Articles; }
            set { _Articles = value; }
        }
        public MsgPropertiesForNews()
        {
            this.MsgType = WeiXinMsgType._news;
        }
    }
    /// <summary>
    /// 多条图文消息信息，默认第一个item为大图
    /// </summary>
    public class Article
    {
        string _Title;
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        string _Description;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        string _PicUrl;
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。
        /// </summary>
        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }
        string _Url;
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
        public Article() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Title_">标题</param>
        /// <param name="Description_">内容</param>
        /// <param name="PicUrl_">图片地址</param>
        /// <param name="Url_">连接地址</param>
        public Article(string Title_, string Description_, string PicUrl_,string Url_)
        {
            _Title=Title_;
            _Description = Description_;
            _PicUrl = PicUrl_;
            _Url = Url_;
        }
    }
    /// <summary>
    /// 音乐信息(发送)
    /// </summary>
    public class MsgPropertiesForMusic : WeixinBase
    {
        int _FuncFlag;
        /// <summary>
        /// 星标刚收到的消息。(默认为0)[发送时有效]
        /// </summary>
        public int FuncFlag
        {
            get { return _FuncFlag; }
            set { _FuncFlag = value; }
        }
        MusicItem _Music;
        /// <summary>
        /// 音乐信息
        /// </summary>
        public MusicItem Music
        {
            get { return _Music; }
            set { _Music = value; }
        }
        public MsgPropertiesForMusic()
        {
            this.MsgType = WeiXinMsgType._music;

        }
    }
    /// <summary>
    /// 音乐信息基类
    /// </summary>
    public class MusicItem
    {
        string _Title;
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        string _Description;
        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        string _MusicUrl;
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicUrl
        {
            get { return _MusicUrl; }
            set { _MusicUrl = value; }
        }
        string _HQMusicUrl;
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl
        {
            get { return _HQMusicUrl; }
            set { _HQMusicUrl = value; }
        }
    }
    /// <summary>
    /// 地理位置类（接收）
    /// </summary>
    public class MsgPropertiesForLocation : WeixinBase
    {
        float _Location_X;
        /// <summary>
        /// 纬度
        /// </summary>
        public float Location_X
        {
            get { return _Location_X; }
            set { _Location_X = value; }
        }

        float _Location_Y;
        /// <summary>
        /// 经度
        /// </summary>
        public float Location_Y
        {
            get { return _Location_Y; }
            set { _Location_Y = value; }
        }
        int _Scale;
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }
        string _Label;
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label
        {
            get { return _Label; }
            set { _Label = value; }
        }
        public MsgPropertiesForLocation()
        {
            this.MsgType = WeiXinMsgType._location;
        }
    }
    /// <summary>
    /// 连接信息类（接收）
    /// </summary>
    public class MsgPropertiesForLink : WeixinBase
    {
        string _Title;
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        string _Description;
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        string _Url;
        /// <summary>
        /// 消息连接
        /// </summary>
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }
    }
    /// <summary>
    /// 事件模式(订阅，自定义菜单)
    /// </summary>
    public class MsgPropertiesForEvent : WeixinBase
    {
        string _Event;
        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)、CLICK(自定义菜单点击事件),LOCATION(上报地理位置)，SCAN（扫描事件）
        /// </summary>
        public string Event
        {
            get { return _Event; }
            set { _Event = value; }
        }
        string _EventKey;
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应,qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey
        {
            get { return _EventKey; }
            set { _EventKey = value; }
        }
        decimal _Latitude;
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public decimal Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
        
        decimal _Longitude;
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public decimal Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }

        decimal _Precision;

        public decimal Precision
        {
            get { return _Precision; }
            set { _Precision = value; }
        }

    }
    /// <summary>
    /// 语音模式
    /// </summary>
    public class MsgPropertiesForVoice : WeixinBase
    {
        string _MediaId;

        public string MediaId
        {
            get { return _MediaId; }
            set { _MediaId = value; }
        }
        string _Format;
        /// <summary>
        /// 格式
        /// </summary>
        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }
        string _Recognition;
        public string Recognition
        {
            set{_Recognition=value;}
            get{return _Recognition;}
        }
        
    }

    /// <summary>
    /// 设备解绑和绑定
    /// </summary>
    public class MsgPropertiesForDevice_event : WeixinBase
    {
        public string DeviceType { set; get; }
        public string DeviceID { set; get; }
        public string Content { set; get; }
        public string SessionID { set; get; }
        public string MsgID { set; get; }
        public string OpenID { set; get; }
        public MsgPropertiesForDevice_event()
        {
            this.MsgType = WeiXinMsgType._device_event;
        }
    }
    /// <summary>
    /// 设备推送信息
    /// </summary>
    public class MsgPropertiesForDevice_text : MsgPropertiesForDevice_event
    {
        public MsgPropertiesForDevice_text()
        {
            this.MsgType = WeiXinMsgType._device_text;
        }
    }

}

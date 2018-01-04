using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;


namespace Com.Ddlev.Weixin.High
{
    /// <summary>
    /// 客服回复信息功能
    /// </summary>
    public class CustomerRequest : IFace.IRequest<CustomerResponse>
    {
        Config c;
        dynamic t;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="C"></param>
        /// <param name="T">只能是Com.Ddlev.Weixin.High.CustomerClass的前面是CustomerFor的类</param>
        /// <returns></returns>
        public CustomerRequest(Config C, dynamic T)
        {
            c = C;
            t = T;
        }

        public CustomerResponse send()
        {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + token.Token;
            return send(url);
        }
        public CustomerResponse send(string url)
        {
            string st = Newtonsoft.Json.JsonConvert.SerializeObject(t);
            string sb = BaseClass.BaseMethod.WebRequestPost(st, url, Encoding.UTF8);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerResponse>(sb);
        }
    }
    public class CustomerResponse : BaseClass.BaseResponse, IFace.IResponse
    {
        
    }
}
namespace Com.Ddlev.Weixin.High.CustomerClass.Base
{
    /// <summary>
    /// 基础信息类(用不上的列)
    /// </summary>
    public class CustomerBase
    {
        string _touser;
        /// <summary>
        /// 普通用户openid
        /// </summary>
        public string touser 
        { 
            set { _touser = value; } 
            get { return _touser; } 
        }
        string _msgtype;
        /// <summary>
        /// 消息类型:text/image/voice/video/music/news/mpnews/wxcard/
        /// </summary>
        public string msgtype 
        { 
            set { _msgtype = value; } 
            get { return _msgtype; } 
        }
    }
}

namespace Com.Ddlev.Weixin.High.CustomerClass
{
    
    /// <summary>
    /// 文本信息类
    /// </summary>
    public class CustomerFortext : Base.CustomerBase
    {
        ForText _text;
        public ForText text 
        { 
            set { _text = value; } 
            get { return _text; } 
        }
    }
    public class ForText {
        string _content;
        /// <summary>
        /// 文本消息内容(点击连接打开小程序页面 <a href="#" data-miniprogram-appid="appid" data-miniprogram-path="pages/index/index">点击跳小程序</a>)
        /// </summary>
        public string content 
        { 
            set { _content = value; } 
            get { return _content; } 
        }
        public ForText() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_content">文本消息内容</param>
        public ForText(string _content)
        {
            this.content = _content;
        }
    }

    /// <summary>
    /// 图片信息类
    /// </summary>
    public class CustomerForimage : Base.CustomerBase
    {
        ForImage _ForImage;
        public ForImage image
        { 
            set { _ForImage = value; } 
            get { return _ForImage; } 
        }
    }
    /// <summary>
    /// 媒体基础类
    /// </summary>
    public class ForMedia { 
        string _media_id;
        /// <summary>
        /// 发送的媒体ID
        /// </summary>
        public string media_id 
        { 
            set { _media_id = value; } 
            get { return _media_id; }
        }
        public ForMedia() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_media_id">发送的图片的媒体ID</param>
        public ForMedia(string _media_id)
        {
            this.media_id = _media_id;
        }
    }
    public class ForImage : ForMedia
    {
        public ForImage() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_media_id">发送的图片的媒体ID</param>
        public ForImage(string _media_id):base(_media_id)
        {
            
        }
    }

    /// <summary>
    /// 语音信息类
    /// </summary>
    public class CustomerForvoice : Base.CustomerBase
    {
        ForVoice _voice;
        public ForVoice voice 
        { 
            set { _voice = value; } 
            get { return _voice; } 
        }
    }
    public class ForVoice : ForMedia
    {
        
        public ForVoice() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_media_id">发送的语音的媒体ID</param>
        public ForVoice(string _media_id):base(_media_id)
        {
        }
    }

    /// <summary>
    ///  视频信息类
    /// </summary>
    public class CustomerForvideo : Base.CustomerBase
    {
        ForVideo _video;
        public ForVideo video 
        { 
            set { _video = value; } 
            get { return _video; } 
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ForVideo : ForMedia
    {
        
        string _title;
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string title 
        { 
            set { _title = value; } 
            get { return _title; } 
        }
        string _description;
        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string description 
        { 
            set { _description = value; } 
            get { return _description; } 
        }
        public ForVideo() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_media_id">发送的视频的媒体ID</param>
        /// <param name="_description">视频消息的描述</param>
        /// <param name="_title">视频消息的标题</param>
        public ForVideo(string _media_id, string _description, string _title):base(_media_id)
        {
            this.description = _description;
            this.title = _title;
        }
    }

    /// <summary>
    ///  视频信息类
    /// </summary>
    public class CustomerFormusic : Base.CustomerBase
    {
        ForMusic _ForMusic;
        public ForMusic music 
        { 
            set { _ForMusic = value; } 
            get { return _ForMusic; } 
        }
    }
    public class ForMusic
    {
        string _thumb_media_id;
        /// <summary>
        /// 缩略图的媒体ID
        /// </summary>
        public string thumb_media_id 
        { 
            set { _thumb_media_id = value; } 
            get { return _thumb_media_id; } 
        }
        string _title;
        /// <summary>
        /// 音乐标题
        /// </summary>
        public string title 
        { 
            set { _title = value; }
            get { return _title; } 
        }
        string _description;
        /// <summary>
        /// 音乐描述
        /// </summary>
        public string description 
        { 
            set { _description = value; } 
            get { return _description; }
        }
        string _musicurl;
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string musicurl 
        { 
            set { _musicurl = value; }
            get { return _musicurl; } 
        }
        string _hqmusicurl;
        /// <summary>
        /// 高品质音乐链接，wifi环境优先使用该链接播放音乐
        /// </summary>
        public string hqmusicurl 
        { 
            set { _hqmusicurl = value; } 
            get { return _hqmusicurl; }
        }
        public ForMusic() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_thumb_media_id">缩略图的媒体ID</param>
        /// <param name="_description">音乐描述</param>
        /// <param name="_title">音乐标题</param>
        /// <param name="_hqmusicurl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="_musicurl">音乐链接</param>
        public ForMusic(string _thumb_media_id, string _description, string _title, string _musicurl, string _hqmusicurl)
        {
            this.thumb_media_id = _thumb_media_id;
            this.description = _description;
            this.title = _title;
            this.hqmusicurl = _hqmusicurl;
            this.musicurl = _musicurl;
        }
    }

    /// <summary>
    /// 图文信息类
    /// </summary>
    public class CustomerFornews : Base.CustomerBase
    {
        ForNews _ForNews;
        public ForNews news 
        { 
            set { _ForNews = value; } 
            get { return _ForNews; }
        }
    }
    public class ForNews
    {
        List<ForArticle> _articles;
        public List<ForArticle> articles 
        { 
            set { _articles = value; } 
            get { return _articles; }
        }
    }
    /// <summary>
    /// 单个图文的类
    /// </summary>
    public class ForArticle
    {
        string _title;
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        string _description;
        /// <summary>
        /// 描述
        /// </summary>
        public string description
        { 
            set { _description = value; }
            get { return _description; }
        }
        string _url;
        /// <summary>
        /// 点击后跳转的链接
        /// </summary>
        public string url 
        {
            set { _url = value; } 
            get { return _url; } 
        }
        string _picurl;
        /// <summary>
        /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
        /// </summary>
        public string picurl 
        { 
            set { _picurl = value; }
            get { return _picurl; }
        }
    }

}

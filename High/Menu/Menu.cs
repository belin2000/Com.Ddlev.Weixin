using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.High.Menu
{
    /// <summary>
    /// 自定义菜单
    /// （目前自定义菜单最多包括3个一级菜单，每个一级菜单最多包含5个二级菜单）
    /// </summary>
    public class Menu
    {
        public List<IMenuItem> button
        {
            set; get;
        }
        public Menu()
        {

        }
        public Menu(List<IMenuItem> _button)
        {
            this.button = _button;
        }
    }

    /// <summary>
    /// 菜单的功能type
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// 默认（为空）
        /// </summary>
        Default = 0,
        /// <summary>
        /// 点击推事件用户点击click类型按钮后，微信服务器会通过消息接口推送消息类型为event的结构给开发者（参考消息接口指南），并且带上按钮中开发者填写的key值，开发者可以通过自定义的key值与用户进行交互
        /// </summary>
        click = 1,
        /// <summary>
        /// 跳转URL用户点击view类型按钮后，微信客户端将会打开开发者在按钮中填写的网页URL，可与网页授权获取用户基本信息接口结合，获得用户基本信息。
        /// </summary>
        view = 2,
        /// <summary>
        /// 扫码推事件用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后显示扫描结果（如果是URL，将进入URL），且会将扫码的结果传给开发者，开发者可以下发消息。
        /// </summary>
        scancode_push = 3,
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后，将扫码的结果传给开发者，同时收起扫一扫工具，然后弹出“消息接收中”提示框，随后可能会收到开发者下发的消息。
        /// </summary>
        scancode_waitmsg = 4,
        /// <summary>
        /// 弹出系统拍照发图用户点击按钮后，微信客户端将调起系统相机，完成拍照操作后，会将拍摄的相片发送给开发者，并推送事件给开发者，同时收起系统相机，随后可能会收到开发者下发的消息。
        /// </summary>
        pic_sysphoto = 5,
        /// <summary>
        /// 弹出拍照或者相册发图用户点击按钮后，微信客户端将弹出选择器供用户选择“拍照”或者“从手机相册选择”。用户选择后即走其他两种流程
        /// </summary>
        pic_photo_or_album = 6,
        /// <summary>
        /// 弹出微信相册发图器用户点击按钮后，微信客户端将调起微信相册，完成选择操作后，将选择的相片发送给开发者的服务器，并推送事件给开发者，同时收起相册，随后可能会收到开发者下发的消息
        /// </summary>
        pic_weixin = 7,
        /// <summary>
        /// 弹出地理位置选择器用户点击按钮后，微信客户端将调起地理位置选择工具，完成选择操作后，将选择的地理位置发送给开发者的服务器，同时收起位置选择工具，随后可能会收到开发者下发的消息
        /// </summary>
        location_select = 8,
        /// <summary>
        /// 下发消息（除文本消息）用户点击media_id类型按钮后，微信服务器会将开发者填写的永久素材id对应的素材下发给用户，永久素材类型可以是图片、音频、视频、图文消息。请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id
        /// </summary>
        media_id = 9,
        /// <summary>
        /// 跳转图文消息URL用户点击view_limited类型按钮后，微信客户端将打开开发者在按钮中填写的永久素材id对应的图文消息URL，永久素材类型只支持图文消息。请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id
        /// </summary>
        view_limited = 10,
        /// <summary>
        /// 跳转到小程序
        /// </summary>
        miniprogram = 11
    }

    public interface IMenuItem
    {

    }
    public class MenuItemBase: IMenuItem
    {
        [JsonIgnore]
        public MenuType mtype { set; get; }
        string _type = null;
        /// <summary>
        /// 菜单的响应动作类型(不支持直接赋值，只能从mtype类型选择)，目前有click、view,scancode_push,scancode_waitmsg,pic_sysphoto,pic_photo_or_album,pic_weixin,location_select,media_id,view_limited,miniprogram类型
        /// </summary>
        public string type
        {
            get { return _type; }
        }
        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name
        {
            set; get;
        }
        /// <summary>
        /// 二级菜单数组，个数应为1~5个
        /// </summary>
        public List<IMenuItem> sub_button
        {
            set; get;
        }

        public MenuItemBase(string _name, MenuType _mtype= MenuType.Default, List<IMenuItem> _sub_button=null) {
            this.name = _name;
            _type = _mtype == MenuType.Default ? null : _mtype.ToString();
            this.sub_button = _sub_button;
        }
    }

    /// <summary>
    /// 用于点击事件（click,scancode_push,scancode_waitmsg,pic_sysphoto,pic_photo_or_album,pic_weixin,location_select）
    /// </summary>
    public class MenuItemForClick : MenuItemBase, IMenuItem
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { set; get; }
        public MenuItemForClick(string _name, string _key, MenuType _miniprogram,  List<IMenuItem> _sub_button=null) : base(_name, _miniprogram, _sub_button)
        {
            this.key = _key;
        }
    }
    /// <summary>
    /// 用于点击事件获取永久素材（media_id,view_limited）
    /// </summary>
    public class MenuItemForMedia : MenuItemBase, IMenuItem
    {
        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key { set; get; }
        /// <summary>
        /// 调用新增永久素材接口返回的合法media_id
        /// </summary>
        public string media_id { set; get; }
        public MenuItemForMedia(string _name, string _key,string _media_id, MenuType _miniprogram, List<IMenuItem> _sub_button=null) : base(_name, _miniprogram, _sub_button)
        {
            this.key = _key;
            this.media_id = _media_id;
        }

    }
    /// <summary>
    /// 跳转URL用户点击view类型按钮后，微信客户端将会打开开发者在按钮中填写的网页URL，可与网页授权获取用户基本信息接口结合，获得用户基本信息
    /// </summary>
    public class MenuItemForView : MenuItemBase, IMenuItem
    {
        /// <summary>
        /// 网页 链接，用户点击菜单可打开链接，不超过1024字节。 
        /// </summary>
        public string url { set; get; }
        public MenuItemForView(string _name, string _url, List<IMenuItem> _sub_button=null) : base(_name, MenuType.view, _sub_button)
        {
            this.url = _url;
        }
    }
    /// <summary>
    /// 打开小程序
    /// </summary>
    public class MenuItemForMiniprogram : MenuItemBase, IMenuItem
    {
        /// <summary>
        /// 网页 链接，用户点击菜单可打开链接，不超过1024字节。 type为miniprogram时，不支持小程序的老版本客户端将打开本url。
        /// </summary>
        public string url { set; get; }
        /// <summary>
        /// 小程序的appid（仅认证公众号可配置） [miniprogram类型必须]
        /// </summary>
        public string appid { set; get; }
        /// <summary>
        /// 小程序的页面路径 [miniprogram类型必须]
        /// </summary>
        public string pagepath { set; get; }

        public MenuItemForMiniprogram(string _name, string _url, string _appid,string _pagepath, List<IMenuItem> _sub_button=null) :base(_name, MenuType.miniprogram,_sub_button) {
            this.url = _url;
            this.appid = _appid;
            this.pagepath = _pagepath;
        }
    }
}

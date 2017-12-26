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
        List<MenuItem> button_;
        /// <summary>
        /// 一级菜单数组，个数应为1~3个
        /// </summary>
        public List<MenuItem> button 
        { 
            set { button_ = value; } 
            get { return button_; } 
        }
        public Menu() {

        }
        public Menu(List<MenuItem> _button)
        {
            this.button = _button;
        }
    }





    internal class Menuclass
    {
        Menu menu_;
        public Menu menu { 
            set { menu_ = value; } 
            get { return menu_; } 
        }
    }

    public class MenuItem
    {
        string type_;
        /// <summary>
        /// 菜单的响应动作类型，目前有click、view两种类型
        /// </summary>
        public string type
        {
            set{type_=value;}
            get{return type_;}
        }
        string name_;
        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name 
        { 
            set { name_ = value; }
            get { return name_; }
        }
        string key_;
        /// <summary>
        /// (click类型必须) 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        public string key 
        { 
            set { key_ = value; }
            get { return key_; } 
        }
        string url_;
        /// <summary>
        /// (view类型必须)网页链接，用户点击菜单可打开链接，不超过256字节(必须是完整的网址，带Http的)
        /// </summary>
        public string url 
        { 
            set { url_ = value; }
            get { return url_; }
        }
        List<MenuItem> sub_button_;
        /// <summary>
        /// 二级菜单数组，个数应为1~5个
        /// </summary>
        public List<MenuItem> sub_button
        {
            set { sub_button_ = value; }
            get { return sub_button_; }
        }
        public MenuItem() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type">菜单的响应动作类型，目前有click、view两种类型</param>
        /// <param name="_name">菜单标题，不超过16个字节，子菜单不超过40个字节</param>
        /// <param name="_key">(click类型必须) 菜单KEY值，用于消息接口推送，不超过128字节</param>
        /// <param name="_url">(view类型必须)网页链接，用户点击菜单可打开链接，不超过256字节(必须是完整的网址，带Http的)</param>
        public MenuItem(string _type, string _name, string _key, string _url) {
            if (!string.IsNullOrEmpty(_key) && _key!="")
            {
                this.key = _key;
            }
            else
            {
                this.key = null;
            }
            if (!string.IsNullOrEmpty(_name) && _name != "")
            {
                this.name = _name;
            }
            else
            {
                this.name = null;
            }
            if (!string.IsNullOrEmpty(_type) && _type != "")
            {
                this.type = _type;
            }
            else
            {
                this.type = null;
            }

            if (!string.IsNullOrEmpty(_url) && _url != "")
            {
                this.url = _url;
            }
            else
            {
                this.url = null;
            }
            this.sub_button = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_type">菜单的响应动作类型，目前有click、view两种类型</param>
        /// <param name="_name">菜单标题，不超过16个字节，子菜单不超过40个字节</param>
        /// <param name="_key">(click类型必须) 菜单KEY值，用于消息接口推送，不超过128字节</param>
        /// <param name="_url">(view类型必须)网页链接，用户点击菜单可打开链接，不超过256字节(必须是完整的网址，带Http的)</param>
        public MenuItem(string _type, string _name, string _key, string _url, MenuItem[] _sub_button)
        {
            if (!string.IsNullOrEmpty(_key) && _key != "")
            {
                this.key = _key;
            }
            else
            {
                this.key = null;
            }
            if (!string.IsNullOrEmpty(_name) && _name!="")
            {
                this.name = _name;
            }
            else
            {
                this.name = null;
            }
            if (!string.IsNullOrEmpty(_type) && _type!="")
            {
                this.type = _type;
            }
            else
            {
                this.type = null;
            }

            if (!string.IsNullOrEmpty(_url) && _url!="")
            {
                this.url = _url;
            }
            else
            {
                this.url = null;
            }
            List<MenuItem> list = new List<MenuItem>();
            foreach (MenuItem mi in _sub_button)
            {
                list.Add(mi);
            }
            this.sub_button = list;
           
        }
    }
}

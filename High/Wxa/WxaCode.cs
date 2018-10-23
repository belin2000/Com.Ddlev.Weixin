using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Wxa
{
    /// <summary>
    /// 小程序二维码
    /// </summary>
    public class WxaCodeRequest : IFace.IRequest<WxaCodeResponse>
    {
        Config c;
        /// <summary>
        /// 二维码的宽度
        /// </summary>
        public int width { set; get; }
        /// <summary>
        /// 最大长度 128 字节 (例如 /pages/index?query=1)  (codetype =CodeType.code | CodeType.appqrcode 时候有效)
        /// </summary>
        public string path { set; get; }
        /// <summary>
        /// 是否不需要配置主色调（默认为true）
        /// </summary>
        public bool auto_color { set; get; }
        /// <summary>
        /// auto_color 为 false 时生效，使用 rgb 设置颜色
        /// </summary>
        public Line_Color line_color { set; get; }
        /// <summary>
        /// 是否使用透明背景色
        /// </summary>
        public bool is_hyaline { set; get; }
        /// <summary>
        /// 必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面 (codetype =CodeType.codeunlimit 时候有效)
        /// </summary>
        public string page { set; get; }
        /// <summary>
        /// 最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~ (codetype =CodeType.codeunlimit 时候有效)
        /// </summary>
        public string scene { set; get; }
        /// <summary>
        /// 二维码类型
        /// </summary>
        CodeType codetype { set; get; }
        /// <summary>
        /// 用于判断传送的参数
        /// </summary>
        IwxaCodePath wxacodepaty { set; get; }

        /// <summary>
        /// 初始化[已过期，不建议使用]
        /// </summary>
        /// <param name="_c">配置</param>
        /// <param name="_codetype">二维码类型</param>
        /// <param name="_width">二维码长度</param>
        /// <param name="_path">跳转到指定页面(例如 pages/index?query=1)(codetype =CodeType.code | CodeType.appqrcode 时候有效) </param>
        /// <param name="_page">必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面 (codetype =CodeType.codeunlimit 时候有效)</param>
        /// <param name="_scene">最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~ (codetype =CodeType.codeunlimit 时候有效)</param>
        /// <param name="_is_hyaline">是否使用透明背景色（默认true）,如果不是（false）,则为白色</param>
        /// <param name="_auto_color">是否不需要配置主色调（默认为true）</param>
        /// <param name="_line_color">色彩类型 </param>
        public WxaCodeRequest(Config _c, CodeType _codetype,int _width=430,string _path=null, string _page=null, string _scene=null, bool _is_hyaline=true, bool _auto_color=true,Line_Color _line_color=null)
        {
            this.c = _c;
            this.codetype = _codetype;
            this.width = _width;
            this.page = _page;
            this.path = _path;
            this.scene = _scene;
            this.auto_color = _auto_color;
            this.line_color = _line_color;
            this.is_hyaline = _is_hyaline;
        }
        /// <summary>
        /// 初始化(推荐使用)
        /// </summary>
        /// <param name="_c"></param>
        /// <param name="_wxacodepaty"></param>
        public WxaCodeRequest(Config _c, IwxaCodePath _wxacodepaty)
        {
            this.c = _c;
            this.wxacodepaty = _wxacodepaty;

        }
        public WxaCodeResponse send()
        {
            var url = GetcmethodUrl();
            return send(url);
        }
        public WxaCodeResponse send(string url)
        {
            var st = new Newtonsoft.Json.JsonSerializerSettings();
            st.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            var pjson = Newtonsoft.Json.JsonConvert.SerializeObject(wxacodepaty, st);
            var stm= BaseClass.BaseMethod.WebRequestPostBase<System.Drawing.Image>(pjson, url, Encoding.UTF8, 
                delegate(Stream stms,string ContentType, WebHeaderCollection headers) {
                Image mImage = Image.FromStream(stms);
                stms.Close();
                return mImage;
            });
            WxaCodeResponse wr = new WxaCodeResponse();
            wr.img = stm;
            wxacodepaty = null;
            return wr;
            //return Newtonsoft.Json.JsonConvert.DeserializeObject<WxaCodeResponse>(json);
        }
        string  GetcmethodUrl()
        {
            string url = "";
            if (wxacodepaty == null)
            {
                if (auto_color)
                {
                    line_color = null;
                }
                switch (codetype)
                {
                    case CodeType.code:
                        page = null;
                        scene = null;
                        wxacodepaty = new wxaPath_code(path, width, auto_color, line_color, is_hyaline);
                        break;
                    case CodeType.codeunlimit:
                        path = null;
                        wxacodepaty = new wxaPath_codeunlimit(scene, width, page, auto_color, line_color, is_hyaline);
                        break;
                    case CodeType.appqrcode:
                        auto_color = true;
                        line_color = null;
                        page = null;
                        scene = null;
                        wxacodepaty = new wxaPath_appqrcode(path, width);
                        break;
                }
            }
            switch (wxacodepaty.GetType().Name)
            {
                case "wxaPath_appqrcode":
                    url = "https://api.weixin.qq.com/cgi-bin/wxaapp/createwxaqrcode?access_token=";
                    break;
                case "wxaPath_code":
                    url = "https://api.weixin.qq.com/wxa/getwxacode?access_token=";
                    break;
                case "wxaPath_codeunlimit":
                    url = "https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token=";
                    break;
            }
            return url+new HightToken(c).Token;
        }

        public async Task<WxaCodeResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }

    

    public class WxaCodeResponse : BaseClass.BaseResponse, IFace.IResponse
    {
        public int code { set; get; }
        public System.Drawing.Image img { set; get; }
    }

    public class Line_Color
    {
        /// <summary>
        /// 表示红色（0-255）
        /// </summary>
        public int r { set; get; }
        /// <summary>
        /// 表示绿色（0-255）
        /// </summary>
        public int g { set; get; }
        /// <summary>
        /// 表示蓝色（0-255）
        /// </summary>
        public int b { set; get; }

        public Line_Color(int _r=0,int _g=0,int _b=0) {
            r = _r;
            g = _g;
            b = _b;
        }
    }

    public enum CodeType
    {
        /// <summary>
        /// 小程序码 默认（wxa/getwxacode）,直接进入path对应的页面 (接口A: 适用于需要的码数量较少的业务场景 )
        /// </summary>
        code = 0,
        /// <summary>
        /// 小程序二维码， 用户扫描该码进入小程序后，开发者需在对应页面获取的码中 scene 字段的值，再做处理逻辑(接口B：适用于需要的码数量极多，或仅临时使用的业务场景)
        /// </summary>
        codeunlimit = 1,
        /// <summary>
        /// 小程序二维码，通过该接口生成的小程序二维码，永久有效，请谨慎使用。用户扫描该码进入小程序后，将直接进入 path 对应的页面。(接口C：适用于需要的码数量较少的业务场景)
        /// </summary>
        appqrcode = 2
    }
    public interface IwxaCodePath
    {
    }

    /// <summary>
    /// (提交类)小程序二维码
    /// </summary>
    public class wxaPath_appqrcode: IwxaCodePath
    {
        /// <summary>
        /// 二维码的宽度
        /// </summary>
        public int width { set; get; }
        /// <summary>
        /// 最大长度 128 字节 (例如 pages/index?query=1)  (codetype =CodeType.code | CodeType.appqrcode 时候有效)
        /// </summary>
        public string path { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_width">二维码的宽度</param>
        /// <param name="_page">最大长度 128 字节 (例如 pages/index?query=1)</param>
        public wxaPath_appqrcode( string _path, int _width=430)
        {
            this.width = _width;
            this.path = _path;
        }
    }
    /// <summary>
    /// (提交类)小程序二维码，通过该接口生成的小程序二维码，永久有效，请谨慎使用。用户扫描该码进入小程序后，将直接进入 path 对应的页面。(接口C：适用于需要的码数量较少的业务场景)
    /// </summary>
    public class wxaPath_code : IwxaCodePath
    {
        /// <summary>
        /// 二维码的宽度
        /// </summary>
        public int width { set; get; }
        /// <summary>
        /// 最大长度 128 字节 (例如 pages/index?query=1)  (codetype =CodeType.code | CodeType.appqrcode 时候有效)
        /// </summary>
        public string path { set; get; }
        /// <summary>
        /// 是否不需要配置主色调（默认为true）
        /// </summary>
        public bool auto_color { set; get; }
        /// <summary>
        /// auto_color 为 false 时生效，使用 rgb 设置颜色
        /// </summary>
        public Line_Color line_color { set; get; }
        /// <summary>
        /// 是否使用透明背景色
        /// </summary>
        public bool is_hyaline { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_width">二维码的宽度</param>
        /// <param name="_path">最大长度 128 字节 (例如 pages/index?query=1)</param>
        /// <param name="_auto_color">是否不需要配置主色调（默认为true）</param>
        /// <param name="_line_color">auto_color 为 false 时生效，使用 rgb 设置颜色</param>
        /// <param name="_is_hyaline">是否使用透明背景色,默认是false</param>
        public wxaPath_code(string _path, int _width=430, bool _auto_color=true, Line_Color _line_color=null, bool _is_hyaline=false)
        {
            this.width = _width;
            this.path = _path;
            this.auto_color = _auto_color;
            this.line_color = _line_color;
            this.is_hyaline = _is_hyaline;
        }
    }
    /// <summary>
    /// (提交类)小程序二维码， 用户扫描该码进入小程序后，开发者需在对应页面获取的码中 scene 字段的值，再做处理逻辑(接口B：适用于需要的码数量极多，或仅临时使用的业务场景)
    /// </summary>
    public class wxaPath_codeunlimit : IwxaCodePath
    {
        /// <summary>
        /// 二维码的宽度
        /// </summary>
        public int width { set; get; }
        /// <summary>
        /// (可以为null或者空)最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~ (codetype =CodeType.codeunlimit 时候有效)
        /// </summary>
        public string scene { set; get; }
        /// <summary>
        /// (可以为null或者空)必须是已经发布的小程序页面，例如 "pages/index/index" ,根路径前不要填加'/',不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面 (codetype =CodeType.codeunlimit 时候有效)
        /// </summary>
        public string page { set; get; }
        /// <summary>
        /// 是否不需要配置主色调（默认为true）
        /// </summary>
        public bool auto_color { set; get; }
        /// <summary>
        /// auto_color 为 false 时生效，使用 rgb 设置颜色
        /// </summary>
        public Line_Color line_color { set; get; }
        /// <summary>
        /// 是否使用透明背景色
        /// </summary>
        public bool is_hyaline { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_width">二维码的宽度</param>
        /// <param name="_scene">(可以为null或者空)最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~ </param>
        /// <param name="_page">必须是已经发布的小程序页面，例如 "pages/index/index"</param>
        /// <param name="_auto_color">是否不需要配置主色调（默认为true）</param>
        /// <param name="_line_color">auto_color 为 false 时生效，使用 rgb 设置颜色</param>
        /// <param name="_is_hyaline">是否使用透明背景色,默认是false</param>
        public wxaPath_codeunlimit( string _scene=null, int _width = 430, string _page=null, bool _auto_color = true, Line_Color _line_color = null, bool _is_hyaline = false)
        {
            this.width = _width;
            this.scene = _scene;
            this.page = _page;
            this.auto_color = _auto_color;
            this.line_color = _line_color;
            this.is_hyaline = _is_hyaline;
        }
    }
}

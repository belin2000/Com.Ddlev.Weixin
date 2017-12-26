using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.High
{
    /// <summary>
    /// 带参数的二维码
    /// </summary>
    public class QrcodeRequest:IFace.IRequest<QrcodeResponse>
    {
        Config c;
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过1800(临时有效)
        /// </summary>
        public int expire_seconds { set; get; }
        /// <summary>
        /// 二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久,QR_LIMIT_STR_SCENE为永久的字符串参数值
        /// </summary>
        public string action_name { set; get; }
        /// <summary>
        /// 二维码详细信息
        /// </summary>
        public ActionInfo action_info { set; get; }
        /// <summary>
        /// 初始化（需要设置属性）
        /// </summary>
        /// <param name="_c">配置</param>
        public QrcodeRequest(Config _c)
        {
            this.c = _c;
        }
        protected QrcodeResponse send(string url) {
            string json = JsonConvert.SerializeObject(this);
            string s = BaseClass.BaseMethod.WebRequestPost(json, url, Encoding.UTF8);
            var rs = (QrcodeResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(s, typeof(QrcodeResponse));
            return rs;
        }
        public QrcodeResponse send() {
            HightToken token = new HightToken(c);
            string url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + token.Token;
            return send(url);
        }

    }
    /// <summary>
    /// 二维码详细信息
    /// </summary>
    public class ActionInfo
    {
        object _scene;

        public object scene
        {
            get { return _scene; }
            set { _scene = value; }
        }
        public ActionInfo() { }

        public ActionInfo(int _sid)
        {
            _scene = new Scene(_sid);
        }
        /// <summary>
        /// 只支持永久二维码使用
        /// </summary>
        /// <param name="_scene_str"></param>
        public ActionInfo(string _scene_str)
        {
            _scene = new SceneStr(_scene_str);
        }
    }
    public class Scene
    {
        int scene_id_;
        /// <summary>
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
        /// </summary>
        public int scene_id
        {
            set { scene_id_ = value; }
            get { return scene_id_; }
        }
        public Scene() { }
        /// <summary>
        /// 临时/永久二维码(使用)
        /// </summary>
        /// <param name="_scene_id">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）</param>
        public Scene(int _scene_id)
        {
            scene_id_ = _scene_id;
        }
    }

    public class SceneStr
    {
        public SceneStr() { }
        /// <summary>
        /// 永久二维码(使用)
        /// </summary>
        /// <param name="_scene_str"></param>
        public SceneStr(string _scene_str)
        {
            scene_str = _scene_str;
        }
        string scene_str_;
        /// <summary>
        /// 永久二维码ID
        /// </summary>
        public string scene_str
        {
            set { scene_str_ = value; }
            get { return scene_str_; }
        }
    }

    public class QrcodeResponse: IFace.IResponse {
        #region 正确返回
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码。(https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=TICKET TICKET记得进行UrlEncode)
        /// </summary>
        public string ticket { set; get; }
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过2592000（即30天）。
        /// </summary>
        public int expire_seconds { set; get; }
        /// <summary>
        /// 二维码图片解析后的地址
        /// </summary>
        public string url { set; get; }
        #endregion

    }
}

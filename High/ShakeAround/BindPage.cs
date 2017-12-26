using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.ShakeAround
{
    public class BindPageRequest : IFace.IRequest<BindPageResponse>
    {

        Config c;

        /// <summary>
        /// 设备信息
        /// </summary>
        public Device_Identifier device_identifier { set; get; }
        /// <summary>
        /// 对于的页面的ID集合
        /// </summary>
        public List<int> page_ids { set; get; }


        public BindPageRequest(Config _c)
        {
            c = _c;
        }
        public BindPageResponse send()
        {
            var p = Newtonsoft.Json.JsonConvert.SerializeObject(this);

            BindPageResponse sr = new BindPageResponse();
            try
            {
                sr = (BindPageResponse)Newtonsoft.Json.JsonConvert.DeserializeObject(BaseClass.BaseMethod.WebRequestPost(p, "https://api.weixin.qq.com/shakearound/device/bindpage?access_token=" + new HightToken(c).Token, Encoding.UTF8), typeof(BindPageResponse));
            }
            catch
            { }
            return sr;
        }
    }

    public class BindPageResponse : ShakeAroundBaseResponse, IFace.IResponse
    { }
    public class Device_Identifier
    {
        public int device_id { set; get; }
        public string uuid { set; get; }
        public int major { set; get; }
        public int minor { set; get; }

        public Device_Identifier(int _device_id, string _uuid, int _major, int _minor)
        {
            this.device_id = _device_id;
            this.uuid = _uuid;
            this.major = _major;
            this.minor = _minor;
        }
    }
}

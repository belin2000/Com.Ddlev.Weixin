using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Device
{
    public class DeviceItem
    {
        /// <summary>
        /// 设备的deviceid
        /// </summary>
        public string device_id { set; get; }
        /// <summary>
        /// 设备类型，目前为“公众账号原始ID”
        /// </summary>
        public string device_type { set; get; }
    }
}

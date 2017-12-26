using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.Pay.Unifiedorder
{
    public class Wap_Info: IH5_Info
    {
        /// <summary>
        /// 场景类型
        /// </summary>
        public string type
        {
            get { return "Wap"; }
        }
        /// <summary>
        ///  WAP网站URL地址 (https://m.jd.com)
        /// </summary>
        public string wap_url
        {
            set; get;
        }
        /// <summary>
        /// WAP 网站名(京东官网)
        /// </summary>
        public string wap_name
        {
            set; get;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_app_name">WAP 网站名(京东官网)</param>
        /// <param name="package_name">WAP网站URL地址 (https://m.jd.com)</param>
        public Wap_Info(string _wap_name, string _wap_url)
        {
            this.wap_name = _wap_name;
            this.wap_url = _wap_url;
        }
    }
}

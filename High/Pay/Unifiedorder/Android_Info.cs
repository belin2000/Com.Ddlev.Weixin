using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.Pay.Unifiedorder
{
    /// <summary>
    /// 安卓接口
    /// </summary>
    public class Android_Info : IH5_Info
    {
        /// <summary>
        /// 场景类型
        /// </summary>
        public string type
        {
            get { return "Android"; }
        }
        /// <summary>
        ///  应用名 例如(QQ)
        /// </summary>
        public string app_name
        {
            set; get;
        }
        /// <summary>
        /// 应用包名字 (com.tencent.wzryIOS)
        /// </summary>
        public string package_name
        {
            set; get;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_app_name">应用名 例如(QQ)</param>
        /// <param name="package_name">应用包名字 (com.tencent.wzryIOS)</param>
        public Android_Info(string _app_name, string _package_name)
        {
            this.app_name = _app_name;
            this.package_name = _package_name;
        }
    }
}

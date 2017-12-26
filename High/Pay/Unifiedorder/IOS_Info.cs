using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ddlev.Weixin.High.Pay.Unifiedorder
{
    /// <summary>
    /// IOS接口
    /// </summary>
    public class IOS_Info : IH5_Info
    {
        /// <summary>
        /// 场景类型
        /// </summary>
        public string type
        {
            get { return "ISO"; }
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
        public string bundle_id
        {
            set; get;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_app_name">应用名 例如(QQ)</param>
        /// <param name="_bundle_id">应用包名字 (com.tencent.wzryIOS)</param>
        public IOS_Info(string _app_name, string _bundle_id)
        {
            this.app_name = _app_name;
            this.bundle_id = _bundle_id;
        }
    }
}

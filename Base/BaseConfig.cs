using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.Base
{
    public class BaseConfig:IFace.IConfig
    {
        /// <summary>
        /// 第三方用户唯一凭证 
        /// </summary>
        public string AppID { set; get; }
        /// <summary>
        /// 第三方用户唯一凭证密钥，即appsecret 
        /// </summary>
        public string SecretKey{ set; get; }
    }
}

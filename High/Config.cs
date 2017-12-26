using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.High
{
    /// <summary>
    /// 微信认证配置
    /// </summary>
    public class Config: Com.Ddlev.Weixin.Config
    {
        public Config() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_Token">设置token</param>
        /// <param name="_URL">接口的URL</param>
        /// <param name="_AppID">第三方用户唯一凭证(基础版本为空)</param>
        /// <param name="_SecretKey">第三方用户唯一凭证密钥，即appsecret </param>
        public Config(string _Token, string _URL, string _AppID, string _SecretKey)
        {
            this.AppID = _AppID;
            this.SecretKey = _SecretKey;
            this.Token = _Token;
            this.URL = _URL;
        }


    }
}

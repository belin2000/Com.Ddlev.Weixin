using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin
{
    public class Config:Base.BaseConfig
    {
        /// <summary>
        /// 设置获取获取token
        /// </summary>
        public string Token{set;get;}
        /// <summary>
        /// 接口的URL
        /// </summary>
        public string URL{ set; get; }

        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public string EncodingAESKey { set; get; }
        /// <summary>
        /// 消息加解密方式(1,明文，2兼容模式,3,密文模式)
        /// </summary>
        public int EncodingType { set; get; }

        public Config() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_Appid">APPid</param>
        /// <param name="_SecretKey">APPid对应的密钥</param>
        public Config(string _Appid, string _SecretKey)
        {
            this.AppID = "";
        }
        /// <summary>
        /// 初始化(企业号只能是密文模式)
        /// </summary>
        /// <param name="_Appid">APPid</param>
        /// <param name="_SecretKey">APPid对应的密钥</param>
        /// <param name="_Token">设置token</param>
        /// <param name="_URL">接口的URL</param>
        /// <param name="EncodingAESKey_">消息加解密密钥</param>
        /// <param name="EncodingType_">消息加解密方式(1,明文，2兼容模式,3,密文模式)</param>
        public Config(string _Appid, string _SecretKey,string _Token, string _URL, string EncodingAESKey_, int EncodingType_)
        {
            this.Token = _Token;
            this.URL = _URL;
            this.EncodingAESKey = EncodingAESKey_;
            this.EncodingType = EncodingType_;
            this.AppID = _Appid;
            this.SecretKey = _SecretKey;
        }
    }

}

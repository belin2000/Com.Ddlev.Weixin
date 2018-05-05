using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Wxa
{
    /// <summary>
    /// 通过小程序获取用户的电话号码（填写在微信的）
    /// </summary>
    public class PhoneNumberRequest : IFace.IRequest<PhoneNumberResponse>
    {
        string session_key; string iv; string encryptedData;
        public PhoneNumberRequest(string openid, string _iv, string _encryptedData)
        {
            this.session_key = DataCacheConfig.GetHelper().Get<string>("sessionkey_"+openid); 
            this.iv = _iv;
            this.encryptedData = _encryptedData;
        }
        public PhoneNumberResponse send()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PhoneNumberResponse>(Com.Ddlev.Cryptography.Encrypt.AESDecrypt(encryptedData, Convert.FromBase64String(session_key), Convert.FromBase64String(iv)));
        }
        public async Task<PhoneNumberResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    public class PhoneNumberResponse : IFace.IResponse
    {
        /// <summary>
        /// 用户绑定的手机号（国外手机号会有区号）
        /// </summary>
        public string phoneNumber { set; get; }
        /// <summary>
        /// 没有区号的手机号
        /// </summary>
        public string purePhoneNumber { set; get; }
        /// <summary>
        /// 区号
        /// </summary>
        public string countryCode { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.High.Wxa
{
    /// <summary>
    /// 获取用户的相关信息（微信资料，只是小程序使用）
    /// </summary>
    public class UserInfoRequest : IFace.IRequest<UserInfoResponse>
    {
        string session_key; string iv; string encryptedData;
        /// <summary>
        /// 获取用户的信息
        /// </summary>
        /// <param name="_session_key">Snsscodesession获取的的session_key</param>
        /// <param name="_iv">登录时候返回的iv</param>
        /// <param name="_encryptedData">登录时候返回的encryptedData</param>
        public UserInfoRequest( string _session_key, string _iv, string _encryptedData)
        {
            this.session_key = _session_key;
            this.iv = _iv;
            this.encryptedData = _encryptedData;
        }
        public UserInfoResponse send()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfoResponse>(Com.Ddlev.Cryptography.Encrypt.AESDecrypt(encryptedData, Convert.FromBase64String(session_key), Convert.FromBase64String(iv)));
        }

        public async Task<UserInfoResponse> sendasync()
        {
            return await Task.Run(() => { return send(); });
        }
    }
    /// <summary>
    /// 返回用户的相关信息,和User类一样
    /// </summary>
    public class UserInfoResponse : User.User, IFace.IResponse
    {
    }
}

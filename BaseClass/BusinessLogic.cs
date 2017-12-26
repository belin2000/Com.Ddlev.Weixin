using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.BaseClass
{
    public class BusinessLogic:ReturnCode
    {
        string _appid;
        /// <summary>
        /// 微信分配的公众账号ID
        /// </summary>
        public string appid
        {
            get { return _appid; }
            set { _appid = value; }
        }
        string _mch_id;
        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mch_id
        {
            get { return _mch_id; }
            set { _mch_id = value; }
        }

        string _sub_mch_id;
        /// <summary>
        /// 微信支付分配的子商户号，受理模式下必填
        /// </summary>
        public string sub_mch_id
        {
            get { return _sub_mch_id; }
            set { _sub_mch_id = value; }
        }
        string _device_info;
        /// <summary>
        /// 微信支付分配的终端设备号，
        /// </summary>
        public string device_info
        {
            get { return _device_info; }
            set { _device_info = value; }
        }
        string _nonce_str;
        /// <summary>
        /// 随机字符串，不长于32位
        /// </summary>
        public string nonce_str
        {
            get { return _nonce_str; }
            set { _nonce_str = value; }
        }
        string _sign;
        /// <summary>
        /// 签名
        /// </summary>
        public string sign
        {
            get { return _sign; }
            set { _sign = value; }
        }
        string _result_code;
        /// <summary>
        /// 业务结果:SUCCESS/FAIL
        /// </summary>
        public string result_code
        {
            get { return _result_code; }
            set { _result_code = value; }
        }
        string _err_code;
        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code
        {
            get { return _err_code; }
            set { _err_code = value; }
        }
        string _err_code_des;
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des
        {
            get { return _err_code_des; }
            set { _err_code_des = value; }
        }

    }
}

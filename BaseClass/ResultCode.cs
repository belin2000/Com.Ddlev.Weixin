using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.BaseClass
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultCode
    {
        string _result_code;
        /// <summary>
        /// 业务结果 SUCCESS/FAIL
        /// </summary>
        public string result_code
        {
            get { return _result_code; }
            set { _result_code = value; }
        }
        string _err_code_des;
        /// <summary>
        /// 当 result_code 为 FAIL 时，返回错误信息，微信直接展示给用户，例如：订单过期，无效订单等
        /// </summary>
        public string err_code_des
        {
            get { return _err_code_des; }
            set { _err_code_des = value; }
        }
    }
}

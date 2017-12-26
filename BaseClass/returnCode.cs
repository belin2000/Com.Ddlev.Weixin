using System;
using System.Collections.Generic;
using System.Text;


namespace Com.Ddlev.Weixin.BaseClass
{
    public class ReturnCode
    {
        string _return_code;
        /// <summary>
        /// 此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
        /// </summary>
        public string return_code
        {
            get { return _return_code; }
            set { _return_code = value; }
        }
        string _return_msg;
        /// <summary>
        /// 返回信息，如非空，为错误原因签名失败参数格式校验错误
        /// </summary>
        public string return_msg
        {
            get { return _return_msg; }
            set { _return_msg = value; }
        }
    }
}

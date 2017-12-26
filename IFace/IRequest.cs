using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.IFace
{
    public interface IRequest<T> where T : IResponse
    {
        /// <summary>
        /// 接口请求数据，并返回信息类
        /// </summary>
        /// <returns></returns>
        T send();
        
    }
}

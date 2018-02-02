using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.IFace
{
    public interface IMethod
    {
        /// <summary>
        /// 执行类的功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T Method<T>(IRequest<T> t) where T : IResponse;
        /// <summary>
        /// 异步执行类的功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<T> MethodAsync<T>(IRequest<T> t) where T : IResponse;
    }
}

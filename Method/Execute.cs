using System;
using Com.Ddlev.Weixin.IFace;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.Method
{
    public class Execute : IFace.IMethod
    {
        public T Method<T>(IRequest<T> t) where T : IResponse
        {
            return t.send();
        }

        public Task<T> MethodAsync<T>(IRequest<T> t) where T : IResponse
        {
            
            return Task.Run(()=> {
                return t.sendasync(); 
            } );
        }
    }
}

using Com.Ddlev.Weixin.IFace;

namespace Com.Ddlev.Weixin.Method
{
    public class Execute : IFace.IMethod
    {
        public T Method<T>(IRequest<T> t) where T : IResponse
        {
            return t.send();
        }
    }
}

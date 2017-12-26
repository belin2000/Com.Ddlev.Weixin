using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ddlev.Weixin.IFace
{
    public interface IMethod
    {
        T Method<T>(IRequest<T> t) where T : IResponse;
    }
}

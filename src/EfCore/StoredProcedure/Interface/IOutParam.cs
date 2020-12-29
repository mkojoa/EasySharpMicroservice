using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.EfCore.StoredProcedure.Interface
{
    public interface IOutParam<T>
    {
        T Value { get; }
    }
}

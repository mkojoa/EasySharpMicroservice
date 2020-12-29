using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Core.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    { }
}

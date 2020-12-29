using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySharp.Core.Queries
{
    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
    { }
}

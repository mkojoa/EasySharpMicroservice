using EasySharp.Core.Events;
using System;

namespace EasySharp.EventStores.Projection
{
    public interface IProjection
    {
        Type[] Handles { get; }
        void Handle(IEvent @event);
    }
}

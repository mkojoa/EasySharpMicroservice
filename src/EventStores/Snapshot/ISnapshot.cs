using EasySharp.EventStores.Aggregate;
using System;

namespace EasySharp.EventStores.Snapshot
{
    public interface ISnapshot
    {
        Type Handles { get; }
        void Handle(IAggregate aggregate);
    }
}

using EasySharp.Core.Events;
using System;
using System.Collections.Generic;

namespace EasySharp.EventStores.Aggregate
{
    public interface IAggregate
    {
        Guid Id { get; }
        int Version { get; }
        DateTime CreatedUtc { get; }

        IEnumerable<IEvent> DequeueUncommittedEvents();

    }
}

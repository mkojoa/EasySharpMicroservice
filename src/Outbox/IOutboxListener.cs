using EasySharp.Core.Events;
using System.Threading.Tasks;

namespace EasySharp.Outbox
{
    public interface IOutboxListener
    {
        Task Commit(OutboxMessage message);
        Task Commit<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}

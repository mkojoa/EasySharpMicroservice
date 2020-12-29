using EasySharp.EventStores;
using System.Threading.Tasks;

namespace EasySharp.Core.Events
{
    public interface IEventBus
    {
        Task PublishLocal(params IEvent[] events);
        Task Commit(params IEvent[] events);
        Task Commit(StreamState stream);
    }
}

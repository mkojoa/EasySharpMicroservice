using MediatR;

namespace EasySharp.Core.Events
{
    public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent
    { }
}

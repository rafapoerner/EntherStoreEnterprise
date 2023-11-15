using EasyNetQ;
using ESE.Core.Messages.Integration;

namespace ESE.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }

        IAdvancedBus AdvancedBus { get; }   

        void Publish<T>(T message) where T : IntegrationEvent;

        Task PublishAsync<T>(T message) where T : IntegrationEvent;

        void Subscribe<T>(string subscritionId, Action<T> onMessage) where T : class;
        void SubscribeAsync<T>(string subscritionId, Func<T, Task> onMessage) where T : class;

        TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> respond)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage;

        Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> respond)
           where TRequest : IntegrationEvent
           where TResponse : ResponseMessage;
    }
}

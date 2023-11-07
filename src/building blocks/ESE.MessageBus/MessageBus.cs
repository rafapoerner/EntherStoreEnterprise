using EasyNetQ;
using ESE.Core.Messages;
using ESE.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace ESE.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        bool IsConnected => _bus?.Advanced.IsConnected ?? false;

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            _bus.PubSub.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.PubSub.PublishAsync(message);
        }

        public void Subscribe<T>(string subscritionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.Subscribe(subscritionId, onMessage);
        }

        public async void SubscribeAsync<T>(string subscritionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            await _bus.PubSub.SubscribeAsync(subscritionId, onMessage);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            return _bus.Rpc.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            return await _bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> respond) 
            where TRequest : IntegrationEvent where TResponse: ResponseMessage
        {
            TryConnect();
            return  _bus.Rpc.Respond<TRequest, TResponse>(respond);
        }

        public async Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> respond)
          where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            var responseDisposable = await _bus.Rpc.RespondAsync(respond);
            return responseDisposable; 
        }

        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt =>
                  TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() => { _bus = RabbitHutch.CreateBus(_connectionString); });
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}

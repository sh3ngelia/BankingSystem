using BankingSystem.Shared.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Identity.Infrastructure.Messaging;

public class RabbitMqEventBus : IDisposable
{
    private readonly IConnection _connection;
    private readonly RabbitMQ.Client.IModel _channel;
    private const string ExchangeName = "banking_system";

    public RabbitMqEventBus(string hostName)
    {
        var factory = new ConnectionFactory { HostName = hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(
            exchange: ExchangeName,
            type: ExchangeType.Topic,
            durable: true);
    }

    public async Task PublishAsync<T>(T @event, CancellationToken ct = default)
        where T : IDomainEvent
    {
        var routingKey = @event.GetType().Name;
        var message = JsonSerializer.Serialize(@event, @event.GetType());
        var body = Encoding.UTF8.GetBytes(message);

        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";
        properties.Type = routingKey;

        _channel.BasicPublish(
            exchange: ExchangeName,
            routingKey: routingKey,
            basicProperties: properties,
            body: body);

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
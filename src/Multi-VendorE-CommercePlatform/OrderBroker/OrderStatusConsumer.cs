using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Multi_VendorE_CommercePlatform.OrderBroker;

public class OrderStatusConsumer: BackgroundService
{
    private readonly IConfiguration _config;

    public OrderStatusConsumer(IConfiguration configuration)
    {
        _config = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ:Host"],
            Port = int.Parse(_config["RabbitMQ:Port"]!),
            VirtualHost = _config["RabbitMQ:VirtualHost"],
            UserName = _config["RabbitMQ:Username"],
            Password = _config["RabbitMQ:Password"]
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: _config["RabbitMQ:QueueName"],
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Processing message: {message}");

            await NotifyVendorOrCustomer(message);
        };

        channel.BasicConsume(
            queue: _config["RabbitMQ:QueueName"],
            autoAck: true,
            consumer: consumer);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task NotifyVendorOrCustomer(string message)
    {
        Console.WriteLine($"Notifying about: {message}");
        await Task.Delay(1000);
    }
}
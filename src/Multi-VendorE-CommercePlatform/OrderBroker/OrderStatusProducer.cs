using System.Text;
using Multi_VendorE_CommercePlatform.Models.Entities;
using RabbitMQ.Client;

namespace Multi_VendorE_CommercePlatform.OrderBroker;

public class OrderStatusProducer
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public OrderStatusProducer(ApplicationDbContext context,
        IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public void ProduceOrderStatus()
    {
        var overDueOrders = _context.Orders
            .Where(order => order.OrderStatus == Models.Order.Status.Processing
            && order.UpdatedAt < DateTime.UtcNow.AddHours(-2))
            .ToList();
        if (overDueOrders.Any())
        {
            foreach (var order in overDueOrders)
            {
                var message = $"OrderId: {order.Id}, CustomerId: {order.CustomerId}, UpdatedAt: {order.UpdatedAt}";
                
                SendMessageToQueue(message);
            }
        }
    }
    
    private void SendMessageToQueue(string message)
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

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: _config["RabbitMQ:QueueName"],
            basicProperties: null,
            body: body);

        Console.WriteLine($"Published message: {message}");
    }
}

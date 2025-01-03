using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMqBrokerLibrary.Broker;

public class RabbitMqProducer
{
    private readonly string _localHost = "localhost";
    private readonly string _queueName = "email_queue";

    public void PublishEmailEvent(EmailMessage email)
    {
        var factory = new ConnectionFactory() { HostName = _localHost };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(
            _queueName, durable: true, exclusive: false,
            autoDelete: false, arguments: null);
        var messageBody = Encoding.UTF8
            .GetBytes(JsonConvert.SerializeObject(email));
        
        channel.BasicPublish(exchange:""
            ,routingKey: _queueName, 
            basicProperties: null,
            body: messageBody);
        
        Console.WriteLine($"[x] Sent Email event {email.Subject}");
    }
    
}

public class EmailMessage
{
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
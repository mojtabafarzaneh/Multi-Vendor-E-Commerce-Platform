using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqBroker.Broker;

public class RabbitMqConsumer
{
    private readonly string _hostName = "localhost";
    private readonly string _queueName = "email_queue";

    public void StartListening()
    {
        var factory = new ConnectionFactory();
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(
            queue: _queueName, durable: true,
            exclusive: false, autoDelete: false,
            arguments: null);
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var email = JsonConvert.DeserializeObject<EmailMessage>(message);

            Console.WriteLine($"[x] Sending Email to: {email!.To}");
            Console.WriteLine($"[x] Subject: {email.Subject}");
            Console.WriteLine($"[x] Body: {email.Body}");
        };
        channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        
        Console.WriteLine("[*] Waiting for messages. Press [enter] to exit.");
        Console.ReadLine();

    }
}
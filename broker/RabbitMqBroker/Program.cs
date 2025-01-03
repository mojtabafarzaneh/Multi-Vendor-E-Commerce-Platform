using Multi_VendorE_CommercePlatform.OrderBroker;
using RabbitMqBrokerLibrary.Broker;

var consumer = new RabbitMqConsumer();
consumer.StartListening();

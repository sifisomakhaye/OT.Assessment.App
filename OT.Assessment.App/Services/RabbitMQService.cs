using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Logging;

namespace OT.Assessment.App.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ILogger<RabbitMQService> _logger; // Logger field
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService(ILogger<RabbitMQService> logger)
        {
            _logger = logger; // Initialize the logger

            // Create the connection and channel once during initialization
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "wagerQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish(exchange: "", routingKey: "wagerQueue", basicProperties: null, body: body);
                _logger.LogInformation("Message published to RabbitMQ: {Message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message to RabbitMQ");
                throw; 
            }
        }

        public void Consume(Func<string, Task> messageHandler)
        {
          
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
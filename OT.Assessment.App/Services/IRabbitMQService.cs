namespace OT.Assessment.App.Services
{
    public interface IRabbitMQService
    {
        void Publish(string message);
        void Consume(Func<string, Task> messageHandler); // Accept a message handler
    }
}
using OT.Assessment.App.Models;
using RabbitMQ.Client.Events;
using System.Text;
using Newtonsoft.Json;
using OT.Assessment.App.Data;
using OT.Assessment.App.Services;

namespace OT.Assessment.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly OTDbContext _context;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IDatabaseService _databaseService;

        public Worker(OTDbContext context, IRabbitMQService rabbitMQService, IDatabaseService databaseService)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
            _databaseService = databaseService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _rabbitMQService.Consume(async (message) =>
            {
                var wager = JsonConvert.DeserializeObject<CasinoWager>(message);

                // Check if wager is null and handle it
                if (wager == null)
                {
                    // Log the error or handle the null case appropriately
                    Console.WriteLine($"Failed to deserialize message: {message}");
                    return; // Exit early to avoid passing null to the SaveToDatabaseAsync method
                }

                await SaveToDatabaseAsync(wager);
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);  // Keep the worker alive
            }
        }

        private async Task SaveToDatabaseAsync(CasinoWager wager)
        {
            await _databaseService.SaveWagerAsync(wager); // Use database service to save the wager
        }
    }
}
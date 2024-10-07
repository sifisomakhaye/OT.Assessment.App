using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OT.Assessment.App.Models;
using OT.Assessment.App.Services;

namespace OT.Assessment.App.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMqService;
        private readonly ILogger<PlayerController> _logger;
        private readonly ICasinoWagerService _casinoWagerService; // New service for fetching casino wagers

        // Injecting the RabbitMQ service, logger, and casino wager service via dependency injection
        public PlayerController(IRabbitMQService rabbitMqService, ILogger<PlayerController> logger, ICasinoWagerService casinoWagerService)
        {
            _rabbitMqService = rabbitMqService;
            _logger = logger;
            _casinoWagerService = casinoWagerService; // Initialize the new service
        }

        [HttpPost("casinowager")]
        public IActionResult PostCasinoWager([FromBody] CasinoWager wager)
        {
            var json = JsonConvert.SerializeObject(wager);
            _logger.LogInformation("Publishing wager: {Wager}", json);
            _rabbitMqService.Publish(json);

            return Ok();
        }

        /// <summary>
        /// Returns a paginated list of the latest casino wagers for a specific player.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        /// <param name="page">The page number (default is 1).</param>
        /// <returns>A paginated list of casino wagers.</returns>
        [HttpGet("{playerId}/casino")]
        public IActionResult GetPlayerCasinoWagers(Guid playerId, int pageSize = 10, int page = 1)
        {
            var wagers = _casinoWagerService.GetPlayerWagers(playerId, pageSize, page);

            if (wagers == null || !wagers.Items.Any())
            {
                return NotFound();
            }

            return Ok(new
            {
                data = wagers.Items,
                page = wagers.Page,
                pageSize = wagers.PageSize,
                total = wagers.TotalCount,
                totalPages = wagers.TotalPages
            });
        }
    }
}
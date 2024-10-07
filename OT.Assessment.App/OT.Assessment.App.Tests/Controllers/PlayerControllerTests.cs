using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OT.Assessment.App.Controllers;
using OT.Assessment.App.Models;
using OT.Assessment.App.Services;
using Xunit;
using Microsoft.Extensions.Logging; 

namespace OT.Assessment.App.Tests.Controllers
{
    public class PlayerControllerTests
    {
        private readonly Mock<IRabbitMQService> _mockRabbitMqService;
        private readonly Mock<ICasinoWagerService> _mockCasinoWagerService;
        private readonly Mock<ILogger<PlayerController>> _mockLogger; // Mock logger
        private readonly PlayerController _controller;

        public PlayerControllerTests()
        {
            _mockRabbitMqService = new Mock<IRabbitMQService>();
            _mockCasinoWagerService = new Mock<ICasinoWagerService>();
            _mockLogger = new Mock<ILogger<PlayerController>>(); // Initialize mock logger
            _controller = new PlayerController(_mockRabbitMqService.Object, _mockLogger.Object, _mockCasinoWagerService.Object); // Pass mock logger
        }

        [Fact]
        public void GetPlayerCasinoWagers_ReturnsOkResult_WithWagers()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var mockWagers = new PaginatedList<CasinoWager>(
                new List<CasinoWager>
                {
                    new CasinoWager { WagerId = Guid.NewGuid(), GameName = "Test Game", Provider = "Test Provider", Amount = 100, CreatedDateTime = DateTime.UtcNow },
                    // Add more mock wagers as needed
                },
                2, // Total count
                1, // Current page
                10 // Page size
            );

            _mockCasinoWagerService.Setup(service => service.GetPlayerWagers(playerId, 10, 1)).Returns(mockWagers);

            // Act
            var result = _controller.GetPlayerCasinoWagers(playerId, 10, 1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var responseData = result.Value as dynamic ?? new { data = new List<CasinoWager>(), page = 0, pageSize = 0, total = 0, totalPages = 0 };

            Assert.Equal(mockWagers.Items, responseData.data);
            Assert.Equal(1, responseData.page);
            Assert.Equal(10, responseData.pageSize);
            Assert.Equal(2, responseData.total);
            Assert.Equal(1, responseData.totalPages);
        }
    }
}
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OT.Assessment.App.Data; 
using OT.Assessment.App.Models;
using Xunit;

namespace OT.Assessment.App.Tests.Data
{
    public class OTDbContextTests : IDisposable
    {
        private readonly OTDbContext _context;

        public OTDbContextTests()
        {
            var options = new DbContextOptionsBuilder<OTDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new OTDbContext(options);

           
        }

        [Fact]
        public void Can_Add_CasinoWager()
        {
            // Arrange
            var wager = new CasinoWager
            {
                WagerId = Guid.NewGuid(),
                Theme = "Adventure",
                Provider = "GameProvider",
                GameName = "Treasure Hunt",
                Amount = 100.00m,
                CreatedDateTime = DateTime.Now,
                NumberOfBets = 5,
                CountryCode = "US",
                SessionData = "SessionInfo",
                Duration = 120
            };

            // Act
            _context.CasinoWagers.Add(wager);
            _context.SaveChanges();

            // Assert
            var savedWager = _context.CasinoWagers.FirstOrDefault(w => w.WagerId == wager.WagerId);
            Assert.NotNull(savedWager);
            Assert.Equal("Adventure", savedWager.Theme);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
using System;
using OT.Assessment.App.Models;
using Xunit;

namespace OT.Assessment.App.Tests.Models
{
    public class CasinoWagerTests : BaseModelTests<CasinoWager>
    {
        [Fact]
        public void Wager_ShouldHaveValidProperties()
        {
            // Arrange
            var wager = CreateDefaultModel();

            // Assign default values
            wager.WagerId = Guid.NewGuid();
            wager.Theme = "Adventure";
            wager.Provider = "GameProvider";
            wager.GameName = "Treasure Hunt";
            wager.TransactionId = "TX12345";
            wager.BrandId = "BRAND001";
            wager.AccountId = "ACC12345";
            wager.Username = "PlayerOne";
            wager.ExternalReferenceId = "EXT12345";
            wager.TransactionTypeId = "TRANSACTION001";
            wager.Amount = 100.00m;
            wager.CreatedDateTime = DateTime.Now; 
            wager.NumberOfBets = 5;
            wager.CountryCode = "US";
            wager.SessionData = "SessionInfo";
            wager.Duration = 120;

            // Act & Assert
            AssertModelProperties(wager, model =>
            {
                Assert.NotEqual(Guid.Empty, model.WagerId);
                Assert.Equal("Adventure", model.Theme);
                Assert.Equal("GameProvider", model.Provider);
                Assert.Equal("Treasure Hunt", model.GameName);
                Assert.Equal("TX12345", model.TransactionId);
                Assert.Equal("BRAND001", model.BrandId);
                Assert.Equal("ACC12345", model.AccountId);
                Assert.Equal("PlayerOne", model.Username);
                Assert.Equal("EXT12345", model.ExternalReferenceId);
                Assert.Equal("TRANSACTION001", model.TransactionTypeId);
                Assert.Equal(100.00m, model.Amount);
                Assert.NotEqual(DateTime.MinValue, model.CreatedDateTime); // Correct assertion for DateTime
                Assert.Equal(5, model.NumberOfBets);
                Assert.Equal("US", model.CountryCode);
                Assert.Equal("SessionInfo", model.SessionData);
                Assert.Equal(120, model.Duration);
            });
        }
    }
}
using OT.Assessment.App.Models;

namespace OT.Assessment.App.Services
{
    public interface IDatabaseService
    {
        Task SaveWagerAsync(CasinoWager wager);
    }
}
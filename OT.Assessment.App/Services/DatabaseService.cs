using OT.Assessment.App.Data;
using OT.Assessment.App.Models;
using System.Threading.Tasks;

namespace OT.Assessment.App.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly OTDbContext _context;

        public DatabaseService(OTDbContext context)
        {
            _context = context;
        }

        public async Task SaveWagerAsync(CasinoWager wager)
        {
            _context.CasinoWagers.Add(wager);
            await _context.SaveChangesAsync();
        }
    }
}
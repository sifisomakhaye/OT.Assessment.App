using System;
using OT.Assessment.App.Models;

namespace OT.Assessment.App.Services
{
    public interface ICasinoWagerService
    {
        PaginatedList<CasinoWager> GetPlayerWagers(Guid playerId, int pageSize, int page);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Service.Contracts
{
    public interface ITournamentService
    {
        Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false, bool sortByTitle = false);
        Task<TournamentDetails?> GetAsync(int id, bool includeGames = false);
        Task<bool> AnyAsync(int id);
    }
}

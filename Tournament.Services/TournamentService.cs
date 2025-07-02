using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false, bool sortByTitle = false)
        {
            throw new NotImplementedException();
        }

        public Task<TournamentDetails?> GetAsync(int id, bool includeGames = false)
        {
            throw new NotImplementedException();
        }
    }
}

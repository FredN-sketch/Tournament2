using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Request;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tournament.Core.Repositories;

public interface ITournamentRepository
{
    Task<PagedList<TournamentDetails>> GetAllAsync(TournamentRequestParams requestParams);
    Task<TournamentDetails?> GetAsync(int id, bool includeGames = false);
    Task<bool> AnyAsync(int id);
    void Add(TournamentDetails tournamentDetails);
    void Update(TournamentDetails tournamentDetails);
    void Remove (TournamentDetails tournamentDetails);
    Task<int> CountGames(int tournamentDetailsId);
}

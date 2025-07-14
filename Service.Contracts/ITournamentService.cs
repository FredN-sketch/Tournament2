using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Request;

namespace Service.Contracts
{
    public interface ITournamentService
    {
        //Todo: TrackChanges=false?
    
        Task<(IEnumerable<TournamentDto> tournamentDtos, MetaData metaData)> GetAllAsync(TournamentRequestParams requestParams);
        Task<TournamentDto?> GetAsync(int id, bool includeGames = false);
        Task<bool> AnyAsync(int id);
    }
}

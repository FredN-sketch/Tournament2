using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Core.Request;
using Tournament.Data.Data;
    
namespace Tournament.Data.Repositories;

public class TournamentRepository : ITournamentRepository
{
    private readonly TournamentApiContext _context;
    public TournamentRepository(TournamentApiContext context)
    {
        _context = context;
    }
  //  public async Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false, bool sortByTitle = false)
   // public async Task<IEnumerable<TournamentDetails>> GetAllAsync(bool includeGames = false, bool sortByTitle = false)
    public async Task<PagedList<TournamentDetails>> GetAllAsync(TournamentRequestParams requestParams)
    {
        //var tournaments = requestParams.IncludeGames 
        //    ? await _context.TournamentDetails.Include(t => t.Games).ToListAsync() 
        //    : await _context.TournamentDetails.ToListAsync();
        //var sortedTournaments = requestParams.SortByTitle 
        //    ? tournaments.OrderBy(t => t.Title).ToList() 
        //    : tournaments;
       
        var tournaments = requestParams.IncludeGames
           ? _context.TournamentDetails.AsQueryable().Include(t => t.Games)
           : _context.TournamentDetails.AsQueryable();
        var sortedTournaments = requestParams.SortByTitle
            ? tournaments.OrderBy(t => t.Title)
            : tournaments;
       
        // return sortedTournaments;
        return await PagedList<TournamentDetails>.CreateAsync(sortedTournaments, requestParams.PageNumber, requestParams.PageSize);
    }
    public async Task<TournamentDetails?> GetAsync(int id, bool includeGames = false)
    {
        return includeGames
            ? await _context.TournamentDetails
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id) 
            : await _context.TournamentDetails.FindAsync(id); 
    }

    public async Task<bool> AnyAsync(int id)
    {
        return await _context.TournamentDetails.AnyAsync(e => e.Id == id);
    }
    public void Add(TournamentDetails tournamentDetails)
    {
        _context.TournamentDetails.Add(tournamentDetails);        
    }
    public void Update(TournamentDetails tournamentDetails)
    {
        _context.TournamentDetails.Update(tournamentDetails);
    }
    public void Remove(TournamentDetails tournamentDetails)
    {
        _context.TournamentDetails.Remove(tournamentDetails);    
    }
}

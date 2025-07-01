using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Tournament.Data.Data;
using System.Reflection.Metadata.Ecma335;

namespace Tournament.Data.Repositories;

public class GameRepository : IGameRepository
{
    private readonly TournamentApiContext _context;

    public GameRepository(TournamentApiContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Game>> GetAllAsync(bool sortByTitle = false)
    {
        var games = await _context.Game            
            .ToListAsync();
        var sortedGames = sortByTitle
            ? games.OrderBy(g => g.Title).ToList() 
            : games;
        return sortedGames;
    }
    public async Task<Game?> GetAsync(int id)
    {
        return await _context.Game.FindAsync(id);
    }
    public async Task<bool> AnyAsync(int id)
    {
        return await _context.Game.AnyAsync(g => g.Id == id);
    }
    public void Add(Game game)
    {
        _context.Game.AddAsync(game);      
    }
    public void Update(Game game)
    {
        _context.Game.Update(game);
    }
    public void Remove(Game game)
    {
        _context.Game.Remove(game);      
    }

    public async Task<Game?> GetAsync(string title) 
    {      
        return await _context.Game.FirstOrDefaultAsync(g => g.Title == title);
    }
}

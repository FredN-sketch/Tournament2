using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Request;

namespace Tournament.Core.Repositories;

public interface IGameRepository
{
    //Task<IEnumerable<Game>> GetAllAsync(bool sortByTitle = false);
    Task<PagedList<Game>> GetAllAsync(GameRequestParams requestParams);
    Task<Game?> GetAsync(int id);
    Task<Game?> GetAsync(string title);
    Task<bool> AnyAsync(int id);
    void Add(Game game);
    void Update(Game game);
    void Remove(Game game);
}

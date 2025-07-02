using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        public Task<bool> AnyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAllAsync(bool sortByTitle = false)
        {
            throw new NotImplementedException();
        }

        public Task<Game?> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Game?> GetAsync(string title)
        {
            throw new NotImplementedException();
        }
    }
}

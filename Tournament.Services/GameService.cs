using AutoMapper;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        private IUnitOfWork uow;
        private IMapper mapper;

        public GameService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

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

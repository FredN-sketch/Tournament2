using AutoMapper;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Repositories;
using Tournament.Core.Request;

namespace Tournament.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await _uow.GameRepository.AnyAsync(id);
        }

        
        public async Task<(IEnumerable<GameDto> gameDtos, MetaData metaData)> GetAllAsync(GameRequestParams requestParams)
        {
            var pagedList = await _uow.GameRepository.GetAllAsync(requestParams);
            var gamesDto = _mapper.Map<IEnumerable<GameDto>>(pagedList.Items);
            return (gamesDto, pagedList.MetaData);

        }

        public async Task<GameDto?> GetAsync(int id)
        {
            var game = await _uow.GameRepository.GetAsync(id);
            if (game == null)
            {
                throw new Tournament.Core.Exceptions.GameNotFoundException(id);
            }
            return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto?> GetAsync(string title)
        {
            var game = await _uow.GameRepository.GetAsync(title);
            if (game == null)
            {
                throw new Tournament.Core.Exceptions.GameNotFoundException(title);
            }
            return _mapper.Map<GameDto>(await _uow.GameRepository.GetAsync(title));
        }

        public async Task<GameDto> PostGame(GameCreateDto dto)
        {
            var game = _mapper.Map<Game>(dto);
            _uow.GameRepository.Add(game);
            await _uow.CompleteAsync();
            return _mapper.Map<GameDto>(game);
        }
        // CRUD operations
        public void Add(Game game)
        {
            _uow.GameRepository.Add(game);
            _uow.CompleteAsync();
        }
        public void Update(Game game)
        {
            _uow.GameRepository.Update(game);
            _uow.CompleteAsync();
        }
        public void Remove(Game game)
        {
            _uow.GameRepository.Remove(game);
            _uow.CompleteAsync();
        }

        public async Task CompleteAsync()
        {
            await _uow.CompleteAsync();
        }
    }
}

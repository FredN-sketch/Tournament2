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

        //public async Task<IEnumerable<GameDto>> GetAllAsync(GameRequestParams requestParams)
        //{
        //    return _mapper.Map<IEnumerable<GameDto>>(await _uow.GameRepository.GetAllAsync(requestParams));

        //}
        public async Task<(IEnumerable<GameDto> gameDtos, MetaData metaData)> GetAllAsync(GameRequestParams requestParams)
        {
            var pagedList = await _uow.GameRepository.GetAllAsync(requestParams);
            var gamesDto = _mapper.Map<IEnumerable<GameDto>>(pagedList.Items);
            return (gamesDto, pagedList.MetaData);

        }

        public async Task<GameDto?> GetAsync(int id)
        {
            return _mapper.Map<GameDto>(await _uow.GameRepository.GetAsync(id));               
        }

        public async Task<GameDto?> GetAsync(string title)
        {
            return _mapper.Map<GameDto>(await _uow.GameRepository.GetAsync(title));
        }
    }
}

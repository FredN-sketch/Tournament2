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
    public class TournamentService : ITournamentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public TournamentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await _uow.TournamentRepository.AnyAsync(id);
        }

    
        public async Task<(IEnumerable<TournamentDto> tournamentDtos, MetaData metaData)> GetAllAsync(TournamentRequestParams requestParams)
        {          
            var pagedList = await _uow.TournamentRepository.GetAllAsync(requestParams);
            var tournamentsDto = _mapper.Map<IEnumerable<TournamentDto>>(pagedList.Items);
            return (tournamentsDto, pagedList.MetaData);
        }

        public async Task<TournamentDto?> GetAsync(int id, bool includeGames = false)
        {
            TournamentDetails? tournament = await _uow.TournamentRepository.GetAsync(id, includeGames);
            return _mapper.Map<TournamentDto?>(tournament);
        }

        // CRUD operations
        public void Add(TournamentDetails tournamentDetails)
        {
            _uow.TournamentRepository.Add(tournamentDetails);
            _uow.CompleteAsync();
        }
        public void Update(TournamentDetails tournamentDetails)
        {
            _uow.TournamentRepository.Update(tournamentDetails);
            _uow.CompleteAsync();
        }
        public void Remove(TournamentDetails tournamentDetails)
        {
            _uow.TournamentRepository.Remove(tournamentDetails);
            _uow.CompleteAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _uow.CompleteAsync();
        }
    }
}

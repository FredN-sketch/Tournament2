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
            int tournamentNrOfGames = await _uow.TournamentRepository.CountGames(dto.TournamentDetailsId);
            if (tournamentNrOfGames >= TournamentDetails.MaxGames)
            {
                throw new Tournament.Core.Exceptions.MaxGamesException(dto.TournamentDetailsId);
            }
            _uow.GameRepository.Add(game);
            await _uow.CompleteAsync();
            return _mapper.Map<GameDto>(game);
        }
        //public async Task<GameDto> PatchGame(int gameId, JsonPatchDocument<GameCreateDto> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        return BadRequest("Invalid patch document.");
        //    }
        //    if (!await GameExistsAsync(gameId))
        //    {
        //        return NotFound("Game does not exist");
        //    }
        //    var existingGame = await _uow.GameRepository.GetAsync(gameId);
        //    if (existingGame == null)
        //    {
        //        return NotFound("Game does not exist");
        //    }
        //    var dto = _mapper.Map<GameCreateDto>(existingGame);
        //    patchDoc.ApplyTo(dto, ModelState);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    _mapper.Map(dto, existingGame);
        //    try
        //    {
        //        await _uow.CompleteAsync();
        //    }
        //    catch
        //    {
        //        return StatusCode(500);
        //    }
        //    return NoContent();
        //}
        // CRUD operations
        //public void Add(Game game)
        //{
        //    _uow.GameRepository.Add(game);
        //    _uow.CompleteAsync();
        //}
        //public void Update(Game game)
        //{
        //    _uow.GameRepository.Update(game);
        //    _uow.CompleteAsync();
        //}
        //public void Remove(Game game)
        //{
        //    _uow.GameRepository.Remove(game);
        //    _uow.CompleteAsync();
        //}

        public async Task CompleteAsync()
        {
            await _uow.CompleteAsync();
        }
        public GameCreateDto MapGame(GameDto gameDto)
        {
            Game game = _mapper.Map<Game>(gameDto);
            GameCreateDto dto = _mapper.Map<GameCreateDto>(game);
            return dto;
        }
        //public GameCreateDto MapGame(object existingGame)
        //{
        //    if (existingGame is Game game)
        //    {
        //        return MapGame(game);
        //    }
        //    throw new ArgumentException("Invalid game object type", nameof(existingGame));
        //}
        public async Task MapGameCreateDto(GameCreateDto dto, GameDto existingGame)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "GameCreateDto cannot be null");
            }
            _mapper.Map(dto, existingGame);
            await _uow.CompleteAsync();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Dto;
using Tournament.Core.Entities;
using Tournament.Core.Request;

namespace Service.Contracts
{
    public interface IGameService
    {
        Task<(IEnumerable<GameDto> gameDtos, MetaData metaData)> GetAllAsync(GameRequestParams requestParams);
        Task<GameDto?> GetAsync(int id);
        Task<GameDto?> GetAsync(string title);
        Task<bool> AnyAsync(int id);
        Task<GameDto> PostGame(GameCreateDto dto);
        Task CompleteAsync();
        GameCreateDto MapGame(Game existingGame);
        Task MapGameCreateDto(GameCreateDto dto, Game existingGame);
        Task<Game> GetGameAsync(int gameId);
        //public void Update(Game game);
    }
}

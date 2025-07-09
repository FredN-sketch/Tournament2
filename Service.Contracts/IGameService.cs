using System;
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
    }
}

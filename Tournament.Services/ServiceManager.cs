using AutoMapper;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournament.Core.Repositories;

namespace Tournament.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITournamentService> tournamentService;
        private readonly Lazy<IGameService> gameService;
        public ITournamentService TournamentService => tournamentService.Value;
        public IGameService GameService => gameService.Value;
        
      //  public ServiceManager(Lazy<ITournamentService> tournamentservice, Lazy<IGameService> gameservice)
        public ServiceManager(IUnitOfWork uow, IMapper mapper)
        {
            tournamentService = new Lazy<ITournamentService>(() => new TournamentService(uow, mapper));
            gameService = new Lazy<IGameService>(() => new GameService(uow, mapper)); ;
        }
    }
}

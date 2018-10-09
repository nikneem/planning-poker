using System;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Poker.Contracts.Repositories;
using HexMaster.PlanningPoker.Poker.Contracts.Services;
using HexMaster.PlanningPoker.Poker.DataTransferObjects;
using HexMaster.PlanningPoker.Poker.DomainModels;
using HexMaster.PlanningPoker.Poker.Mapping;

namespace HexMaster.PlanningPoker.Poker.Services
{
    public class PokerSessionsService: IPokerSessionsService
    {
        public IPokerSessionsRepository Repository { get; }

        public async Task<PokerSessionDto> Create(PokerSessionCreateRequestDto model)
        {
            var pokerSession = PokerSession.Create(model.FirstName, model.LastName, model.SessionName,
                model.ControlType, model.StartType);
            if (await Repository.Create(pokerSession))
            {
                return pokerSession.ToDataTransferObject();
            }

            return null;
        }

        public Task<PokerSessionDto> Join(PokerSessionJoinRequestDto model)
        {
            throw new NotImplementedException();
        }

        public PokerSessionsService(IPokerSessionsRepository repository)
        {
            Repository = repository;
        }
    }
}

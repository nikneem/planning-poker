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

        public async Task<PokerSessionDto> Join(PokerSessionJoinRequestDto model)
        {
            var pokerSession = await Repository.Get(model.SessionCode);
            if (pokerSession != null)
            {
                var participant = Participant.Create(model.FirstName, model.LastName);
                pokerSession.AddParticipant(participant);
            }

            if (await Repository.Update(pokerSession))
            {
                return pokerSession.ToDataTransferObject();
            }

            return null;
        }

        public async Task<bool> Estimate(PokerEstimationDto dto)
        {
            var pokerSession = await Repository.Get(dto.SessionId);
            pokerSession.SetEstimation(dto.ParticipantId, dto.Estimation);
            return await Repository.Update(pokerSession);
        }

        public PokerSessionsService(IPokerSessionsRepository repository)
        {
            Repository = repository;
        }
    }
}

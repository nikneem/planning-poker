using System;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Poker.Contracts.Repositories;
using HexMaster.PlanningPoker.Poker.Contracts.Services;
using HexMaster.PlanningPoker.Poker.DataTransferObjects;
using HexMaster.PlanningPoker.Poker.DomainModels;
using HexMaster.PlanningPoker.Poker.IntegrationEvents;
using HexMaster.PlanningPoker.Poker.IntegrationEvents.Events;
using HexMaster.PlanningPoker.Poker.Mapping;

namespace HexMaster.PlanningPoker.Poker.Services
{
    public class PokerSessionsService : IPokerSessionsService
    {
        public IPokerSessionsRepository Repository { get; }
        public IPlanningPokerEventsService EventService { get; }

        public async Task<PokerSessionDto> Create(PokerSessionCreateRequestDto model)
        {
            var pokerSession = PokerSession.Create(model.FirstName, model.LastName, model.SessionName,
                model.ControlType, model.StartType);
            if (await Repository.Create(pokerSession))
            {
                return pokerSession.ToDataTransferObject(pokerSession.Participants.First().Id);
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


                if (await Repository.Update(pokerSession))
                {
                    var displayName = string.IsNullOrEmpty(participant.LastName)
                        ? participant.FirstName
                        : $"{participant.FirstName} {participant.LastName}";
                    EventService.PublishThroughEventBusAsync(
                        new PokerSessionParticipantJoinedEvent(pokerSession.Id, participant.Id, displayName));
                    return pokerSession.ToDataTransferObject(participant.Id);
                }
            }

            return null;
        }

        public async Task<bool> Leave(PokerSessionLeaveRequestDto model)
        {
            var pokerSession = await Repository.Get(model.PokerSessionId);
            if (pokerSession != null)
            {
                pokerSession.RemoveParticipant(model.ParticipantId);
                if (await Repository.Update(pokerSession))
                {
                    EventService.PublishThroughEventBusAsync(
                        new PokerSessionParticipantLeftEvent(model.PokerSessionId, model.ParticipantId));
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Start(Guid pokerSessionId)
        {
            var pokerSession = await Repository.Get(pokerSessionId);
            if (pokerSession != null)
            {
                pokerSession.Start();
                if (await Repository.Update(pokerSession))
                {
                    EventService.PublishThroughEventBusAsync(
                        new PokerSessionStartedEvent(pokerSessionId));
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Reset(Guid pokerSessionId)
        {
            var pokerSession = await Repository.Get(pokerSessionId);
            if (pokerSession != null)
            {
                pokerSession.Reset();
                if (await Repository.Update(pokerSession))
                {
                    EventService.PublishThroughEventBusAsync(
                        new PokerSessionRoundResetEvent(pokerSessionId));
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> Estimate(PokerEstimationDto dto)
        {
            var pokerSession = await Repository.Get(dto.SessionId);
            pokerSession.SetEstimation(dto.ParticipantId, dto.Estimation);
            var result = await Repository.Update(pokerSession);
            if (result)
            {
                EventService.PublishThroughEventBusAsync(
                    new PokerSessionParticipantEstimatedEvent(pokerSession.Id, dto.ParticipantId, dto.Estimation));
            }

            return result;
        }

        public async Task<PokerSessionDto> Get(Guid sessionId, Guid participantId)
        {
            var pokerSession = await Repository.Get(sessionId);
            if (pokerSession.Participants.Any(x => x.Id == participantId))
            {
                return pokerSession.ToDataTransferObject(participantId);
            }
            return null;
        }

        public PokerSessionsService(IPokerSessionsRepository repository, IPlanningPokerEventsService eventService)
        {
            Repository = repository;
            EventService = eventService;
        }
    }
}
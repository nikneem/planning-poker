using System;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents.Events
{
    public class PokerSessionParticipantLeftEvent : IntegrationEvent
    {
        public Guid PokerSessionId { get; }
        public Guid ParticipantId { get; }

        public PokerSessionParticipantLeftEvent(Guid pokerSessionId, Guid participantId)
        {
            PokerSessionId = pokerSessionId;
            ParticipantId = participantId;
        }
    }
}

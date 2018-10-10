using System;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents.Events
{
    public class PokerSessionParticipantJoinedEvent : IntegrationEvent
    {
        public Guid PokerSessionId { get; }
        public Guid ParticipantId { get; }
        public string DisplayName { get; }

        public PokerSessionParticipantJoinedEvent(Guid pokerSessionId, Guid participantId, string displayName)
        {
            PokerSessionId = pokerSessionId;
            ParticipantId = participantId;
            DisplayName = displayName;
        }
    }
}

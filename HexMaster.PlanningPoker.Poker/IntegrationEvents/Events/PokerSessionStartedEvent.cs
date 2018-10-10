using System;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Poker.IntegrationEvents.Events
{
    public class PokerSessionStartedEvent : IntegrationEvent
    {
        public Guid PokerSessionId { get; }

        public PokerSessionStartedEvent(Guid pokerSessionId)
        {
            PokerSessionId = pokerSessionId;
        }
    }
}

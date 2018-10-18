using System;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents.Events
{
    public class PokerSessionCardRevealEvent : IntegrationEvent
    {
        public Guid PokerSessionId { get; }

        public PokerSessionCardRevealEvent(Guid pokerSessionId)
        {
            PokerSessionId = pokerSessionId;
        }
    }
}

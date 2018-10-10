using System;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents.Events
{
    public class PokerSessionRoundResetEvent : IntegrationEvent
    {
        public Guid PokerSessionId { get; }

        public PokerSessionRoundResetEvent(Guid pokerSessionId) { 
        }
    }
}

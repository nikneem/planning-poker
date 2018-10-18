using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Poker.IntegrationEvents.Events
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

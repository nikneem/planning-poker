using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.BuildingBlocks.EventBus.Abstractions;
using HexMaster.PlanningPoker.Live.Hubs;
using HexMaster.PlanningPoker.Live.IntegrationEvents.Events;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents.Handlers
{
    public class PokerSessionCardRevealEventHandler : IIntegrationEventHandler<PokerSessionCardRevealEvent>
    {
        public IHubContext<PokerSessionHub> Context { get; }

        public PokerSessionCardRevealEventHandler(IHubContext<PokerSessionHub> context)
        {
            Context = context;
        }

        public async Task Handle(PokerSessionCardRevealEvent @event)
        {
            var hub = new PokerSessionHub(Context);
            await hub.RevealCards(@event.PokerSessionId);
        }
    }
}

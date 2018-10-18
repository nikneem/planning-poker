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
    public class PokerSessionRoundResetEventHandler : IIntegrationEventHandler<PokerSessionRoundResetEvent>
    {

        public IHubContext<PokerSessionHub> Context { get; }

        public PokerSessionRoundResetEventHandler(IHubContext<PokerSessionHub> context)
        {
            Context = context;
        }
        public async Task Handle(PokerSessionRoundResetEvent @event)
        {
            var hub = new PokerSessionHub(Context);
            await hub.ResetAll(@event.PokerSessionId);
        }
    }
}

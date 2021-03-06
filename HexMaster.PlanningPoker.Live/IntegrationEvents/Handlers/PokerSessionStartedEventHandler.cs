﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.BuildingBlocks.EventBus.Abstractions;
using HexMaster.PlanningPoker.Live.Hubs;
using HexMaster.PlanningPoker.Live.IntegrationEvents.Events;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents.Handlers
{
    public class PokerSessionStartedEventHandler : IIntegrationEventHandler<PokerSessionStartedEvent>
    {

        public IHubContext<PokerSessionHub> Context { get; }

        public PokerSessionStartedEventHandler(IHubContext<PokerSessionHub> context)
        {
            Context = context;
        }
        public async Task Handle(PokerSessionStartedEvent @event)
        {
            var hub = new PokerSessionHub(Context);
            await hub.SessionStarted(@event.PokerSessionId);
        }
    }
}

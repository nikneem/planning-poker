using System;
using System.Threading.Tasks;
using HexMaster.BuildingBlocks.EventBus.Abstractions;
using HexMaster.PlanningPoker.Live.Hubs;
using HexMaster.PlanningPoker.Live.IntegrationEvents.Events;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents.Handlers
{
    public class PokerSessionParticipantEstimatedEventHandler : IIntegrationEventHandler<PokerSessionParticipantEstimatedEvent>
    {
        public IHubContext<PokerSessionHub> Context { get; }

        public PokerSessionParticipantEstimatedEventHandler(IHubContext<PokerSessionHub> context)
        {
            Context = context;
        }

        public async Task Handle(PokerSessionParticipantEstimatedEvent @event)
        {
            Console.WriteLine("ESTIMATE EVENT ARRIVED AT TOPIC SUBSCRIPTION");
            var hub = new PokerSessionHub(Context);
            await hub.ParticipantEstimated(@event.PokerSessionId, @event.ParticipantId, @event.PokerValue);
        }
    }
}

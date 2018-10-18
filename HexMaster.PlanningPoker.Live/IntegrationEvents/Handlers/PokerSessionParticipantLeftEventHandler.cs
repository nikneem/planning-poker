using System.Threading.Tasks;
using HexMaster.BuildingBlocks.EventBus.Abstractions;
using HexMaster.PlanningPoker.Live.Hubs;
using HexMaster.PlanningPoker.Live.IntegrationEvents.Events;
using Microsoft.AspNetCore.SignalR;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents.Handlers
{
    public class PokerSessionParticipantLeftEventHandler : IIntegrationEventHandler<PokerSessionParticipantLeftEvent>
    {

        public IHubContext<PokerSessionHub> Context { get; }

        public PokerSessionParticipantLeftEventHandler(IHubContext<PokerSessionHub> context)
        {
            Context = context;
        }
        public async Task Handle(PokerSessionParticipantLeftEvent @event)
        {
            var hub = new PokerSessionHub(Context);
            await hub.ParticipantLeft(@event.PokerSessionId, @event.ParticipantId);
        }
    }
}

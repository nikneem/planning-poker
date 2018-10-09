using System;
using HexMaster.BuildingBlocks.EventBus.Abstractions;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Poker.IntegrationEvents
{
    public class PlanningPokerEventsService : IPlanningPokerEventsService
    {
        private readonly IEventBus _eventBus;

        public PlanningPokerEventsService(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public void PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);
        }
    }
}

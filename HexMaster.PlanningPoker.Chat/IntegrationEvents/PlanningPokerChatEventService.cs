using System;
using HexMaster.BuildingBlocks.EventBus.Abstractions;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Chat.IntegrationEvents
{
    public class PlanningPokerChatEventService : IPlanningPokerChatEventService
    {
        private readonly IEventBus _eventBus;

        public PlanningPokerChatEventService(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public void PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            _eventBus.Publish(evt);
        }
    }
}

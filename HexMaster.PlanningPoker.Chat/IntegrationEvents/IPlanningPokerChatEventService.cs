using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Chat.IntegrationEvents
{
    public interface IPlanningPokerChatEventService
    {
        void PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
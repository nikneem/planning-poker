using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Poker.IntegrationEvents
{
    public interface IPlanningPokerEventsService
    {
        void PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
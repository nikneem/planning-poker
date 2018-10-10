using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Live.IntegrationEvents
{
    public interface IPlanningPokerEventsService
    {
        void PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
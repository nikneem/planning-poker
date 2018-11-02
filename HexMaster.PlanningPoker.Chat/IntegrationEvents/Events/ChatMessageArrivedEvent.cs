using System;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Chat.IntegrationEvents.Events
{
    public class ChatMessageArrivedEvent : IntegrationEvent
    {
        public Guid MessageId { get; }
        public Guid ChannelId { get; }
        public Guid RecipientId { get; }
        public string SenderName { get; }
        public string Message { get; }
        public DateTimeOffset CreatedOn { get; }

        public ChatMessageArrivedEvent(Guid id, Guid channelId, Guid recipientId, string senderName, string message,
            DateTimeOffset createdOn)
        {
            MessageId = id;
            ChannelId = channelId;
            RecipientId = recipientId;
            SenderName = senderName;
            Message = message;
            CreatedOn = createdOn;
        }
    }
}

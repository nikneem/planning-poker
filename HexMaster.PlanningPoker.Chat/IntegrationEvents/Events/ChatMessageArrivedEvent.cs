using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.BuildingBlocks.EventBus.Events;

namespace HexMaster.PlanningPoker.Chat.IntegrationEvents.Events
{
    public class ChatMessageArrivedEvent : IntegrationEvent
    {
        public Guid Id { get; }
        public Guid ChannelId { get; }
        public Guid RecipientId { get; }
        public string SenderName { get; }
        public string Message { get; }
        public DateTimeOffset CreatedOn { get; }

        public ChatMessageArrivedEvent(Guid id, Guid channelId, Guid recipientId, string senderName, string message,
            DateTimeOffset createdOn)
        {
            Id = id;
            ChannelId = channelId;
            RecipientId = recipientId;
            SenderName = senderName;
            Message = message;
            CreatedOn = createdOn;
        }
    }
}

using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;
using System;

namespace HexMaster.PlanningPoker.Chat.DomainModel
{
    public sealed class ChatMessage : DomainModelBase<Guid>
    {

        public Guid ChannelId { get; set; }
        public Guid ParticipantId { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public bool IsOriginator { get; set; }
        public bool MarkedAsNew { get; set; }
        public DateTimeOffset CreatedOn { get; private set; }

        public ChatMessage(Guid id, TrackingState state = TrackingState.Unchanged) : base(id, state)
        {
        }

        private ChatMessage(Guid channelId, Guid participantId, string senderName, string message, bool isOriginator) : base(Guid.NewGuid(), TrackingState.Added)
        {
            ChannelId = channelId;
            ParticipantId = participantId;
            SenderName = senderName;
            Message = message;
            IsOriginator = isOriginator;
            MarkedAsNew = !isOriginator;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public static ChatMessage Create(Guid channelId, Guid participantId, string senderName, string message,
            bool isOriginator)
        {
            return new ChatMessage(channelId, participantId, senderName, message, isOriginator);
        }

        public void Delete()
        {
            SetState(TrackingState.Deleted);
        }
    }
}

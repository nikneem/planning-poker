using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;
using System;

namespace HexMaster.PlanningPoker.Chat.DomainModel
{
    public sealed class ChatMessage : DomainModelBase<Guid>
    {

        public Guid ParticipantId { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public bool IsOriginator { get; set; }
        public bool MarkedAsNew { get; set; }
        public DateTimeOffset CreatedOn { get; private set; }

        public ChatMessage(Guid id,  Guid participantId, string senderName, string message, bool isOriginator, bool isNew, DateTimeOffset createdOn) : base(id)
        {
            ParticipantId = participantId;
            SenderName = senderName;
            Message = message;
            IsOriginator = isOriginator;
            MarkedAsNew = isNew;
            CreatedOn = createdOn;
        }

        private ChatMessage( Guid participantId, string senderName, string message, bool isOriginator) : base(Guid.NewGuid(), TrackingState.Added)
        {
            ParticipantId = participantId;
            SenderName = senderName;
            Message = message;
            IsOriginator = isOriginator;
            MarkedAsNew = !isOriginator;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public static ChatMessage Create( Guid participantId, string senderName, string message,
            bool isOriginator)
        {
            return new ChatMessage( participantId, senderName, message, isOriginator);
        }

        public void Delete()
        {
            SetState(TrackingState.Deleted);
        }
    }
}

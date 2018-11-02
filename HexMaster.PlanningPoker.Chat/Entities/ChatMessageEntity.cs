using System;

namespace HexMaster.PlanningPoker.Chat.Entities
{
    public class ChatMessageEntity
    {
        public Guid Id { get; set; }
        public Guid ParticipantId { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public bool IsOriginator { get; set; }
        public bool MarkedAsNew { get; set; }
        public DateTimeOffset CreatedOn { get;  set; }
    }
}
using System;

namespace HexMaster.PlanningPoker.Chat.DataTransferObjects
{
    public class AddChatMessageDto
    {
        public Guid ChannelId { get; set; }
        public Guid ParticipantId { get;  set; }
        public string Message { get; set; }
    }
}

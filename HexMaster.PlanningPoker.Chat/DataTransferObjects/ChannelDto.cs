using System;
using System.Collections.Generic;

namespace HexMaster.PlanningPoker.Chat.DataTransferObjects
{
    public class ChannelDto
    {
        public Guid Id { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}

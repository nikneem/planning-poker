using System;

namespace HexMaster.PlanningPoker.Poker.DataTransferObjects
{
    public class PokerSessionLeaveRequestDto
    {
        public Guid PokerSessionId { get; set; }
        public Guid ParticipantId { get; set; }
    }
}

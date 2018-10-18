using System;

namespace HexMaster.PlanningPoker.Poker.DataTransferObjects
{
    public class PokerSessionLeaveRequestDto
    {
        public Guid SessionId { get; set; }
        public Guid ParticipantId { get; set; }
    }
}

using System;

namespace HexMaster.PlanningPoker.Poker.DataTransferObjects
{
    public class PokerEstimationDto
    {
        public Guid SessionId { get; set; }
        public Guid ParticipantId { get; set; }
        public decimal Estimation { get; set; }
    }
}
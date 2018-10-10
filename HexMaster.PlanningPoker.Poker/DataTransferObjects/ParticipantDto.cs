using System;

namespace HexMaster.PlanningPoker.Poker.DataTransferObjects
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public bool CanEdit { get; set; }
        public decimal? Estimation { get; set; }
    }
}

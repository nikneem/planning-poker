using System;

namespace HexMaster.PlanningPoker.Refinements.Entity
{
    public class VoteEntity
    {
        public Guid Id { get; set; }
        public Guid InviteeId { get; set; }
        public int StoryPoints { get; set; }
        public DateTimeOffset VoteCastedOn { get; set; }
    }
}

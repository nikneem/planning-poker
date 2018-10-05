using System;

namespace HexMaster.PlanningPoker.Refinements.DataTransferObjects
{
    public class VoteDto
    {
        public Guid Id { get; set; }

        public Guid InviteeId { get; set; }
        public int StoryPoints { get; set; }
        public DateTimeOffset VoteCastedOn { get; set; }


    }
}

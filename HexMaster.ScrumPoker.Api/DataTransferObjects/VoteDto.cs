using System;
using HexMaster.Helpers.DomainModels;

namespace HexMaster.ScrumPoker.Api.DataTransferObjects
{
    public class VoteDto
    {
        public Guid Id { get; set; }

        public Guid InviteeId { get; set; }
        public int StoryPoints { get; set; }
        public DateTimeOffset VoteCastedOn { get; set; }


    }
}

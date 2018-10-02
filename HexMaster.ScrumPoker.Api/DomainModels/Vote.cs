using System;
using HexMaster.Helpers.DomainModels;

namespace HexMaster.ScrumPoker.Api.DomainModels
{
    public class Vote : DomainModelBase<Guid>
    {
        public Guid InviteeId { get; set; }
        public int StoryPoints { get; set; }
        public DateTimeOffset VoteCastedOn { get; set; }

        public Vote(Guid id, Guid invitee, int storyPoints, DateTimeOffset castedOn) : base(id)
        {
            InviteeId = invitee;
            StoryPoints = storyPoints;
            VoteCastedOn = castedOn;
        }
    }
}

using System.Linq;
using HexMaster.ScrumPoker.Api.DomainModels;
using HexMaster.ScrumPoker.Api.Entity;

namespace HexMaster.ScrumPoker.Api.Mapping
{
    public static class RefinementMapping
    {

        public static Refinement ToDomainModel(this RefinementEntity entity)
        {
            var invitees = entity.Invitees.Select(ToDomainModel).ToList();
            var productBacklogItems = entity.Subjects.Select(ToDomainModel).ToList();
            return new Refinement(entity.Id, entity.Name, entity.InvitationCode, invitees, productBacklogItems,
                entity.IsOpen, entity.IsStarted, entity.CreatedOn);
        }

        public static Invitee ToDomainModel(this InviteeEntity entity)
        {
            return new Invitee(entity.Id, entity.Email, entity.DisplayName, entity.IsActive);
        }

        public static ProductBacklogItem ToDomainModel(this ProductBacklogItemEntity entity)
        {
            var votes = entity.Votes.Select(ToDomainModel).ToList();
            return new ProductBacklogItem(entity.Id, entity.Name, entity.Title, entity.Description, entity.LinkUrl,
                entity.IsRefined, entity.StoryPoints, votes);
        }

        public static Vote ToDomainModel(this VoteEntity entity)
        {
            return new Vote(entity.Id, entity.InviteeId, entity.StoryPoints, entity.VoteCastedOn);
        }
    }
}
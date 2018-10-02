using System.Linq;
using HexMaster.ScrumPoker.Api.DomainModels;
using HexMaster.ScrumPoker.Api.Entity;
using MongoDB.Bson;

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


        public static RefinementEntity ToEntity(this Refinement domainModel)
        {
            var invitees = domainModel.Invitees.Select(ToEntity).ToList();
            var productBacklogItems = domainModel.ProductBacklogItems.Select(ToEntity).ToList();
            return new RefinementEntity()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                InvitationCode = domainModel.InvitationCode,
                IsOpen = domainModel.IsOpen,
                IsStarted = domainModel.IsStarted,
                CreatedOn = domainModel.CreatedOn,
                Invitees = invitees,
                Subjects = productBacklogItems
            };
        }

        public static InviteeEntity ToEntity(this Invitee domainModel)
        {
            return new InviteeEntity
            {
                Id = domainModel.Id,
                Email = domainModel.Email,
                DisplayName = domainModel.DisplayName,
                IsActive = domainModel.IsActive
            };
        }

        public static ProductBacklogItemEntity ToEntity(this ProductBacklogItem domainModel)
        {
            var votes = domainModel.Votes.Select(ToEntity).ToList();
            return new ProductBacklogItemEntity
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Description = domainModel.Description,
                LinkUrl = domainModel.LinkUrl,
                StoryPoints = domainModel.StoryPoints,
                Title = domainModel.Title,
                IsRefined = domainModel.IsRefined,
                Votes = votes
            };
        }

        public static VoteEntity ToEntity(this Vote domainModel)
        {
            return new VoteEntity
            {
                Id = domainModel.Id,
                StoryPoints = domainModel.StoryPoints,
                VoteCastedOn = domainModel.VoteCastedOn,
                InviteeId = domainModel.InviteeId
            };
        }
    }
}
using System;
using System.Linq;
using HexMaster.Helpers.Infrastructure.Enums;
using HexMaster.PlanningPoker.Chat.DataTransferObjects;
using HexMaster.PlanningPoker.Chat.DomainModel;
using HexMaster.PlanningPoker.Chat.Entities;

namespace HexMaster.PlanningPoker.Chat.Mappings
{
    public static class ChatMappings
    {

        public static Channel ToDomainModel(this ChannelEntity entity)
        {
            var participants = entity.Participants.Select(ToDomainModel).ToList();
            var messages = entity.ChatMessages.Select(ToDomainModel).ToList();
            return new Channel(entity.Id, participants, messages);
        }
        public static Participant ToDomainModel(this ParticipantEntity entity)
        {
            return new Participant(entity.Id, entity.Name);
        }
        public static ChatMessage ToDomainModel(this ChatMessageEntity entity)
        {
            return new ChatMessage(entity.Id, entity.ParticipantId, entity.SenderName, entity.Message,
                entity.IsOriginator, entity.MarkedAsNew, entity.CreatedOn);
        }


        public static ChannelEntity ToEntity(this Channel domainModel)
        {
            var participants = domainModel.Participants
                .Where(x=> x.State != TrackingState.Deleted)
                .Select(ToEntity).ToList();
            var messages = domainModel.Messages
                .Where(x => x.State != TrackingState.Deleted)
                .Select(ToEntity).ToList();
            return new ChannelEntity
            {
                Id = domainModel.Id,
                Participants = participants,
                ChatMessages = messages
            };
        }
        public static ParticipantEntity ToEntity(this Participant domainModel)
        {
            return new ParticipantEntity()
            {
                Id = domainModel.Id,
                Name = domainModel.Name
            };
        }
        public static ChatMessageEntity ToEntity(this ChatMessage domainModel)
        {
            return new ChatMessageEntity
            {
                Id = domainModel.Id,
                ParticipantId = domainModel.ParticipantId,
                MarkedAsNew = domainModel.MarkedAsNew,
                SenderName = domainModel.SenderName,
                Message = domainModel.Message,
                IsOriginator = domainModel.IsOriginator,
                CreatedOn = domainModel.CreatedOn
            };
        }


        public static ChannelDto ToDataTransferObject(this Channel domainModel, Guid self)
        {
            var messages = domainModel.Messages
                .Where(m=> m.ParticipantId==self)
                .OrderByDescending(m=> m.CreatedOn)
                .Select(ToDataTransferObject).ToList();
            return new ChannelDto()
            {
                Id = domainModel.Id,
                Messages=messages
            };
        }
        public static MessageDto ToDataTransferObject(this ChatMessage domainModel)
        {
            return new MessageDto()
            {
                Id = domainModel.Id,
                Author = domainModel.SenderName,
                Message = domainModel.Message,
                SentOn = domainModel.CreatedOn,
                IsNew = domainModel.MarkedAsNew
            };
        }

    }
}
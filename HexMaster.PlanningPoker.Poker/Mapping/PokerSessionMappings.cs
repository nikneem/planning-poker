using System;
using System.Linq;
using HexMaster.Helpers.Infrastructure.Enums;
using HexMaster.PlanningPoker.Poker.DataTransferObjects;
using HexMaster.PlanningPoker.Poker.DomainModels;
using HexMaster.PlanningPoker.Poker.Entities;

namespace HexMaster.PlanningPoker.Poker.Mapping
{
    public static class PokerSessionMappings
    {

        public static PokerSession ToDomainModel(this PokerSessionEntity entity)
        {
            var participants = entity.Participants.Select(ToDomainModel).ToList();
            return new PokerSession(entity.Id, entity.Name, entity.SessionCode, entity.ControlType, participants,
                entity.CreatedOn, entity.StartedOn, entity.ExpiresOn);
        }
        public static Participant ToDomainModel(this ParticipantEntity entity)
        {
            return new Participant(entity.Id, entity.FirstName, entity.Lastname, entity.IsOwner, entity.IsConnected, entity.Estimation, entity.LastActivityOn, entity.SubjectId);
        }


        public static PokerSessionEntity ToEntity(this PokerSession domainModel)
        {
            var participants = domainModel.Participants
                .Where(x=> x.State != TrackingState.Deleted)
                .Select(ToEntity).ToList();
            return new PokerSessionEntity()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                StartedOn = domainModel.StartedOn,
                ControlType = domainModel.ControlType,
                ExpiresOn = domainModel.ExpiresOn,
                CreatedOn = domainModel.CreatedOn,
                SessionCode = domainModel.SessionCode,
                Participants = participants
            };
        }
        public static ParticipantEntity ToEntity(this Participant domainModel)
        {
            return new ParticipantEntity()
            {
                Id = domainModel.Id,
                FirstName = domainModel.FirstName,
                Lastname =  domainModel.LastName,
                IsConnected = domainModel.IsConnected,
                IsOwner = domainModel.IsOwner,
                LastActivityOn = domainModel.LastActivityOn,
                Estimation = domainModel.Estimation,
                SubjectId = domainModel.SubjectId
            };
        }


        public static PokerSessionDto ToDataTransferObject(this PokerSession domainModel, Guid self)
        {
            var participants = domainModel.Participants.Select(ToDataTransferObject).ToList();
            return new PokerSessionDto()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                SessionCode = domainModel.SessionCode,
                FirstActivity = domainModel.CreatedOn,
                IsStarted = domainModel.StartedOn.HasValue,
                LastActivity = domainModel.CreatedOn,
                Me = participants.FirstOrDefault(p => p.Id == self),
                Others = participants.Where(p => p.Id != self).ToList()
            };
        }
        public static ParticipantDto ToDataTransferObject(this Participant domainModel)
        {
            return new ParticipantDto()
            {
                Id = domainModel.Id,
                DisplayName = string.IsNullOrEmpty(domainModel.LastName) ? domainModel.FirstName : $"{domainModel.FirstName} {domainModel.LastName}",
                CanEdit= domainModel.IsOwner,
                Estimation = domainModel.Estimation,
                SubjectId =  domainModel.SubjectId
            };
        }
        
    }
}
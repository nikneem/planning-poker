using System;
using System.Collections.Generic;
using HexMaster.PlanningPoker.Poker.Infrastructure.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HexMaster.PlanningPoker.Poker.Entities
{
    public class PokerSessionEntity 
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string SessionCode { get;  set; }
        public ControlType ControlType { get;  set; }
        public List<ParticipantEntity> Participants { get; set; }
        public DateTimeOffset CreatedOn { get;  set; }
        public DateTimeOffset? StartedOn { get;  set; }
        public DateTimeOffset ExpiresOn { get;  set; }
    }
}

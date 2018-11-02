using System;
using MongoDB.Bson.Serialization.Attributes;

namespace HexMaster.PlanningPoker.Chat.Entities
{
    public class ParticipantEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get;  set; }
    }
}

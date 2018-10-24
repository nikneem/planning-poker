using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace HexMaster.PlanningPoker.Chat.Entities
{
    public class ChannelEntity
    {
        [BsonId]
        public Guid Id { get; set; }

        public List<ParticipantEntity> Participants { get; set; }
        public List<ChatMessageEntity> ChatMessages { get; set; }
    }
}

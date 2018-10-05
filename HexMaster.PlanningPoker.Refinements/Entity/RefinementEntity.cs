using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace HexMaster.PlanningPoker.Refinements.Entity
{
    public class RefinementEntity
    {
        [BsonId] public Guid Id { get; set; }
        public string Name { get; set; }
        public string InvitationCode { get; set; }
        public List<InviteeEntity> Invitees { get; set; }
        public List<ProductBacklogItemEntity> Subjects { get; set; }
        public bool IsOpen { get; set; }
        public bool IsStarted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace HexMaster.PlanningPoker.Poker.Entities
{
    public class ParticipantEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get;  set; }
        public string Lastname { get;  set; }
        public bool IsOwner { get;  set; }
        public int? PokerValue { get;  set; }
        public bool IsConnected { get;  set; }
        public DateTimeOffset LastActivityOn { get;  set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexMaster.PlanningPoker.Poker.DataTransferObjects
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public bool IsOwner { get; set; }
        public int? PokerValue { get; set; }
        public bool IsConnected { get; set; }
        public DateTimeOffset LastActivityOn { get; set; }
    }
}

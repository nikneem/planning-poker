using System;
using System.Collections.Generic;
using HexMaster.PlanningPoker.Poker.Infrastructure.Enums;

namespace HexMaster.PlanningPoker.Poker.DataTransferObjects
{
    public class PokerSessionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SessionCode { get; set; }
        public ParticipantDto Me { get; set; }
        public List<ParticipantDto> Others { get; set; }
        public DateTimeOffset LastActivity { get; set; }
        public DateTimeOffset FirstActivity{ get; set; }
        public bool IsStarted { get; set; }
    }
}

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
        public ControlType ControlType { get; set; }
        public List<ParticipantDto> Participants { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? StartedOn { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
    }
}

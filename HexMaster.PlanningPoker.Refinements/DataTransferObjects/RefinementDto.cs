using System;
using System.Collections.Generic;

namespace HexMaster.PlanningPoker.Refinements.DataTransferObjects
{
    public class RefinementDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string InvitationCode { get; set; }
        public List<InviteeDto> Invitees { get; set; }
        public List<ProductBacklogItemDto> ProductBacklogItems { get; set; }
        public bool IsOpen { get; set; }
        public bool IsStarted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

    }
}

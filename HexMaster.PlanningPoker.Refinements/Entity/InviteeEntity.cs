using System;

namespace HexMaster.PlanningPoker.Refinements.Entity
{
    public class InviteeEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}

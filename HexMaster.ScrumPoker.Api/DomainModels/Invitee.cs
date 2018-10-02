using System;
using HexMaster.Helpers.DomainModels;

namespace HexMaster.ScrumPoker.Api.DomainModels
{
    public class Invitee : DomainModelBase<Guid>
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        public Invitee(Guid id, string email, string displayName, bool isActive):base(id)
        {
            Email = email;
            DisplayName = displayName;
            IsActive = isActive;
        }
    }
}

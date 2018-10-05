using System;
using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;

namespace HexMaster.ScrumPoker.Api.DomainModels
{
    public class Invitee : DomainModelBase<Guid>
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }

        public void SetDisplayName(string value)
        {
            SetState(TrackingState.Touched);
            if (!Equals(DisplayName, value))
            {
                DisplayName = value;
                SetState(TrackingState.Modified);
            }
        }

        public Invitee(Guid id, string email, string displayName, bool isActive):base(id)
        {
            Email = email;
            DisplayName = displayName;
            IsActive = isActive;
        }
        public Invitee(string email) : base(Guid.NewGuid(), TrackingState.Added)
        {
            Email = email;
        }


    }
}

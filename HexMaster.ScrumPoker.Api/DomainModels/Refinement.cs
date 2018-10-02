using System;
using System.Collections.Generic;
using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;

namespace HexMaster.ScrumPoker.Api.DomainModels
{
    public class Refinement : DomainModelBase<Guid>
    {
        public string Name { get; set; }
        public string InvitationCode { get; set; }
        public List<Invitee> Invitees { get; set; }
        public List<ProductBacklogItem> ProductBacklogItems { get; set; }
        public bool IsOpen { get; set; }
        public bool IsStarted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Refinement(Guid id, string name, string code, List<Invitee> invitees, List<ProductBacklogItem> pbis, bool isOpen, bool isStarted, DateTimeOffset createdOn) : base(id)
        {
            Name = name;
            InvitationCode = code;
            Invitees = invitees;
            ProductBacklogItems = pbis;
            IsOpen = isOpen;
            IsStarted = isStarted;
            CreatedOn = createdOn;
        }
    }
}

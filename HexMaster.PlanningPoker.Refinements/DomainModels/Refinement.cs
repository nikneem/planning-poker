using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;
using HexMaster.PlanningPoker.Refinements.Services;

namespace HexMaster.PlanningPoker.Refinements.DomainModels
{
    public class Refinement : DomainModelBase<Guid>
    {

        private List<Invitee> _invitees;
        private List<ProductBacklogItem> _pbis;
        public string Name { get; set; }
        public string InvitationCode { get; private set; }
        public ReadOnlyCollection<Invitee> Invitees => _invitees.AsReadOnly();
        public ReadOnlyCollection<ProductBacklogItem> ProductBacklogItems => _pbis.AsReadOnly();
        public bool IsOpen { get; private set; }
        public bool IsStarted { get; private set; }
        public DateTimeOffset CreatedOn { get; private set; }


        public void SetName(string value)
        {
            SetState(TrackingState.Touched);
            if (!Equals(Name, value))
            {
                Name = value;
                SetState(TrackingState.Modified);
            }
        }
        public void AddInvitee(string email,string displayName)
        {
            var invitee = _invitees.FirstOrDefault(inv => inv.Email == email);
            if (invitee == null)
            {
                invitee= new Invitee(email);
                _invitees.Add(invitee);
            }
            invitee.SetDisplayName(displayName ?? email);
        }
        public void AddProductBacklogItem(string title, string description, string linkUrl)
        {
            var pbi = new ProductBacklogItem(title, description, linkUrl);
            _pbis.Add(pbi);
        }

        public Refinement(Guid id, string name, string code, List<Invitee> invitees, List<ProductBacklogItem> pbis, bool isOpen, bool isStarted, DateTimeOffset createdOn) : base(id)
        {
            Name = name;
            InvitationCode = code;
            _invitees = invitees ?? new List<Invitee>();
            _pbis = pbis ?? new List<ProductBacklogItem>();
            IsOpen = isOpen;
            IsStarted = isStarted;
            CreatedOn = createdOn;
        }
        private Refinement() : base(Guid.NewGuid(), TrackingState.Added)
        {
            _invitees = new List<Invitee>();
            _pbis = new List<ProductBacklogItem>();
        }

        public static Refinement Create(string name)
        {
            var refinement = new Refinement();
            refinement.CreatedOn= DateTimeOffset.UtcNow;
            refinement.InvitationCode = Randomizer.GetRandomRefinementCode();
            refinement.SetName(name);
            return refinement;
        }



    }
}

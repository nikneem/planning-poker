using System;
using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;

namespace HexMaster.PlanningPoker.Poker.DomainModels
{
    public class Participant : DomainModelBase<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsOwner { get; private set; }
        public decimal? Estimation { get; private set; }
        public bool IsConnected { get; private set; }
        public DateTimeOffset LastActivityOn { get; private set; }
        public string SubjectId { get; set; }

        public void SetFirstname(string value)
        {
            SetState(TrackingState.Modified);
            if (!Equals(FirstName, value))
            {
                FirstName = value;
                SetState(TrackingState.Modified);
            }
        }
        public void SetLastname(string value)
        {
            SetState(TrackingState.Modified);
            if (!Equals(LastName, value))
            {
                LastName = value;
                SetState(TrackingState.Modified);
            }
        }

        public void ResetValue()
        {
            if (Estimation.HasValue)
            {
                Estimation = null;
                SetState(TrackingState.Modified);
            }
        }

        public void SetPokerValue(decimal value)
        {
            if (!Equals(Estimation, value))
            {
                Estimation = value;
                SetState(TrackingState.Modified);
                LastActivityOn=DateTimeOffset.UtcNow;
            }
        }

        public void Connect()
        {
            if (!IsConnected)
            {
                IsConnected = true;
                SetState(TrackingState.Modified);
            }
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                IsConnected = false;
                SetState(TrackingState.Modified);
            }
        }



        public Participant(Guid id, string firstName, string lastName, bool isOwner, bool isConnected, decimal? estimation, DateTimeOffset lastActivity, string subjectId = null) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            IsOwner = isOwner;
            IsConnected = isConnected;
            Estimation = estimation;
            LastActivityOn = lastActivity;
            SubjectId = SubjectId;
        }
        private Participant() : base(Guid.NewGuid(), TrackingState.Added)
        {

        }

        public static Participant Create(string firstName, string lastName, bool isOwner = false)
        {
            var participant = new Participant();
            participant.SetFirstname(firstName);
            participant.SetLastname(lastName);
            participant.IsOwner = isOwner;
            return participant;
        }

        public void Delete()
        {
            SetState(TrackingState.Deleted);
        }
    }
}

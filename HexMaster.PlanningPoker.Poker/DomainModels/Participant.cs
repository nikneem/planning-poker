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
        public int? PokerValue { get; private set; }
        public bool IsConnected { get; private set; }
        public DateTimeOffset LastActivityOn { get; private set; }

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
            if (PokerValue.HasValue)
            {
                PokerValue = null;
                SetState(TrackingState.Modified);
            }
        }

        public void SetPokerValue(int value)
        {
            if (!Equals(PokerValue, value))
            {
                PokerValue = value;
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



        public Participant(Guid id, string firstName, string lastName, bool isOwner, bool isConnected, int? pokerCard, DateTimeOffset lastActivity) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            IsOwner = isOwner;
            IsConnected = isConnected;
            PokerValue = pokerCard;
            LastActivityOn = lastActivity;
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
    }
}

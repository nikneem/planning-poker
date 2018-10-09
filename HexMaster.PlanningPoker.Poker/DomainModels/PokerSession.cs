using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;
using HexMaster.PlanningPoker.Poker.Infrastructure.Enums;
using HexMaster.PlanningPoker.Poker.Services;

namespace HexMaster.PlanningPoker.Poker.DomainModels
{
    public class PokerSession : DomainModelBase<Guid>
    {
        private List<Participant> _participants;

        public string Name { get; private set; }
        public string SessionCode { get; private set; }
        public ControlType ControlType { get; private set; }
        public ReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();
        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset? StartedOn { get; private set; }
        public DateTimeOffset ExpiresOn { get; private set; }


        public void AddParticipant(Participant participant)
        {
            if (_participants.All(p => p.Id != participant.Id))
            {
                _participants.Add(participant);
                SetState(TrackingState.Modified);
            }
           }
        public void SetName(string value)
        {
            SetState(TrackingState.Modified);
            if (!Equals(Name, value))
            {
                Name = value;
                SetState(TrackingState.Modified);
            }
        }
        public void SetControlType(ControlType value)
        {
            SetState(TrackingState.Modified);
            if (!Equals(ControlType, value))
            {
                ControlType = value;
                SetState(TrackingState.Modified);
            }
        }
        public void Start()
        {
            if (!StartedOn.HasValue)
            {
                StartedOn = DateTimeOffset.UtcNow;
                SetState(TrackingState.Modified);
            }
        }
        public void Reset()
        {
            _participants.ForEach(p => p.ResetValue());
            SetState(TrackingState.Modified);
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(3);
        }


        public PokerSession(Guid id, string name, string sessionCode, ControlType control, List<Participant> participants, DateTimeOffset created, DateTimeOffset? started, DateTimeOffset expires) : base(id)
        {
            Name = name;
            SessionCode = sessionCode;
            ControlType = control;
            _participants = participants ?? new List<Participant>();
            CreatedOn = created;
            StartedOn = started;
            ExpiresOn = expires;
        }
        private PokerSession() : base(Guid.NewGuid(), TrackingState.Added)
        {
            _participants=new List<Participant>();
            SessionCode = Randomizer.GetRandomRefinementCode();
        }

        public static PokerSession Create(string firstName, string lastName, string sessionName, ControlType type, StartType start)
        {
            var participant = Participant.Create(firstName, lastName, true);

            var pokerSession = new PokerSession
            {
                CreatedOn = DateTimeOffset.UtcNow, ExpiresOn = DateTimeOffset.UtcNow.AddHours(3)
            };
            pokerSession.AddParticipant(participant);
            pokerSession.SetName(sessionName);
            pokerSession.SetControlType(type);
            if (start == StartType.Automatically)
            {
                pokerSession.Start();
            }

            return pokerSession;
        }


    }
}

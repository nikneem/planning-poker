using System;
using System.Collections.Generic;
using System.Linq;
using HexMaster.Helpers.DomainModels;
using HexMaster.Helpers.Infrastructure.Enums;
using HexMaster.PlanningPoker.Chat.DataTransferObjects;
using HexMaster.PlanningPoker.Chat.IntegrationEvents.Events;

namespace HexMaster.PlanningPoker.Chat.DomainModel
{
    public sealed class Channel : DomainModelBase<Guid>
    {

        private readonly List<Participant> _participants;
        private readonly List<ChatMessage> _messages;

        public IReadOnlyCollection<Participant> Participants => _participants.AsReadOnly();
        public IReadOnlyCollection<ChatMessage> Messages => _messages.AsReadOnly();


        public void AddParticipant(Participant participant)
        {
            if (_participants.All(p => p.Id != participant.Id))
            {
                _participants.Add(participant);
                SetState(TrackingState.Modified);
            }
        }
        public void RemoveParticipant(Guid participantId)
        {
            var participant = _participants.FirstOrDefault(p => p.Id != participantId);
            if (participant != null)
            {
                _participants.Remove(participant);
                SetState(TrackingState.Modified);
            }
        }
        public void AddMessage(ChatMessage msg)
        {
                    _messages.Add(msg);
        }


        public Channel(Guid id, List<Participant> participants, List<ChatMessage> messages, TrackingState state = TrackingState.Unchanged) : base(id, state)
        {
            _participants = participants ?? new List<Participant>();
            _messages = messages ?? new List<ChatMessage>();
        }
        private Channel(Guid id, Participant participant) : base(id,  TrackingState.Added)
        {
            _participants =  new List<Participant>();
            _messages =  new List<ChatMessage>();
            AddParticipant(participant);
        }


    }
}

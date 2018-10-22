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
        public void AddMessage(AddChatMessageDto dto)
        {
            var eventList = new List<ChatMessageArrivedEvent>();
            var sender = _participants.FirstOrDefault(p => p.Id == dto.ParticipantId);
            if (sender != null)
            {
                foreach (var participant in _participants)
                {
                    var message = ChatMessage.Create(
                        dto.ChannelId, 
                        participant.Id, 
                        sender.Name,
                        dto.Message,
                        sender.Id == participant.Id);
                    _messages.Add(message);
                    //eventList.Add(new ChatMessageArrivedEvent(message.Id, message.ChannelId, participant.Id, sender.Name, message.Message, message.CreatedOn));

                    var removableMessages = _messages
                        .Where(msg => msg.ParticipantId == participant.Id)
                        .OrderByDescending(msg => msg.CreatedOn)
                        .Skip(40)
                        .ToList();
                    removableMessages.ForEach(m => m.Delete());
                }
            }
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

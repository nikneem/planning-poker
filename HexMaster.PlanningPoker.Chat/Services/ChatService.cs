using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Chat.Contracts.Repositories;
using HexMaster.PlanningPoker.Chat.Contracts.Services;
using HexMaster.PlanningPoker.Chat.DataTransferObjects;
using HexMaster.PlanningPoker.Chat.DomainModel;
using HexMaster.PlanningPoker.Chat.IntegrationEvents;
using HexMaster.PlanningPoker.Chat.IntegrationEvents.Events;
using HexMaster.PlanningPoker.Chat.Mappings;

namespace HexMaster.PlanningPoker.Chat.Services
{
    public class ChatService : IChatService
    {
        public IChatRepository ChatRepository { get; }
        public IPlanningPokerChatEventService EventService { get; }


        public async Task<ChannelDto> Get(Guid channelId, Guid participantId)
        {
            var channel = await ChatRepository.Get(channelId);
            return channel.ToDataTransferObject(participantId);
        }

        public async Task <bool> AddMessage(AddChatMessageDto dto)
        {
            var channel = await ChatRepository.Get(dto.ChannelId);
            if (channel != null)
            {
                var eventList = new List<ChatMessageArrivedEvent>();
                var sender = channel.Participants.FirstOrDefault(p => p.Id == dto.ParticipantId);
                if (sender != null)
                {
                    foreach (var participant in channel.Participants)
                    {
                        var message = ChatMessage.Create(
                            
                            participant.Id,
                            sender.Name,
                            dto.Message,
                            sender.Id == participant.Id);
                        channel.AddMessage(message);
                        eventList.Add(new ChatMessageArrivedEvent(message.Id, channel.Id, participant.Id, sender.Name, message.Message, message.CreatedOn));

                        var removableMessages = channel.Messages
                            .Where(msg => msg.ParticipantId == participant.Id)
                            .OrderByDescending(msg => msg.CreatedOn)
                            .Skip(40)
                            .ToList();
                        removableMessages.ForEach(m => m.Delete());
                    }
                }

                var result = await ChatRepository.Update(channel);
                if (result)
                {
                    foreach (var evt in eventList)
                    {
                        EventService.PublishThroughEventBusAsync(evt);
                    }
                }

                return result;
            }

            return false;
        }



        public ChatService(IChatRepository chatRepository, IPlanningPokerChatEventService eventService)
        {
            ChatRepository = chatRepository;
            EventService = eventService;
        }
    }
}

using System;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Chat.DataTransferObjects;

namespace HexMaster.PlanningPoker.Chat.Contracts.Services
{
    public interface IChatService
    {
        Task <bool> AddMessage(AddChatMessageDto dto);
        Task<ChannelDto> Get(Guid channelId, Guid participantId);
    }
}
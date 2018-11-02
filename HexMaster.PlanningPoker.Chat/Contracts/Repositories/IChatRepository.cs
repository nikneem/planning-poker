using System;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Chat.DomainModel;

namespace HexMaster.PlanningPoker.Chat.Contracts.Repositories
{
    public interface IChatRepository
    {
        Task<Channel> Get(Guid sessionId);
        Task<bool> Create(Channel user);
        Task<bool> Update(Channel model);
    }
}
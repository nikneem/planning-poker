using System;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Poker.DomainModels;

namespace HexMaster.PlanningPoker.Poker.Contracts.Repositories
{
    public interface IPokerSessionsRepository
    {
        Task<bool> Create(PokerSession model);
        Task<bool> Update(PokerSession model);
        Task<PokerSession> Get(string sessionCode);
        Task<PokerSession> Get(Guid sessionId);
    }
}
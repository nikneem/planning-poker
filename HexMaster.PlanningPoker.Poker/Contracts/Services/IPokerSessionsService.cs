using System;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Poker.DataTransferObjects;

namespace HexMaster.PlanningPoker.Poker.Contracts.Services
{
    public interface IPokerSessionsService
    {
        Task<PokerSessionDto> Create(PokerSessionCreateRequestDto model);
        Task<PokerSessionDto> Join(PokerSessionJoinRequestDto model);
        Task<bool> Leave(PokerSessionLeaveRequestDto model);
        Task<bool> Start(Guid pokerSessionId);
        Task<bool> Reset(Guid pokerSessionId);
        Task<bool> Estimate(PokerEstimationDto dto);
    }
}
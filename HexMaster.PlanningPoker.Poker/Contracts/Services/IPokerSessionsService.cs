using System.Threading.Tasks;
using HexMaster.PlanningPoker.Poker.DataTransferObjects;

namespace HexMaster.PlanningPoker.Poker.Contracts.Services
{
    public interface IPokerSessionsService
    {
        Task<PokerSessionDto> Create(PokerSessionCreateRequestDto model);
        Task<PokerSessionDto> Join(PokerSessionJoinRequestDto model);

    }
}
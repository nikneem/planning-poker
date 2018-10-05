using System.Threading.Tasks;
using HexMaster.PlanningPoker.Refinements.DataTransferObjects;

namespace HexMaster.PlanningPoker.Refinements.Contracts.Services
{
    public interface IRefinementsService
    {
        Task<RefinementDto> Read(string code);
        Task<RefinementDto> Create(RefinementDto dto);
    }
}
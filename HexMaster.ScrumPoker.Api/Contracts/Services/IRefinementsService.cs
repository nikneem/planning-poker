using System.Threading.Tasks;
using HexMaster.ScrumPoker.Api.DataTransferObjects;

namespace HexMaster.ScrumPoker.Api.Contracts.Services
{
    public interface IRefinementsService
    {
        Task<RefinementDto> Read(string code);
        Task<RefinementDto> Create(RefinementDto dto);
    }
}
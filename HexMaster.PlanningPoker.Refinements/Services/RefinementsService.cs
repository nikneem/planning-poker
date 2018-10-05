using System.Threading.Tasks;
using HexMaster.PlanningPoker.Refinements.Contracts.Repositories;
using HexMaster.PlanningPoker.Refinements.Contracts.Services;
using HexMaster.PlanningPoker.Refinements.DataTransferObjects;
using HexMaster.PlanningPoker.Refinements.DomainModels;
using HexMaster.PlanningPoker.Refinements.Mapping;

namespace HexMaster.PlanningPoker.Refinements.Services
{
    public class RefinementsService: IRefinementsService
    {
        private readonly IRefinementsRepository _repository;

        public RefinementsService(IRefinementsRepository repository)
        {
            _repository = repository;
        }

        public async Task<RefinementDto> Read(string code)
        {
            return null;
        }

        public async Task<RefinementDto> Create(RefinementDto dto)
        {
            var refinement = Refinement.Create(dto.Name);
            foreach (var invitee in dto.Invitees)
            {
                refinement.AddInvitee(invitee.DisplayName, invitee.Email);
            }
            foreach (var pbi in dto.ProductBacklogItems)
            {
                refinement.AddProductBacklogItem(pbi.Title, pbi.Description, pbi.LinkUrl);
            }

            if (await _repository.Create(refinement))
            {
                return refinement.ToDataTransferObject();
            }

            return null;
        }
    }
}

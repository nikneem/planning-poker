using System;
using System.Threading.Tasks;
using HexMaster.ScrumPoker.Api.Contracts.Repositories;
using HexMaster.ScrumPoker.Api.Contracts.Services;
using HexMaster.ScrumPoker.Api.DataTransferObjects;
using HexMaster.ScrumPoker.Api.DomainModels;
using HexMaster.ScrumPoker.Api.Mapping;

namespace HexMaster.ScrumPoker.Api.Services
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

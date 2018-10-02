using System;
using System.Threading.Tasks;
using HexMaster.ScrumPoker.Api.DomainModels;

namespace HexMaster.ScrumPoker.Api.Contracts.Repositories
{
    public interface IRefinementsRepository
    {
        Task<Refinement> Get(Guid id);
        Task<bool> Create(Refinement user);
        Task<bool> Update(Refinement user);
    }
}
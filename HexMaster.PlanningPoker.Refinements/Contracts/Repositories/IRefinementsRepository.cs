using System;
using System.Threading.Tasks;
using HexMaster.PlanningPoker.Refinements.DomainModels;

namespace HexMaster.PlanningPoker.Refinements.Contracts.Repositories
{
    public interface IRefinementsRepository
    {
        Task<Refinement> Get(Guid id);
        Task<bool> Create(Refinement user);
        Task<bool> Update(Refinement user);
    }
}
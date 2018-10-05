using System;
using System.Threading.Tasks;
using HexMaster.Helpers.Configuration;
using HexMaster.Helpers.Infrastructure.Enums;
using HexMaster.Helpers.Mongo;
using HexMaster.PlanningPoker.Refinements.Contracts.Repositories;
using HexMaster.PlanningPoker.Refinements.DomainModels;
using HexMaster.PlanningPoker.Refinements.Entity;
using HexMaster.PlanningPoker.Refinements.Mapping;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HexMaster.PlanningPoker.Refinements.Repositories
{
    public class RefinementsRepository : MongoRepositoryBase<RefinementEntity>, IRefinementsRepository
    {

        private const string CollectionName = "Refinements";

        public async Task<Refinement> Get(Guid id)
        {
            var filter = Builders<RefinementEntity>.Filter.Eq(nameof(RefinementEntity.Id), id);
            var cursor = await Collection.FindAsync(filter);
            var entity = cursor.FirstOrDefault();

            return entity?.ToDomainModel();
        }

        public async Task<bool> Create(Refinement user)
        {
            if (user.State == TrackingState.Added)
            {
                var entity = user.ToEntity();
                await Collection.InsertOneAsync(entity);
                return true;
            }

            return false;
        }

        public async Task<bool> Update(Refinement user)
        {
            bool updated = false;
            if (user.State == TrackingState.Modified)
            {
                var entity = user.ToEntity();
                var filter = Builders<RefinementEntity>.Filter.Eq(nameof(RefinementEntity.Id), user.Id);
                var result = await Collection.ReplaceOneAsync(filter, entity);
                updated = result.IsAcknowledged;
            }
            if (user.State == TrackingState.Touched)
            {
                updated = true;
            }
            return updated;
        }

        public RefinementsRepository(IOptions<MongoDbSettings> settingsOptions) : base(settingsOptions, CollectionName)
        {
        }
    }
}

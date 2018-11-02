using System;
using System.Threading.Tasks;
using HexMaster.Helpers.Configuration;
using HexMaster.Helpers.Infrastructure.Enums;
using HexMaster.Helpers.Mongo;
using HexMaster.PlanningPoker.Poker.Contracts.Repositories;
using HexMaster.PlanningPoker.Poker.DomainModels;
using HexMaster.PlanningPoker.Poker.Entities;
using HexMaster.PlanningPoker.Poker.Mapping;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HexMaster.PlanningPoker.Poker.Repositories
{
    public class PokerSessionsRepository : MongoRepositoryBase<PokerSessionEntity>, IPokerSessionsRepository
    {

        private const string CollectionName = "planningpoker";



        public async Task<PokerSession> Get(string sessionCode)
        {
            var filter = Builders<PokerSessionEntity>.Filter.Eq(nameof(PokerSessionEntity.SessionCode), sessionCode);
            var cursor = await Collection.FindAsync(filter);
            var entity = cursor.FirstOrDefault();

            return entity?.ToDomainModel();
        }

        public async Task<PokerSession> Get(Guid sessionId)
        {
            var filter = Builders<PokerSessionEntity>.Filter.Eq(nameof(PokerSessionEntity.Id), sessionId);
            var cursor = await Collection.FindAsync(filter);
            var entity = cursor.FirstOrDefault();

            return entity?.ToDomainModel();
        }

        public async Task<bool> Create(PokerSession user)
        {
            if (user.State == TrackingState.Added)
            {
                var entity = user.ToEntity();
                await Collection.InsertOneAsync(entity);
                return true;
            }

            return false;
        }
        public async Task<bool> Update(PokerSession model)
        {
            bool updated = false;
            if (model.State == TrackingState.Modified)
            {
                var entity = model.ToEntity();
                var filter = Builders<PokerSessionEntity>.Filter.Eq(nameof(PokerSessionEntity.Id), model.Id);
                var result = await Collection.ReplaceOneAsync(filter, entity);
                updated = result.IsAcknowledged;
            }

            if (model.State == TrackingState.Touched)
            {
                updated = true;
            }

            return updated;
        }

        public PokerSessionsRepository(IOptions<MongoDbSettings> settingsOptions) : base(settingsOptions, CollectionName)
        {
        }
    }
}

using System;
using System.Threading.Tasks;
using HexMaster.Helpers.Configuration;
using HexMaster.Helpers.Infrastructure.Enums;
using HexMaster.Helpers.Mongo;
using HexMaster.PlanningPoker.Chat.Contracts.Repositories;
using HexMaster.PlanningPoker.Chat.DomainModel;
using HexMaster.PlanningPoker.Chat.Entities;
using HexMaster.PlanningPoker.Chat.Mappings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HexMaster.PlanningPoker.Chat.Repositories
{
    public class ChatRepository : MongoRepositoryBase<ChannelEntity>, IChatRepository
    {

        private const string CollectionName = "planningpoker";



        public async Task<Channel> Get(Guid sessionId)
        {
            var filter = Builders<ChannelEntity>.Filter.Eq(nameof(ChannelEntity.Id), sessionId);
            var cursor = await Collection.FindAsync(filter);
            var entity = cursor.FirstOrDefault();

            return entity?.ToDomainModel();
        }

        public async Task<bool> Create(Channel user)
        {
            if (user.State == TrackingState.Added)
            {
                var entity = user.ToEntity();
                await Collection.InsertOneAsync(entity);
                return true;
            }

            return false;
        }
        public async Task<bool> Update(Channel model)
        {
            bool updated = false;
            if (model.State == TrackingState.Modified)
            {
                var entity = model.ToEntity();
                var filter = Builders<ChannelEntity>.Filter.Eq(nameof(ChannelEntity.Id), model.Id);
                var result = await Collection.ReplaceOneAsync(filter, entity);
                updated = result.IsAcknowledged;
            }

            if (model.State == TrackingState.Touched)
            {
                updated = true;
            }

            return updated;
        }

        public ChatRepository(IOptions<MongoDbSettings> settingsOptions) : base(settingsOptions, CollectionName)
        {
        }
    }
}

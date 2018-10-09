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

        private const string CollectionName = "PokerSessions";

        public async Task<PokerSession> Get(string sessionCode)
        {
            var filter = Builders<PokerSessionEntity>.Filter.Eq(nameof(PokerSessionEntity.SessionCode), sessionCode);
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



        public PokerSessionsRepository(IOptions<MongoDbSettings> settingsOptions) : base(settingsOptions, CollectionName)
        {
        }
    }
}

using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public class RedisWishListRepository:IWishListRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public RedisWishListRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<bool> DeleteWishListAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();
            return data?.Select(k => k.ToString());
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

        public async Task<WishList> GetWishListAsync(string wishListId)
        {
            var data = await _database.StringGetAsync(wishListId);
            if (data.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<WishList>(data);
        }

        public async Task<WishList> UpdateWishListAsync(WishList wishList)
        {
            var created = await _database.StringSetAsync(wishList.BuyerId, JsonConvert.SerializeObject(wishList));
            if (!created)
            {
                //_logger.LogInformation("Problem occured persisting ")
                return null;
            }
            //_logger.LogInformation("wishList event persisted successfully");
            return await GetWishListAsync(wishList.BuyerId);
        }
    }
}

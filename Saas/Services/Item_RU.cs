using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Items.Types;

namespace Saas.Services
{
    internal class Item_RU : ItemSvc.ItemSvcBase
    {
        private readonly ILogger<Item_RU> _logger;
        private readonly string _connectionString;

        public Item_RU(ILogger<Item_RU> logger, IConfiguration config)
        {
            _logger = logger;
            _connectionString = config.GetConnectionString("AllAboutFood");
        }

        public override Task<Item> Get(Int request, ServerCallContext context)
        {
            return Task.FromResult(Item.Read(request.Value, _connectionString));
        }

        public override Task<Items> GetByRestaurantId(Int request, ServerCallContext context)
        {
            return Task.FromResult(Item.ReadByRestaurantId(request.Value, _connectionString));
        }

        public override Task<Int> Update(Item request, ServerCallContext context)
        {
            return Task.FromResult(new Int(Item.Update(request, _connectionString)));
        }
    }
}
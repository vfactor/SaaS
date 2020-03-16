using Grpc.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Items.Types;
using static Saas.Entities.MenuItems.Types;

namespace Saas.Services
{
  internal class ItemService : ItemSvc.ItemSvcBase
  {
      private readonly ILogger<ItemService> _logger;
      private readonly AppData _app;
      public ItemService(ILogger<ItemService> logger, AppData app)
      {
        _logger = logger;
        _app = app;
      }
      public override Task<Item> Get(Int id, ServerCallContext context)
      {      
        var dbContext = Claim.DbContext<Item>(context.GetHttpContext().User, _app.ConnectionString);
        return Task.FromResult(dbContext?.Read(id.Value));
      }
      public override Task<Items> GetByRestaurant(Int restaurantId, ServerCallContext context)
      {        
        var dbContext = Claim.DbContext<Item>(context.GetHttpContext().User, _app.ConnectionString);
        return Task.FromResult(new Items(dbContext?.Read(new Dictionary<string, int> { { Constant.KEYRESTAURANT, restaurantId.Value } })));
      }
      public override Task<Items> GetByMenuDetail(Int menuDetailId, ServerCallContext context)
      {        
        var dbContextMenuItem = Claim.DbContext<MenuItem>(context.GetHttpContext().User, _app.ConnectionString);
        var itemIds = dbContextMenuItem?.Read(new Dictionary<string, int> { { Constant.KEYMENUDETAIL, menuDetailId.Value } }).Select(mi => mi.ItemId);        

        var dbContextItem = Claim.DbContext<Item>(context.GetHttpContext().User, _app.ConnectionString);
        return Task.FromResult(new Items(itemIds.Select(i => dbContextItem?.Read(i))));
      }
      public override Task<Int> Update(Item obj, ServerCallContext context)
      {        
        var dbContext = Claim.DbContext<Item>(context.GetHttpContext().User, _app.ConnectionString);
        return Task.FromResult(new Int(dbContext?.Update(obj)));
      }
  }
}
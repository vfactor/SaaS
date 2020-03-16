using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.RestaurantMenus.Types;
using System.Collections.Generic;

namespace Saas.Services
{
  internal class RestaurantMenuService : RestaurantMenuSvc.RestaurantMenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> _logger;
    private readonly AppData _app;
    public RestaurantMenuService(ILogger<RestaurantMenuService> logger, AppData app)
    {
      _logger = logger;
      _app = app;  
    }
    public override Task<RestaurantMenu> Get(Int id, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<RestaurantMenu>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(dbContext?.Read(id.Value));
    }
    public override Task<RestaurantMenus> GetByRestaurant(Int restaurantId, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<RestaurantMenu>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(new RestaurantMenus(dbContext?.Read(new Dictionary<string, int> { { Constant.KEYRESTAURANT, restaurantId.Value } })));
    }
    public override Task<Int> Create(RestaurantMenu obj, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<RestaurantMenu>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(new Int(dbContext?.Create(obj)));
    }
  }
}
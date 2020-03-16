using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Restaurants.Types;

namespace Saas.Services
{
  internal class RestaurantService : RestaurantSvc.RestaurantSvcBase
  {
    private readonly ILogger<RestaurantService> _logger;
    private readonly AppData _app; 
    public RestaurantService(ILogger<RestaurantService> logger, AppData data)
    {
      _logger = logger;
      _app = data;
    }
    public override Task<Restaurant> Get(Int id, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<Restaurant>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(dbContext?.Read(id.Value));
    }
    public override Task<Restaurants> Lookup(String lookupStr, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<Restaurant>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(new Restaurants(dbContext?.Read(lookupStr.Value)));
    }
    public override Task<Int> Create(Restaurant obj, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<Restaurant>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(new Int(dbContext?.Create(obj)));
    }
    public override Task<Int> Update(Restaurant obj, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<Restaurant>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(new Int(dbContext?.Update(obj)));
    }
    public override Task<Int> Delete(Int id, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<Restaurant>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(new Int(dbContext?.Update(id.Value, _app.States.Delete.Id)));
    }
  }
}
using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Restaurants.Types;

namespace Saas.Services
{
  internal class Restaurant_CRUD : RestaurantSvc.RestaurantSvcBase
  {
    private readonly ILogger<Restaurant_CRUD> _logger;
    private readonly string _connectionStr;

    public Restaurant_CRUD(ILogger<Restaurant_CRUD> logger, IConfiguration config)
    {
      _logger = logger;
      _connectionStr = config.GetConnectionString("AllAboutFood");
    }

    public override Task<Restaurant> Get(Int request, ServerCallContext context)
    {
      return Task.FromResult(Restaurant.Read(request.Value, _connectionStr));
    }

    public override Task<Restaurants> Lookup(String request, ServerCallContext context)
    {
      return Task.FromResult(Restaurant.Lookup(request.Value, _connectionStr));
    }

    public override Task<Int> Create(Restaurant request, ServerCallContext context)
    {
      return Task.FromResult(Restaurant.Create(request, _connectionStr));
    }

    public override Task<Int> Update(Restaurant request, ServerCallContext context)
    {
      return Task.FromResult(Restaurant.Update(request, _connectionStr));
    }

    public override Task<Int> Delete(Int request, ServerCallContext context)
    {
      return Task.FromResult(Restaurant.Delete(request.Value, _connectionStr));
    }
  }
}
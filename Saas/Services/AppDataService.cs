using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entities;

using static Saas.Entities.States.Types;
using static Saas.Entities.Languages.Types;

namespace Saas.Services
{
  internal class AppDataService : AppDataSvc.AppDataSvcBase
  {
    private readonly ILogger<RestaurantService> _logger;
    private readonly AppData _app;
    internal AppDataService(ILogger<RestaurantService> logger, AppData app)
    {
      _logger = logger;
      _app = app;
    }
    public override Task<Languages> Languages(Empty request, ServerCallContext context)
    {      
      return Task.FromResult(_app.Languages);
    }
    public override Task<States> States(Empty request, ServerCallContext context)
    {      
      return Task.FromResult(_app.States);
    }
  }
}
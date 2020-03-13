using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Tables.Types;
using System.Collections.Generic;

namespace Saas.Services
{
  internal class TableService : TableSvc.TableSvcBase
  {
    private readonly ILogger<TableService> _logger;
    private readonly AppData _app;

    public TableService(ILogger<TableService> logger, AppData data)
    {
      _logger = logger;
      _app = data;     
    }
    public override Task<Table> Get(Int id, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<Table>(_app.ConnectionString);

      return Task.FromResult(dbContext?.Read(id.Value));
    }
    public override Task<Tables> GetByRestaurant(Int restaurantId, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<Table>(_app.ConnectionString);

      return Task.FromResult(new Tables(dbContext?.Read(new Dictionary<string, int> { { Constant.KEYRESTAURANT, restaurantId.Value } })));
    }
  }
}
using Grpc.Core;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Tables.Types;

namespace Saas.Services
{
  internal class Table_R : TableSvc.TableSvcBase
  {
    private readonly ILogger<Table_R> _logger;
    private readonly string _connectionStr;

    public Table_R(ILogger<Table_R> logger, IConfiguration config)
    {
      _logger = logger;
      _connectionStr = config.GetConnectionString("AllAboutFood");
    }

    public override Task<Table> Get(Int request, ServerCallContext context)
    {
      return Task.FromResult(Table.Read(request.Value, _connectionStr) as Table);
    }

    public override Task<Tables> GetByRestaurantId(Int request, ServerCallContext context)
    {
      return Task.FromResult(Table.ReadByRestaurantId(request.Value, _connectionStr));
    }
  }
}
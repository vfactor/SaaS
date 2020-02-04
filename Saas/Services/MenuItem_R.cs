using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Linq;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.MenuItems.Types;

namespace Saas.Services
{
  internal class MenuItem_R : MenuItemSvc.MenuItemSvcBase
  {
    private readonly ILogger<MenuItem_R> _logger;
    private readonly string _connectionStr;

    public MenuItem_R(ILogger<MenuItem_R> logger, IConfiguration config)
    {
      _logger = logger;
      _connectionStr = config.GetConnectionString("AllAboutFood");
    }

    public override Task<MenuItem> Get(Int request, ServerCallContext context)
    {
      return Task.FromResult(MenuItem.Read(request.Value, _connectionStr));
    }

    public override Task<MenuItems> GetByMenuId(Int request, ServerCallContext context)
    {
      return Task.FromResult(MenuItem.ReadByMenuId(request.Value, _connectionStr));
    }

    public override Task<MenuItems> GetByItemId(Int request, ServerCallContext context)
    {
      return Task.FromResult(MenuItem.ReadByItemId(request.Value, _connectionStr));
    }

    public override Task<MenuItem> GetByMenuAndItemId(KeyValuePairs request, ServerCallContext context)
    {
      return Task.FromResult(MenuItem.ReadByMenuAndItemId(request.Values.ToDictionary(v => v.Key, v => v.Value), _connectionStr));
    }
  }
}
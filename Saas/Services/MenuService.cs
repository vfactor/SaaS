using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Menus.Types;
using System.Collections.Generic;

namespace Saas.Services
{
  internal class MenuService : MenuSvc.MenuSvcBase
  {
    private readonly ILogger<MenuService> _logger;
    private readonly AppData _app;
    public MenuService(ILogger<MenuService> logger, AppData app)
    {
      _logger = logger;
      _app = app;  
    }
    public override Task<Menu> Get(Int id, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<Menu>(_app.ConnectionString);

      return Task.FromResult(dbContext?.Read(id.Value));
    }
    public override Task<Menus> GetByRestaurant(Int restaurantId, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<Menu>(_app.ConnectionString);

      return Task.FromResult(new Menus(dbContext?.Read(new Dictionary<string, int> { { Constant.KEYRESTAURANT, restaurantId.Value } })));
    }
    public override Task<Int> Create(Menu obj, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<Menu>(_app.ConnectionString);

      return Task.FromResult(new Int(dbContext?.Create(obj)));
    }
  }
}
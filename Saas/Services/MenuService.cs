using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Menus.Types;

namespace Saas.Services
{
  internal class MenuService : MenuSvc.MenuSvcBase
  {
    private readonly ILogger<RestaurantMenuService> _logger;
    private readonly AppData _app;
    public MenuService(ILogger<RestaurantMenuService> logger, AppData app)
    {
      _logger = logger;
      _app = app;
    }
    public override Task<Menu> Get(Int id, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<Menu>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(dbContext?.Read(id.Value));
    }
    public override Task<Menus> GetByRestaurantMenu(Int menuId, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<Menu>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(new Menus(dbContext?.Read(new Dictionary<string, int> { { Constant.KEYMENU, menuId.Value } })));
    }    
    public override Task<Int> Create(Menu obj, ServerCallContext context)
    {      
      var dbContext = Claim.DbContext<Menu>(context.GetHttpContext().User, _app.ConnectionString);
      return Task.FromResult(new Int(dbContext?.Create(obj)));
    }
  }
}
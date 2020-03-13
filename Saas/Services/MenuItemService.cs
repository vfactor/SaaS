using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.MenuItems.Types;
using System.Collections.Generic;

namespace Saas.Services
{
  internal class MenuItemService : MenuItemSvc.MenuItemSvcBase
  {
    private readonly ILogger<MenuItemService> _logger;
    private readonly AppData _app;
    public MenuItemService(ILogger<MenuItemService> logger, AppData app)
    {
      _logger = logger;
      _app = app;   
    }
    public override Task<MenuItem> Get(Int id, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<MenuItem>(_app.ConnectionString);

      return Task.FromResult(dbContext?.Read(id.Value));
    }    
    public override Task<MenuItems> GetByMenuDetailAndItem(MenuItemIds menuItemIds, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<MenuItem>(_app.ConnectionString);
      
      return Task.FromResult(new MenuItems(dbContext?.Read(
                              new Dictionary<string, int>{ { Constant.KEYMENUDETAIL, menuItemIds.MenuDetailId }, { Constant.KEYITEM, menuItemIds.ItemId } })));
    }
    public override Task<MenuItems> GetByItem(Int itemId, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<MenuItem>(_app.ConnectionString);

      return Task.FromResult(new MenuItems(dbContext?.Read(
                              new Dictionary<string, int> { { Constant.KEYITEM, itemId.Value } })));
    }
    public override Task<MenuItems> GetByMenuDetail(Int menuDetailId, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<MenuItem>(_app.ConnectionString);

      return Task.FromResult(new MenuItems(dbContext?.Read(
                              new Dictionary<string, int> { { Constant.KEYMENUDETAIL, menuDetailId.Value } })));

    }
    public override Task<Int> Create(MenuItem obj, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<MenuItem>(_app.ConnectionString);

      return Task.FromResult(new Int(dbContext?.Create(obj)));
    }
  }
}
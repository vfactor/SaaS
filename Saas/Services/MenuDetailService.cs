using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.MenuDetails.Types;

namespace Saas.Services
{
  internal class MenuDetailService : MenuDetailSvc.MenuDetailSvcBase
  {
    private readonly ILogger<MenuDetailService> _logger;
    private readonly AppData _app;
    public MenuDetailService(ILogger<MenuDetailService> logger, AppData app)
    {
      _logger = logger;
      _app = app;
    }
    public override Task<MenuDetail> Get(Int id, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<MenuDetail>(_app.ConnectionString);

      return Task.FromResult(dbContext?.Read(id.Value));
    }
    public override Task<MenuDetails> GetByMenu(Int menuId, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<MenuDetail>(_app.ConnectionString);

      return Task.FromResult(new MenuDetails(dbContext?.Read(new Dictionary<string, int> { { Constant.KEYMENU, menuId.Value } })));
    }    
    public override Task<Int> Create(MenuDetail obj, ServerCallContext context)
    {
      var claim = new Claim(context.GetHttpContext().User);
      var dbContext = claim.GetDBContext<MenuDetail>(_app.ConnectionString);

      return Task.FromResult(new Int(dbContext?.Create(obj)));
    }
  }
}
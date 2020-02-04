﻿using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Menus.Types;

namespace Saas.Services
{
  internal class Menu_CR : MenuSvc.MenuSvcBase
  {
    private readonly ILogger<Menu_CR> _logger;
    private readonly string _connectionStr;

    public Menu_CR(ILogger<Menu_CR> logger, IConfiguration config)
    {
      _logger = logger;
      _connectionStr = config.GetConnectionString("AllAboutFood");
    }

    public override Task<Menu> Get(Int request, ServerCallContext context)
    {
      return Task.FromResult(Menu.Read(request.Value, _connectionStr));
    }

    public override Task<Menus> GetByRestaurantId(Int request, ServerCallContext context)
    {
      return Task.FromResult(Menu.ReadByRestaurantId(request.Value, _connectionStr));
    }

    public override Task<Int> Create(Menu request, ServerCallContext context)
    {
      return Task.FromResult(Menu.Create(request, _connectionStr));
    }
  }
}
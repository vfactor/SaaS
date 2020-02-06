using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Languages.Types;

namespace Saas.Services
{
  internal class ReferenceService : ReferenceSvc.ReferenceSvcBase
  {
    private readonly ILogger<ReferenceService> _logger;
    private readonly string _connectionStr;

    internal ReferenceService(ILogger<ReferenceService> logger, IConfiguration config)
    {
      _logger = logger;
      _connectionStr = config.GetConnectionString("AllAboutFood");
    }

    public override Task<Languages> GetLanguages(Empty request, ServerCallContext context)
    {
      return Task.FromResult(Language.Read(_connectionStr));
    }
  }
}
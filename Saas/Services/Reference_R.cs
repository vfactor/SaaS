using Grpc.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Saas.gRPC;
using Saas.Entities;
using static Saas.Entities.Languages.Types;

namespace Saas.Services
{
  internal class Reference_R : ReferenceSvc.ReferenceSvcBase
  {
    private readonly ILogger<Reference_R> _logger;
    private readonly string _connectionStr;

    internal Reference_R(ILogger<Reference_R> logger, IConfiguration config)
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
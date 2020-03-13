using System.Security.Claims;
using Saas.Entities.DAL;

namespace Saas.Entities
{
  public class Claim
  {
    private readonly ClaimsPrincipal _cp;
    private readonly int _rootId;
    public Claim(ClaimsPrincipal cp)
    {
      _cp = cp;
      _rootId = 1;
    }
    internal DAL<T> GetDBContext<T>(string connectionStr) where T : new()
    {
      return (_rootId > 0) ? new DAL<T>(_rootId, connectionStr) : null;
    }
    internal bool IsAuthenticated() => _cp.Identity.IsAuthenticated;
    internal bool IsInRole(string role) => _cp.IsInRole(role);    
  }
}

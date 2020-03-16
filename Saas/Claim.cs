using Saas.Entities.DAL;
using System.Security.Claims;

namespace Saas
{
  public class Claim
  {
    protected readonly ClaimsPrincipal CP;
    protected readonly int RootId; 

    internal static StoreProc<T> DbContext<T>(ClaimsPrincipal cp, string connectionStr) where T: new()
    {
      var claim = new Claim(cp);
      return (claim.RootId > 0) ? new StoreProc<T>(claim.RootId, connectionStr) : null;
    }
    private Claim(ClaimsPrincipal cp)
    {
      CP = cp;
      RootId = 1;
    }
    internal StoreProc<T> GetDBContext<T>(string connectionStr) where T : new()
    {
      return (RootId > 0) ? new StoreProc<T>(RootId, connectionStr) : null;
    }
    internal bool IsAuthenticated() => CP.Identity.IsAuthenticated;
    internal bool IsInRole(string role) => CP.IsInRole(role);    
  }
}

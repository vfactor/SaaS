using Microsoft.Extensions.Configuration;
using System.Security.Claims;

using static Saas.Entities.States.Types;
using static Saas.Entities.Languages.Types;

namespace Saas.Entities
{
  public class AppData
  {
    public readonly string ConnectionString;

    public readonly States States;
    public readonly Languages Languages;
    public AppData(IConfiguration config)
    {
      ConnectionString = config.GetConnectionString("AllAboutFood");

      var cp = new ClaimsPrincipal();
      var claim = new Claim(cp);
      
      Languages = new Languages(claim.GetDBContext<Language>(ConnectionString).Read());      
      States = new States(claim.GetDBContext<State>(ConnectionString).Read());
    }   
  }
}

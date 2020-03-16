using Microsoft.Extensions.Configuration;
using System.Security.Claims;

using Saas.Entities;
using Saas.Dictionaries;

using static Saas.Entities.States.Types;
using static Saas.Entities.Languages.Types;
using static Saas.Dictionaries.KeyTypes.Types;

namespace Saas
{
  public partial class AppData
  {
    public readonly string ConnectionString;

    public readonly States States;
    public readonly Languages Languages;
    public readonly KeyTypes DictionaryKeyTypes;
    public AppData(IConfiguration config)
    {
      ConnectionString = config.GetConnectionString("AllAboutFood");

      var cp = new ClaimsPrincipal();
      
      Languages = new Languages(Claim.DbContext<Language>(cp, ConnectionString).Read());      
      States = new States(Claim.DbContext<State>(cp, ConnectionString).Read());
      DictionaryKeyTypes = new KeyTypes(Claim.DbContext<KeyType>(cp, ConnectionString).Read());
    }   
  }
}

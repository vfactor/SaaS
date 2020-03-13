using Saas.Entities.DAL;

namespace Saas.Entities
{
  public partial class RestaurantLanguage
  {    
    internal static Int Create(int rootId, RestaurantLanguage obj, string connectionStr)
    {
      return new Int(new BaseTable<RestaurantLanguage>(rootId, connectionStr).Create(obj));
    }
  }
}

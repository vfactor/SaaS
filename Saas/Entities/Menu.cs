using Saas.Entities.DAL;

namespace Saas.Entities
{
  public partial class Menus
  {
    public partial class Types
    {
      public partial class Menu
      {
        public static Menu Read(int id, string connectionStr)
        {
          var db = new DetailTable<Menu>(connectionStr);
          return db.Read(id);
        }

        public static Menus ReadByRestaurantId(int restaurantId, string connectionStr)
        {
          var db = new DetailTable<Menu>(connectionStr);
          return new Menus(db.ReadByMasterId("restaurantId", restaurantId));
        }

        public static Int Create(Menu obj, string connectionStr)
        {
          var db = new DetailTable<Menu>(connectionStr);
          return new Int(db.Create(obj));
        }
      }
    }
  }
}
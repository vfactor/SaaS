using Saas.Entities.DAL;

namespace Saas.Entities
{
  public partial class Items
  {
    public partial class Types
    {
      public partial class Item
      {
        public static Int Update(Item obj, string connectionStr)
        {
          var db = new DetailTable<Item>(connectionStr);
          return new Int(db.Update(obj));
        }

        public static Item Read(int id, string connectionStr)
        {
          var db = new DetailTable<Item>(connectionStr);
          return db.Read(id);
        }

        public static Int Create(Item obj, string connectionStr)
        {
          var db = new DetailTable<Item>(connectionStr);
          return new Int(db.Create(obj));
        }

        public static Items ReadByRestaurantId(int restaurantId, string connectionStr)
        {
          var db = new DetailTable<Item>(connectionStr);
          return new Items(db.ReadByMasterId("restaurantId", restaurantId));
        }
      }
    }
  }
}
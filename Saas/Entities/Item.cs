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
          return new Int(DetailTable<Item>.GetInstance(connectionStr).Update(obj));
        }

        public static Item Read(int id, string connectionStr)
        {
          return DetailTable<Item>.GetInstance(connectionStr).Read(id);
        }

        public static Int Create(Item obj, string connectionStr)
        {
          return new Int(DetailTable<Item>.GetInstance(connectionStr).Create(obj));
        }

        public static Items ReadByRestaurantId(int restaurantId, string connectionStr)
        {
          return new Items(
            DetailTable<Item>.GetInstance(connectionStr).ReadByMasterId("restaurantId", restaurantId)
          );
        }
      }
    }
  }
}
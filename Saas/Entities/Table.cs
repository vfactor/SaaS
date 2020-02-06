using Saas.Entities.DAL;

namespace Saas.Entities
{
  public partial class Tables
  {
    public partial class Types
    {
      public partial class Table
      {
        public static Table Read(int id, string connectionStr)
        {
          var db = new DetailTable<Table>(connectionStr);
          return db.Read(id);
        }

        public static Tables ReadByRestaurantId(int restaurantId, string connectionStr)
        {
          var db = new DetailTable<Table>(connectionStr);
          return new Tables(db.ReadByMasterId("restaurantId", restaurantId));
        }
      }
    }
  }
}
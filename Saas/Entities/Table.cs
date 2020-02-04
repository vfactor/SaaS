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
          return DetailTable<Table>.GetInstance(connectionStr).Read(id);
        }

        public static Tables ReadByRestaurantId(int restaurantId, string connectionStr)
        {
          return new Tables(
            DetailTable<Table>.GetInstance(connectionStr).ReadByMasterId("restaurantId", restaurantId)
          );
        }
      }
    }
  }
}
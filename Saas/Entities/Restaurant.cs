using Saas.Entities.DAL;

namespace Saas.Entities
{
  public partial class Restaurants
  {
    public partial class Types
    {
      public partial class Restaurant
      {
        public static Restaurant Read(int id, string connectionStr)
        {
          var db = new LookupTable<Restaurant>(connectionStr);
          return db.Read(id);
        }

        public static Restaurants Lookup(string value, string connectionStr)
        {
          var db = new LookupTable<Restaurant>(connectionStr);
          return new Restaurants(db.Lookup(value));
        }

        public static Int Update(Restaurant obj, string connectionStr)
        {
          var db = new LookupTable<Restaurant>(connectionStr);
          return new Int(db.Update(obj));
        }

        public static Int Create(Restaurant obj, string connectionStr)
        {
          var db = new LookupTable<Restaurant>(connectionStr);
          return new Int(db.Create(obj));
        }

        public static Int Delete(int id, string connectionStr)
        {
          var db = new LookupTable<Restaurant>(connectionStr);
          return new Int(db.Delete(id));
        }
      }
    }
  }
}
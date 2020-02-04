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
                    return LookupTable<Restaurant>.GetInstance(connectionStr).Read(id);
                }

                public static Restaurants Lookup(string value, string connectionStr)
                {
                    return new Restaurants(LookupTable<Restaurant>.GetInstance(connectionStr).Lookup(value));
                }

                public static Int Update(Restaurant obj, string connectionStr)
                {
                    return new Int(LookupTable<Restaurant>.GetInstance(connectionStr).Update(obj));
                }

                public static Int Create(Restaurant obj, string connectionStr)
                {
                    return new Int(LookupTable<Restaurant>.GetInstance(connectionStr).Create(obj));
                }

                public static Int Delete(int id, string connectionStr)
                {
                    return new Int(LookupTable<Restaurant>.GetInstance(connectionStr).Delete(id));
                }
            }
        }
    }
}
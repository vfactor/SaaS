using Saas.Entities.DAL;
using System.Collections.Generic;

namespace Saas.Entities
{
  public partial class Restaurants
  {
    public partial class Types
    {
      public partial class Restaurant
      {
        internal static Restaurant Read(int id, string connectionStr)
        {          
          return new ReferenceTable<Restaurant>(connectionStr).Read(id);
        }

       
       
        internal static Restaurants ReadAll(string connectionStr)
        {          
          return new Restaurants(new ReferenceTable<Restaurant>(connectionStr).Read());
        }               
      }
    }

    public Restaurants(IEnumerable<Types.Restaurant> values)
    {
      Values.AddRange(values);
    }
  }
}
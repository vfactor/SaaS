using Saas.Entities.DAL;
using System.Collections.Generic;

namespace Saas.Entities
{
  public partial class Menus
  {
    public partial class Types
    {
      public partial class Menu : Base<Menu>
      {        
       
      }
    }

    public Menus(IEnumerable<Types.Menu> values)
    {
      Values.AddRange(values);
    }
  }
}
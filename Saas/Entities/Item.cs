using System.Collections.Generic;

namespace Saas.Entities
{
  public partial class Items
  {    
    public Items(IEnumerable<Types.Item> values)
    {
      Values.AddRange(values);
    }
  }
}
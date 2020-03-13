using Saas.Entities.DAL;
using System.Collections.Generic;

namespace Saas.Entities
{
  public partial class MenuDetails
  {
    public partial class Types
    {
      public partial class MenuDetail : Base<MenuDetail>
      {        
        
      }
    }

    public MenuDetails(IEnumerable<Types.MenuDetail> values)
    {
      Values.AddRange(values);
    }
  }
}
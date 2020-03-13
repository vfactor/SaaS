using Saas.Entities.DAL;
using System.Collections.Generic;

namespace Saas.Entities
{
  public partial class Tables
  {    
    public Tables(IEnumerable<Types.Table> values)
    {
      Values.AddRange(values);
    }
  }
}
using System.Collections.Generic;

namespace Saas.Dictionaries
{
  public partial class Keys
  {   
    public Keys(IEnumerable<Types.Key> values)
    {
      Values.AddRange(values);
    }
  }
  public partial class Values
  {
    public Values(IEnumerable<Types.Value> values)
    {
      this.Values_.AddRange(values);
    }
  }
  public partial class KeyTypes
  {
    public KeyTypes(IEnumerable<Types.KeyType> values)
    {
      Values.AddRange(values);
    }
  }
}

using Saas.Entities.DAL;
using System.Collections.Generic;

namespace Saas.Entities
{
  public partial class Languages
  {
    public partial class Types
    {
      public partial class Language
      {
        internal static Languages Read(string connectionStr)
        {          
          return new Languages(new ReferenceTable<Language>(connectionStr).Read());
        }
      }
    }
    public Languages(IEnumerable<Types.Language> values)
    {
      Values.AddRange(values);
    }
  }
}
using Saas.Entities.DAL;

namespace Saas.Entities
{
  public partial class Languages
  {
    public partial class Types
    {
      public partial class Language
      {
        public static Languages Read(string connectionStr)
        {
          var db = new ReferenceTable<Language>(connectionStr);
          return new Languages(db.Read());
        }
      }
    }
  }
}
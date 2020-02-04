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
          return new Languages(
            ReferenceTable<Language>.GetInstance(connectionStr).Read()
          );
        }
      }
    }
  }
}
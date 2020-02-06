using System.Collections.Generic;
using System.Linq;
using Saas.Entities.DAL;

namespace Saas.Entities
{
  public partial class MenuItems
  {
    public partial class Types
    {
      public partial class MenuItem
      {
        public static MenuItem Read(int id, string connectionStr)
        {
          var db = new JoinTable<MenuItem>(connectionStr);
          return db.Read(id);
        }

        public static MenuItems ReadByMenuId(int menuId, string connectionStr)
        {
          var db = new JoinTable<MenuItem>(connectionStr);
          return new MenuItems(db.ReadByMasterId(new Dictionary<string, int> { { "menuId", menuId } }));
        }

        public static MenuItems ReadByItemId(int itemId, string connectionStr)
        {
          var db = new JoinTable<MenuItem>(connectionStr);
          return new MenuItems(db.ReadByMasterId(new Dictionary<string, int> { { "itemId", itemId } }));
        }

        public static MenuItem ReadByMenuAndItemId(IDictionary<string, int> masterInfos, string connectionStr)
        {
          var db = new JoinTable<MenuItem>(connectionStr);
          return db.ReadByMasterId(masterInfos)?.First();
        }
      }
    }
  }
}
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
          return JoinTable<MenuItem>.GetInstance(connectionStr).Read(id);
        }

        public static MenuItems ReadByMenuId(int menuId, string connectionStr)
        {
          return new MenuItems(JoinTable<MenuItem>.GetInstance(connectionStr).ReadByMasterId(new Dictionary<string, int> { { "menuId", menuId } }));
        }

        public static MenuItems ReadByItemId(int itemId, string connectionStr)
        {
          return new MenuItems(JoinTable<MenuItem>.GetInstance(connectionStr).ReadByMasterId(new Dictionary<string, int> { { "itemId", itemId } }));
        }

        public static MenuItem ReadByMenuAndItemId(IDictionary<string, int> masterInfos, string connectionStr)
        {
          return JoinTable<MenuItem>.GetInstance(connectionStr).ReadByMasterId(masterInfos)?.First();
        }
      }
    }
  }
}
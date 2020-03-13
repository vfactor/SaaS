using System.Collections.Generic;
using System.Linq;
using Saas.Entities.DAL;

namespace Saas.Entities
{
  public partial class MenuItems
  {
    public partial class Types
    {
      public partial class MenuItem : Base<MenuItem>
      {        
        internal static MenuItems ReadByMenuDetailAndItem(int rootId, MenuItemIds menuItemIds, string connectionStr)
        {
          var ids = new Dictionary<string, int> { { Key.MENUDETAIL, menuItemIds.MenuDetailId }, { Key.ITEM, menuItemIds.ItemId } };
          return new MenuItems(new JoinTable<MenuItem>(rootId, connectionStr).ReadByMasterIds(ids));
        }        
      }
    }

    public MenuItems(IEnumerable<Types.MenuItem> values)
    {
      Values.AddRange(values);
    }
  }
}
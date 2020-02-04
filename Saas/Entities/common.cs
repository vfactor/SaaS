using System.Collections.Generic;

namespace Saas.Entities
{
  public partial class Int
  {
    public Int(int i)
    {
      Value = i;
    }
  }

  public partial class Bool
  {
    public Bool(bool b)
    {
      Value = b;
    }
  }

  public partial class String
  {
    public String(string s)
    {
      Value = s;
    }
  }

  public partial class Restaurants
  {
    public Restaurants(IEnumerable<Types.Restaurant> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Items
  {
    public Items(IEnumerable<Types.Item> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Menus
  {
    public Menus(IEnumerable<Types.Menu> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Tables
  {
    public Tables(IEnumerable<Types.Table> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class Languages
  {
    public Languages(IEnumerable<Types.Language> values)
    {
      Values.AddRange(values);
    }
  }

  public partial class MenuItems
  {
    public MenuItems(IEnumerable<Types.MenuItem> values)
    {
      Values.AddRange(values);
    }
  }
}
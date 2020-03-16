using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Saas.Entities
{  
  public partial class Int
  {
    public Int(int val)
    {
      Value = val;
    }
    public Int(object obj)
    {
      if(obj == null)
        Value = -1;
    }
  }
  public partial class Bool
  {
    public Bool(bool val)
    {
      Value = val;
    }
  }
  public partial class String
  {
    public String(string val)
    {
      Value = val;
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
  public partial class RestaurantMenus
  {    
    public RestaurantMenus(IEnumerable<Types.RestaurantMenu> values)
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
  public partial class MenuItems
  {  
    public MenuItems(IEnumerable<Types.MenuItem> values)
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
  public partial class States
  { 
    internal Types.State Enable => Values.First(s => s.Id == 1 && s.Name == Constant.ENABLE);
    internal Types.State Disable => Values.First(s => s.Id == 2 && s.Name == Constant.DISABLE);
    internal Types.State Delete => Values.First(s => s.Id == 3 && s.Name == Constant.DELETE);

    public States(IEnumerable<Types.State> values)
    {
      Values.AddRange(values);      
    }
  }  
}

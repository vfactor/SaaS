using Saas.Entities.DAL;

namespace Saas.Entities
{
    public partial class Menus
    {
        public partial class Types
        {
            public partial class Menu
            {
                public static Menu Read(int id, string connectionStr)
                {
                    return DetailTable<Menu>.GetInstance(connectionStr).Read(id);
                }

                public static Menus ReadByRestaurantId(int restaurantId, string connectionStr)
                {
                    return new Menus(DetailTable<Menu>.GetInstance(connectionStr).ReadByMasterId("restaurantId", restaurantId));
                }

                public static Int Create(Menu obj, string connectionStr)
                {
                    return new Int(DetailTable<Menu>.GetInstance(connectionStr).Create(obj));
                }
            }
        }
    }
}
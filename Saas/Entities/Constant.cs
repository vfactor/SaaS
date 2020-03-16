namespace Saas.Entities
{
  internal static class Constant
  {
    public const string READSUFFIX = "_R";
    public const string CREATESUFFIX = "_C";
    public const string UPDATESUFFIX = "_U";
    public const string LOOKUPSUFFIX = "_L";
    public const string PARAMPREFIX = "@";
    public const string RETVAL = "retVal";
    public const string ID = "id";

    public const string ERR = "@err";

    public const string ENABLE = "Enable";
    public const string DISABLE = "Disable";
    public const string DELETE = "Delete";

    public const string KEYROOT = PARAMPREFIX + "root" + ID;
    public const string KEYID = PARAMPREFIX + "id";
    public const string KEYSTATE = PARAMPREFIX + "state" + ID;
    public const string KEYRESTAURANT = PARAMPREFIX + "restaurant" + ID;
    public const string KEYMENU = PARAMPREFIX + "menu" + ID;
    public const string KEYMENUDETAIL = PARAMPREFIX + "menuDetail" + ID;
    public const string KEYITEM = PARAMPREFIX + "item" + ID;
    public const string KEYVALUE = PARAMPREFIX + "value";
  }
}

using TBG.Data;

namespace TBG.Logic;

public static class Session
{
    public static User ActiveUser { get; set; }
    public static Save ActiveSave { get; set; }
    public static IDataAccess DataAccess { get; set; }

    static Session()
    {
        DataAccess = new SQLDataAccess();
    }
}
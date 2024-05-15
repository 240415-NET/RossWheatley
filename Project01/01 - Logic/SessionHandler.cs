using TBG.Data;

namespace TBG.Logic;

public static class SessionHandler
{
    public static Session CurrentSession { get; set; }
    public static IDataAccess DataAccess { get; set; }


    public static void StartNewSession()
    {
        CurrentSession = new();
        DataAccess = new JSONFileData();
    }
}
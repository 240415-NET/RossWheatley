namespace TBG.Logic;

public static class SessionHandler
{
    public static Session CurrentSession { get; set; }

    public static void StartNewSession()
    {
        CurrentSession = new();
    }
}
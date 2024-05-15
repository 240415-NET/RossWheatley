using TBG.Data;

namespace TBG.Logic;

public static class SaveHandler
{
    
    static List<Save> userSaveList = new();

    public static List<string> GetUserSavesList()
    {
        List<Save> saveList;

        if (SessionHandler.DataAccess.CheckFileExists(1))
        {
            saveList = SessionHandler.DataAccess.GetSaveList();
        }
        else { saveList = new(); }


        foreach (Save save in saveList)
        {
            if (save.UserId == SessionHandler.CurrentSession.ActiveUser.UserId)
            {
                userSaveList.Add(save);
            }
        }

        List<string> userStringList = new();

        if (userSaveList.Count() > 0)
        {
            // user list contains values
            for (int i = 0; i < userSaveList.Count(); i++)
            {
                userStringList.Add($"{i + 1}. Last saved: {userSaveList[i].SaveDate}");
            }
        }
        return userStringList;
    }

    public static bool CreateNewGame()
    {
        try
        {
            SessionHandler.CurrentSession.ActiveSave = new Save(SessionHandler.CurrentSession.ActiveUser, new GameObject(true));
            SessionHandler.DataAccess.PersistSave(SessionHandler.CurrentSession.ActiveSave);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool LoadSave(int input)
    {
        try
        {
            SessionHandler.CurrentSession.ActiveSave = userSaveList[input - 1];
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static void AutoPersistActiveSave()
    {
        if (SessionHandler.CurrentSession.ActiveSave != null)
        {
            SessionHandler.CurrentSession.ActiveSave.SaveDate = DateTime.Now;
            SessionHandler.DataAccess.PersistSave(SessionHandler.CurrentSession.ActiveSave);
        }
    }
}
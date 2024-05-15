namespace TBG.Logic;

public static class SaveHandler
{
    
    static List<Save> userSaveList = new();

    public static List<string> GetUserSavesList()
    {
        List<Save> saveList;

        if (Session.DataAccess.CheckFileExists(1))
        {
            saveList = Session.DataAccess.GetSaveList();
        }
        else { saveList = new(); }


        foreach (Save save in saveList)
        {
            if (save.UserId == Session.ActiveUser.UserId)
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
            Session.ActiveSave = new Save(Session.ActiveUser, new GameObject(true));
            Session.DataAccess.PersistSave(Session.ActiveSave);
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
            Session.ActiveSave = userSaveList[input - 1];
        }
        catch
        {
            return false;
        }
        return true;
    }

    public static void AutoPersistActiveSave()
    {
        if (Session.ActiveSave != null)
        {
            Session.ActiveSave.SaveDate = DateTime.Now;
            Session.DataAccess.PersistSave(Session.ActiveSave);
        }
    }
}
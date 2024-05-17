using TBG.Data;

namespace TBG.Logic;

public static class SaveHandler
{
    static List<Save> userSaveList = new();

    public static void EmptyUserSaveList()
    {
        userSaveList.Clear();
    }

    public static List<string> GetUserSavesList()
    {
        List<Save> saveList = new();

        if (Session.DataAccess.CheckFileExists(1))
        {
            saveList = Session.DataAccess.GetSaveList();
        }


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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
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
            EmptyUserSaveList();
        }
    }

    public static bool PlayerHasUnits()
    {
        return Session.ActiveSave.Units > 0;
    }

    public static void DeleteActiveSave()
    {
        Session.DataAccess.DeleteSave(Session.ActiveSave);
    }
}
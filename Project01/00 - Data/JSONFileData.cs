using System.Diagnostics;
using System.Text.Json;
using TBG.Presentation;

namespace TBG.Data;

public class JSONFileData : IDataAccess
{
    private readonly static string _savesFile = "./json/SavesFile.json";
    private readonly static string _usersFile = "./json/UsersFile.json";

    #region -- Saves -- 

    void DeleteSave(Save save)
    {
        List<Save> saves = GetSaveList();
        saves.RemoveAll(s => s.SaveId == save.SaveId);
        SaveSaveData(saves);
    }

    // Returns list of all saves in json
    public List<Save> GetSaveList()
    {
        string json = File.ReadAllText(_savesFile);
        return JsonSerializer.Deserialize<List<Save>>(json) ?? new List<Save>();
    }

    bool SaveExists(Save save)
    {
        List<Save> saves = new();

        if (File.Exists(_savesFile))
        {
            saves = GetSaveList();
        }
        return saves.Exists(s => s.SaveId == save.SaveId);
    }

    void SaveSaveData(List<Save> saves)
    {
        string json = JsonSerializer.Serialize(saves);
        File.WriteAllText(_savesFile, json);
    }

    public void PersistSave(Save save)
    {
        List<Save> saves = new();
        if (File.Exists(_savesFile))
        {
            // If previous save data exists, delete it
            if (SaveExists(save))
            {
                DeleteSave(save);
            }
            saves = GetSaveList();
        }
        saves.Add(save);
        SaveSaveData(saves);
    }

    #endregion 

    #region -- User --

    public User GetUser(string userName)
    {
        List<User> users = GetUserList();
        foreach (User user in users)
        {
            if (user.UserName == userName)
            {
                return user;
            }
        }
        return new User();
    }

    public List<User> GetUserList()
    {
        string json = File.ReadAllText(_usersFile);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    void SaveUserData(List<User> users)
    {
        string json = JsonSerializer.Serialize(users);
        File.WriteAllText(_usersFile, json);
    }

    public void StoreUser(User user)
    {
        if (File.Exists(_usersFile))
        {
            // New user name
            List<User> existingUsers = GetUserList();
            existingUsers.Add(user);
            SaveUserData(existingUsers);

        }
        else
        {
            List<User> users = new();
            users.Add(user);
            SaveUserData(users);
        }
    }

    #endregion
}
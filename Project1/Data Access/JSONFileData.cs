using System.Diagnostics;
using System.Text.Json;
using TBG.Presentation;

namespace TBG.Data;

public class JSONFileData : IDataAccess
{
    private readonly static string _usersFile = "UsersFile.json";
    private readonly static string _savesFile = "SavesFile.json";

    public List<Save> GetSaveList()
    {
        if (File.Exists(_savesFile))
        {
            string json = File.ReadAllText(_savesFile);
            return JsonSerializer.Deserialize<List<Save>>(json) ?? new List<Save>();
        }
        else
        {
            return new List<Save>();
        }
    }

    public void StoreUser(User user)
    {
        if (File.Exists(_usersFile))
        {
            if (!UserExists(user.UserName))
            {
                // New user name
                List<User> existingUsers = GetUserList();
                existingUsers.Add(user);
                SaveUserData(existingUsers);
            }
            else
            {
                // Username already exists
            }
        }
        else
        {
            List<User> users = new();
            users.Add(user);
            SaveUserData(users);
        }
    }

    void SaveUserData(List<User> users)
    {
        string json = JsonSerializer.Serialize(users);
        File.WriteAllText(_usersFile, json);
    }

    public bool UserExists(string userName)
    {
        if (File.Exists(_usersFile))
        {
            List<User> existingUsers = GetUserList();
            bool noDuplicatesFound = existingUsers.Any(user => user.UserName == userName);
            return noDuplicatesFound;
        }
        else
        {
            return false;
        }
    }

    List<User> GetUserList()
    {
        string json = File.ReadAllText(_usersFile);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }
}
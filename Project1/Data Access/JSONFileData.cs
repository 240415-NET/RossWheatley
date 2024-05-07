using System.Diagnostics;
using System.Text.Json;
using TBG.Presentation;

namespace TBG.Data;

public class JSONFileData : IDataAccess
{
    private readonly static string _filePath = "UsersFile.json";

    public void StoreUser(User user)
    {
        if (FilePathExists())
        {
            Console.WriteLine("File path exists.");
            if (!UserExists(user.UserName))
            {
                Console.WriteLine("Username does not exist");
                // New user name
                List<User> existingUsers = GetUserList();
                existingUsers.Add(user);
                SaveData(existingUsers);
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
            SaveData(users);
        }
    }

    void SaveData(List<User> users)
    {
        Debug.WriteLine("SaveData");
        string json = JsonSerializer.Serialize(users);
        File.WriteAllText(_filePath, json);
    }

    public bool UserExists(string userName)
    {
        if (FilePathExists())
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
        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    bool FilePathExists()
    {
        return File.Exists(_filePath);
    }
}
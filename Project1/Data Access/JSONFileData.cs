using System.Diagnostics;
using System.Text.Json;
using TBG.Presentation;

namespace TBG.Data;

public class JSONFileData : IDataAccess
{
    private readonly static string _filePath = "UsersFile.json";

    public bool StoreUser(User user)
    {
        if (FilePathExists() && !UserExists(user.UserName))
        {
            // Store user appending to existing file
            List<User> existingUserList = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_filePath));
            existingUserList.Add(user);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(existingUserList));
            return true;
        }
        else if (!UserExists(user.UserName))
        {
            List<User> userList = new();
            // Store user in new file
            userList.Add(user);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(userList));
            return true;
        }
        PresentationUtility.DisplayMessage();
        return false;
    }

    public bool UserExists(string userName)
    {
        return FilePathExists() && GetUserList().Any(user => user.UserName == userName);
    }

    List<User> GetUserList()
    {
        try
        {
            return JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_filePath));
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
        return new List<User>();
    }

    bool FilePathExists()
    {
        return File.Exists(_filePath);
    }
}
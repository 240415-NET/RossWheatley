using TBG;

public interface IDataAccess
{
    void DeleteSave(Save save);
    void PersistSave(Save save);
    void StoreUser(User user); // Stores new user data
    // bool UserExists(String userName); // Returns whether a user name already exists
    User GetUser(string userName);
    List<User> GetUserList();
    List<Save> GetSaveList();
    bool CheckFileExists(int fileIndex);
}
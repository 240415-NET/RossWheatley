using TBG;

public interface IDataAccess
{
    void StoreSave(Save save);
    void StoreUser(User user); // Stores new user data
    bool UserExists(String userName); // Returns whether a user name already exists
    User GetUser(string userName);
}
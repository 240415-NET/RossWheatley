using TBG;

public interface IDataAccess
{
    bool StoreUser(User user); // Stores new user data
    bool UserExists(String userName); // Returns whether a user name already exists
}
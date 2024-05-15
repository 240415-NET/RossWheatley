using TBG.Data;

namespace TBG.Logic;

public static class LoginHandler
{
    static IDataAccess dataAccess = new JSONFileData();

    public static bool CheckUserExists(string input)
    {
        List<User> existingUsers = dataAccess.GetUserList();
        return existingUsers.Any(user => user.UserName == input);
    }

    public static bool CreateNewUser(string input)
    {
        User newUser = new(input);
        dataAccess.StoreUser(newUser);

        if (Login(newUser))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool FindExistingUser(string input)
    {
        if (CheckUserExists(input))
        {
            // user exists log them in
            Login(dataAccess.GetUser(input));
            return true;
        }
        else
        {
            // user not found
            return false;
        }

    }

    public static bool Login(User user)
    {
        Session session = SessionHandler.CurrentSession;
        session.ActiveUser = user;
        return true;
    }
}
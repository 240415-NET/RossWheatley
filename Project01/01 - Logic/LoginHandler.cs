namespace TBG.Logic;

public static class LoginHandler
{
    public static bool CheckUserExists(string input)
    {
        if (Session.DataAccess.CheckFileExists(0))
        {
            List<User> existingUsers = Session.DataAccess.GetUserList();
            return existingUsers.Any(user => user.UserName == input);
        }
        else
        {
            return false;
        }
    }

    public static bool CreateNewUser(string input)
    {
        User newUser = new(input);
        Session.DataAccess.StoreUser(newUser);

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
            Login(Session.DataAccess.GetUser(input));
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
        Session.ActiveUser = user;
        return true;
    }
}
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

    public static bool CreateNewUser(string userName, string password)
    {
        User newUser = new(userName);
        Session.DataAccess.StoreUser(newUser, Authentication.HashPassword(password));
        Session.ActiveUser = newUser;
        return true;
    }

    public static bool Login(string username, string password)
    {
        User user = Session.DataAccess.GetUser(username);

        byte[] salt = Session.DataAccess.GetAuthentication(username, "salt");
        byte[] hash = Session.DataAccess.GetAuthentication(username, "hash");
        if (Authentication.VerifyPassword(salt, hash, password))
        {
            Session.ActiveUser = user;
            return true;
        }
        else
        {
            return false;
        }
    }
}
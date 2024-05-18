using System.Data.SqlClient;

namespace TBG.Data;

public class SQLDataAccess : IDataAccess
{
    public void DeleteSave(Save save)
    {

    }
    public void PersistSave(Save save)
    {

    }
    public void StoreUser(User user)
    {

    }
    public User GetUser(string userName)
    {
        return new User();
    }
    public List<User> GetUserList()
    {
        return new List<User>();
    }
    public List<Save> GetSaveList()
    {
        return new List<Save>();
    }
    public bool CheckFileExists(int fileIndex)
    {
        return false;
    }
}
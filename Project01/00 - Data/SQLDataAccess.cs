using System.Data.SqlClient;
using TBG.Logic;

namespace TBG.Data;

public class SQLDataAccess : IDataAccess
{
    public static string connectionString = File.ReadAllText(@"C:\Tools\connection.txt");

    readonly Dictionary<int, string> query = new Dictionary<int, string>() {
            {1, @"DELETE FROM dbo.Encounters WHERE SaveId = @SaveId"},
            {2, @"DELETE FROM dbo.Tasks WHERE SaveId = @SaveId"},
            {3, @"DELETE FROM dbo.Items WHERE SaveId = @SaveId"},
            {4, @"DELETE FROM dbo.PlayerObject WHERE SaveId = @SaveId"},
            {5, @"DELETE FROM dbo.PlayerItem WHERE SaveId = @SaveId"},
            {6, @"DELETE FROM dbo.Skills WHERE SaveId = @SaveId"},
            {7, @"DELETE FROM dbo.Attributes WHERE SaveId = @SaveId"},
            {8, @"DELETE FROM dbo.Saves WHERE SaveId = @SaveId"}
        };

    public void PersistSave(Save save)
    {
        DeleteSave(save); // Delete entry if saveid already exists
        StoreSave(save); // add data as new entry
    }

    #region -- Store Functions --
    // dbo.Encounters
    // dbo.Items
    // dbo.Saves
    // dbo.Tasks

    #region -- Store Functions :: Player Data --
    // dbo.Attributes
    // dbo.Skills 
    // dbo.PlayerObject -- Done

    void StorePlayer(Save save, SqlConnection connection)
    {
        DeleteSaveExecuteNonQuery(save, connection, 4);
        DeleteSaveExecuteNonQuery(save, connection, 5);

        string query = @"INSERT INTO dbo.PlayerObject
        (SaveId,IsPlayer,Level,Experience,SkillPoints,AttributePoints,CoordinateX,CoordinateY,CharacterClass)
        VALUES (@SaveId,@IsPlayer,@Level,@Experience,@SkillPoints,@AttributePoints,@CoordinateX,@CoordinateY,@CharacterClass)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
        cmd.Parameters.AddWithValue("@IsPlayer", save.PlayerObject.IsPlayer);
        cmd.Parameters.AddWithValue("@Level", save.PlayerObject.Experience);
        cmd.Parameters.AddWithValue("@Experience", save.PlayerObject.Experience);
        cmd.Parameters.AddWithValue("@SkillPoints", save.PlayerObject.SkillPoints);
        cmd.Parameters.AddWithValue("@AttributePoints", save.PlayerObject.AttributePoints);
        cmd.Parameters.AddWithValue("@CoordinateX", save.PlayerObject.Coordinates.X);
        cmd.Parameters.AddWithValue("@CoordinateY", save.PlayerObject.Coordinates.Y);
        cmd.Parameters.AddWithValue("@CharacterClass", save.PlayerObject.CharacterClass);
        cmd.ExecuteNonQuery();

        StoreSkills(save, connection);

        if (save.PlayerObject.Item != null)
        {
            StorePlayerItem(save, connection);
        }
    }

    void StoreAttributes(Save save, SqlConnection connection)
    {
        DeleteSaveExecuteNonQuery(save, connection, 7);

        for (int i = 0; i < save.PlayerObject.Skills.Count(); i++)
        {
            string query = @"INSERT INTO dbo.Attributes
        (SaveId, AttributeIndex, AttributeValue)
        VALUES (@SaveId, @AttributeIndex, @AttributeValue)";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
            cmd.Parameters.AddWithValue("@AttributeIndex", i);
            cmd.Parameters.AddWithValue("@AttributeValue", save.PlayerObject.Attributes[i]);
            cmd.ExecuteNonQuery();
        }
    }

    void StoreSkills(Save save, SqlConnection connection)
    {
        DeleteSaveExecuteNonQuery(save, connection, 6);

        for (int i = 0; i < save.PlayerObject.Skills.Count(); i++)
        {
            string query = @"INSERT INTO dbo.Skills
        (SaveId, SkillIndex, SkillValue)
        VALUES (@SaveId, @SkillIndex, @SkillValue)";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
            cmd.Parameters.AddWithValue("@SkillIndex", i);
            cmd.Parameters.AddWithValue("@SkillValue", save.PlayerObject.Skills[i]);
            cmd.ExecuteNonQuery();
        }
    }

    void StorePlayerItem(Save save, SqlConnection connection)
    {
        string query = @"INSERT INTO dbo.PlayerItem
        (SaveId, ItemId, SkillIndex, Modifier)
        VALUES (@SaveId, @ItemId, @SkillIndex, @Modifier)";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
        cmd.Parameters.AddWithValue("@ItemId", save.PlayerObject.Item.ItemId);
        cmd.Parameters.AddWithValue("@SkillIndex", save.PlayerObject.Item.SkillIndex);
        cmd.Parameters.AddWithValue("@Modifier", save.PlayerObject.Item.Modifier);
        cmd.ExecuteNonQuery();
    }

    #endregion

    #region -- Store Functions :: Save Data --

    void StoreSave(Save save)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string query = @"INSERT INTO dbo.Saves 
        (SaveId, UserId, SaveDate, Turns, Units, GridConstraintX, GridConstraintY) 
        VALUES (@SaveId, @UserId, @SaveDate, @Turns, @Units, @GridConstraintX, @GridConstraintY);";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
        cmd.Parameters.AddWithValue("@UserId", Session.ActiveUser.UserId);
        cmd.Parameters.AddWithValue("@SaveDate", save.SaveDate);
        cmd.Parameters.AddWithValue("@Turns", save.Turns);
        cmd.Parameters.AddWithValue("@Units", save.Units);
        cmd.Parameters.AddWithValue("@GridConstraintX", save.GridConstraints.X);
        cmd.Parameters.AddWithValue("@GridConstraintY", save.GridConstraints.Y);
        cmd.ExecuteNonQuery();

        StoreTasks(save, connection);
        StoreEncounters(save, connection);
        StoreItems(save, connection);

        StorePlayer(save, connection);

        connection.Close();
    }

    void StoreEncounters(Save save, SqlConnection connection) // Store Helper
    {
        DeleteSaveExecuteNonQuery(save, connection, 1);

        foreach (Encounter encounter in save.Encounters)
        {
            string query = @"INSERT INTO dbo.Encounters (EncounterId, SaveId, CoordinateX, CoordinateY) 
            VALUES (@TaskId, @SaveId, @CoordinateX, @CoordinateY);";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TaskId", encounter.EncounterId);
            cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
            cmd.Parameters.AddWithValue("@CoordinateX", encounter.Coordinates.X);
            cmd.Parameters.AddWithValue("@CoordinateY", encounter.Coordinates.Y);
            cmd.ExecuteNonQuery();
        }
    }

    void StoreTasks(Save save, SqlConnection connection) // Store Helper
    {
        DeleteSaveExecuteNonQuery(save, connection, 2);

        foreach (Task task in save.Tasks)
        {
            string query = @"INSERT INTO dbo.Tasks (TaskId, SaveId, UnitCost, Reward, Probability, CoordinateX, CoordinateY) 
            VALUES (@TaskId, @SaveId, @UnitCost, @Reward, @Probability, @CoordinateX, @CoordinateY);";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TaskId", task.TaskId);
            cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
            cmd.Parameters.AddWithValue("@UnitCost", task.UnitCost);
            cmd.Parameters.AddWithValue("@Reward", task.Reward);
            cmd.Parameters.AddWithValue("@Probability", task.Probability);
            cmd.Parameters.AddWithValue("@CoordinateX", task.Coordinates.X);
            cmd.Parameters.AddWithValue("@CoordinateY", task.Coordinates.Y);
            cmd.ExecuteNonQuery();
        }
    }

    void StoreItems(Save save, SqlConnection connection) // Store Helper
    {
        DeleteSaveExecuteNonQuery(save, connection, 3);

        foreach (Item item in save.Items)
        {
            string query = @"INSERT INTO dbo.Items (ItemId, SaveId, SkillIndex, Modifier, CoordinateX, CoordinateY) 
            VALUES (@TaskId, @SaveId, @SkillIndex, @Modifier, @CoordinateX, @CoordinateY);";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@TaskId", item.ItemId);
            cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
            cmd.Parameters.AddWithValue("@SkillIndex", item.SkillIndex);
            cmd.Parameters.AddWithValue("@Modifier", item.Modifier);
            cmd.Parameters.AddWithValue("@CoordinateX", item.Coordinates.X);
            cmd.Parameters.AddWithValue("@CoordinateY", item.Coordinates.Y);
            cmd.ExecuteNonQuery();
        }
    }
    #endregion

    #region -- Store Functions :: Delete Functions --
    public void DeleteSave(Save save)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        for (int i = 1; i <= query.Count(); i++)
        {
            DeleteSaveExecuteNonQuery(save, connection, i);
        }
        connection.Close();
    }

    void DeleteSaveExecuteNonQuery(Save save, SqlConnection connection, int index)
    {
        using SqlCommand cmd = new SqlCommand(query[index], connection);
        cmd.Parameters.AddWithValue("@SaveId", save.SaveId);
        cmd.ExecuteNonQuery();
    }
    #endregion

    #endregion

    #region -- User Data --
    // dbo.Users
    public User GetUser(string userName)
    {
        User user = new User();
        using SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
            string query = @"SELECT UserId, UserName FROM dbo.Users WHERE UserName = @UserName;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserName", userName);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.UserId = reader.GetGuid(0);
                user.UserName = reader.GetString(1);
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }
        return user;
    }

    public List<User> GetUserList()
    {
        List<User> userList = new();
        using SqlConnection connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
            string query = @"SELECT UserId, UserName FROM dbo.Users;";
            using SqlCommand cmd = new SqlCommand(query, connection);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                userList.Add(new User(reader.GetGuid(0), reader.GetString(1)));
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }
        return userList;
    }

    public void StoreUser(User user)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        string query = @"INSERT INTO dbo.Users (UserId, UserName) VALUES (@UserId, @UserName);";
        using SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UserId", user.UserId);
        cmd.Parameters.AddWithValue("@UserName", user.UserName);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    #endregion

    public bool CheckFileExists(int fileIndex)
    {
        // This method is carryover from the interface and kind of redundant with SQL implementation
        // Did my best to make it useful while not changing any code structure that previously implements this method
        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            connection.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }

    #region -- Get Functions --

    public List<Save> GetSaveList()
    {
        Save save = new();
        List<Save> saveList = new();
        GameObject playerObject = new();
        List<Item> items = new();
        List<Task> tasks = new();
        List<Encounter> encounters = new();

        using SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        string query = @"SELECT SaveId, UserId, SaveDate, Turns, Units, GridConstraintX, GridConstraintY
        FROM dbo.Saves";
        using SqlCommand cmd = new SqlCommand(query, connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        // Save(Guid saveId, Guid userId, GameObject playerObject, DateTime saveDate, int turns, int units, Coord gridConstraints, List<Task> tasks, List<Encounter> encounters, List<Item> items)
        while (reader.Read())
        {
            save = new Save(reader.GetGuid(0), reader.GetGuid(1), playerObject, (DateTime)reader.GetDateTime(2), reader.GetInt32(3), reader.GetInt32(4), new Coord(reader.GetInt32(5), reader.GetInt32(6)), tasks, encounters, items);
        }
        saveList.Add(save);
        connection.Close();


        return saveList;
    }

    #endregion

}
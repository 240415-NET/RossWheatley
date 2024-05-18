namespace Project1.Data;

public static class Connection
{
    public static string GetStringFromText()
    {
        string filePath = "C:/Tools/connection.txt";
        if (File.Exists(filePath))
        {
            string connString = File.ReadAllText(filePath);
            return connString;
        }
        else
        {
            return String.Empty;
        }
    }
}

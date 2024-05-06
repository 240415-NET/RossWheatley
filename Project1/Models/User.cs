namespace TBG;

public class User
{
    public Guid UserId { get; private set; }
    public string UserName { get; private set; }

    public User()
    {
        UserId = Guid.NewGuid();
        UserName = "";
    }

    public User(string name) : this()
    {
        UserName = name;
    }
}
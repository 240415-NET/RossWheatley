namespace TBG;

public class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    
    public User() { }

    public User(string name)
    {
        UserId = Guid.NewGuid();
        UserName = name;
    }
}
namespace TBG;

public class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }

    private List<Guid> saves;

    public User() { }

    public User(string name)
    {
        UserId = Guid.NewGuid();
        UserName = name;
    }
}
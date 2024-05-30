namespace Practice.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public User()
    {
        Id = Guid.NewGuid();
    }

    public User(string name) : base()
    {
        Name = name;
    }
}
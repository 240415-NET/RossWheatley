namespace Practice.Entities;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Item()
    {
        Id = Guid.NewGuid();
    }

    public Item(string name) : base()
    {
        Name = name;
    }
}
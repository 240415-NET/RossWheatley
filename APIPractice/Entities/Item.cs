namespace Practice.Entities;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Item() { }

    public Item(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}
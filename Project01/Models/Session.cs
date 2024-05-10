using System.Reflection.Metadata;

namespace TBG;

public class Session
{
    public User ActiveUser { get; set; }
    public Save ActiveSave { get; set; }

    public Session() { }
}

public class SaveFile
{
    public List<Item> Items;
    public List<Pet> Pets;
    public List<Document> Documents;
}
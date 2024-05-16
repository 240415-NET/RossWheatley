namespace TBG;

public class Item
{
    public Guid ItemId { get; set; }
    public int SkillIndex { get; set; }
    public int Modifier { get; set; }
    public Coord Coordinates { get; set; }

    public Item()
    {
        ItemId = Guid.NewGuid();
    }
}
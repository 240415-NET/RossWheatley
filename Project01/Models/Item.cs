namespace TBG;

public class Item
{
    public Guid SaveId { get; set; }
    public Guid ItemId { get; set; }
    public int SkillIndex { get; set; }
    public int Modifier { get; set; }
    public Coord Coordinates { get; set; }

    public Item()
    {
        ItemId = Guid.NewGuid();
    }

    public Item(Guid saveId, Guid itemId, int skillIndex, int modifier, Coord coordinates)
    {
        SaveId = saveId;
        ItemId = itemId;
        SkillIndex = skillIndex;
        Modifier = modifier;
        Coordinates = coordinates;
    }

    public Item(Guid saveId, Guid itemId, int skillIndex, int modifier)
    {
        SaveId = saveId;
        ItemId = itemId;
        SkillIndex = skillIndex;
        Modifier = modifier;
        Coordinates = new Coord(0, 0);
    }
}
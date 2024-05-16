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

    /*
        public Item(int level)
        {
            Random random = new();
            // GameObject obj = new();
            // SkillIndex = random.Next(obj.Skills.Count());
            // Modifier = level + random.Next(3);
        }
        */
}
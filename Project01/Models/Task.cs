namespace TBG;

public class Task
{
    // Attempting tasks costs units
    // Attempting tasks gain XP
    // Successfully completing tasks return bonus XP
    // AttributeA & AttributeB will factor into probability of "winning" a task
    public int unitCost { get; set; } // random # between 1 - 5
    private int reward { get; set; } // unitCost * random # between 1 - 5
    private float probability { get; set; } // random # between 1 - 100
    public Coord Coordinates { get; set; }

    public Task() { }
}